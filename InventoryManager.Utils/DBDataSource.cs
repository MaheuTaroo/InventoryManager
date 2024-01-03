using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace InventoryManager.Utils
{
    public class DBDataSource : IDataSource, IDisposable
    {
        private ReadOnlyCollection<string> Tables = new ReadOnlyCollection<string>(new List<string>() { "items", "restock_hist", "sale_items", "unit_types" });

        private MySqlConnection? _conn;
        public MySqlConnection? Connection
        {
            get
            {
                return _conn;
            }
            private set
            {
                if (value == null || !value.Ping()) return;

                if (_conn != null)
                {
                    _conn.CancelQuery(0);
                    _conn.Close();
                    _conn.Dispose();
                }

                _conn = value;
                _conn.Open();
            }
        }

        public string Table { private get; set; } = "";

        public bool CanConnect() => Connection?.Ping() ?? false;

        private bool WriteToDB(ref ElementData value, bool isInserting)
        {
            if (Connection == null) return false;

            MySqlCommand cmd = new MySqlCommand($"{(isInserting ? "insert" : "update")} into @table (@elements) values (@values);", Connection);

            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@elements", value.GetType().GetFields().ToList()
                                                          .ConvertAll(info => info.Name)
                                                          .Aggregate((el1, el2) => el1 + ", " + el2));
            cmd.Parameters.AddWithValue("@values", value.GetType().GetFields()
                                                        .Select(info => info.GetValue(info.Name)!.ToString()!)
                                                        .Aggregate((el1, el2) => el1 + ", " + el2));

            cmd.Prepare();
            return cmd.ExecuteNonQuery() != 0;
        }

        public DBDataSource(string ipAddr, int port, string user, string pass, string db)
        {
            Connect($"server={ipAddr}:{port};uid={user};pwd={pass};database={db}");
        }

        public bool Connect(string link)
        {
            if (Connection != null && Connection.ConnectionString != link)
                Connection = new MySqlConnection(link);

            return Connection?.State == ConnectionState.Open;
        }

        public bool Disconnect()
        {
            if (Connection == null || Connection.State != ConnectionState.Open) return false;

            Connection.CancelQuery(0);
            Connection.Close();
            return true;
        }

        public bool WriteRow(ref ElementData value)
        {
            return WriteToDB(ref value, true);
        }

        public ElementData ReadRow(int id)
        {
            if (Connection == null || !IsValidTableName(Table)) return ElementData.EMPTY;

            using MySqlCommand cmd = new MySqlCommand("select * from @table where ID = @id;", Connection);
            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return ElementData.EMPTY;

            reader.Read();
            return ElementData.FromReader(Table, reader);
        }

        private bool IsValidTableName(string table) => Tables.Contains(table);

        public bool UpdateRow(ref ElementData value) => WriteToDB(ref value, false);

        public bool DeleteRow(int id)
        {
            if (Connection != null && Connection.State == ConnectionState.Closed) return false;

            MySqlCommand cmd = new MySqlCommand("delete from @table where ID = @id;", Connection);
            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            return cmd.ExecuteNonQuery() > 0;
        }

        public void Dispose()
        {
            if (Connection == null) return;
            if (Connection.State == ConnectionState.Open) Connection.Close();
            Connection.Dispose();
        }

        ~DBDataSource() {
            Dispose();
        }
    }
}
