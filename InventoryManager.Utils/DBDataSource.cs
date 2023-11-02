using MySql.Data.MySqlClient;
using System.Data;

namespace InventoryManager.Utils
{
    public class DBDataSource : IDataSource, IDisposable
    {
        private MySqlConnection Connection
        {
            get
            {
                return Connection;
            }
            set
            {
                if (value == null || !value.Ping()) return;

                if (Connection != null)
                {
                    Connection.CancelQuery(0);
                    Connection.Close();
                    Connection.Dispose();
                }

                Connection = value;
                Connection.Open();
            }
        }

        public string Table { private get; set; } 

        private bool WriteToDB(ElementData value, bool isInserting)
        {
            MySqlCommand cmd = new MySqlCommand($"{(isInserting ? "insert" : "update")} into @table (@elements) values (@values);", Connection);

            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@elements", value.GetType().GetFields()
                                                          .Select(info => info.Name)
                                                          .Aggregate((el1, el2) => el1 + ", " + el2));
            cmd.Parameters.AddWithValue("@values", value.GetType().GetFields()
                                                        .Select(info => info.GetValue(info.Name)!.ToString()!)
                                                        .Aggregate((el1, el2) => el1 + ", " + el2));

            cmd.Prepare();
            return cmd.ExecuteNonQuery() != 0;
        }

        public DBDataSource(string ipAddr, int port, string user, string pass, string db, string table)
        {
            Connect($"server={ipAddr}:{port};uid={user};pwd={pass};database={db}");
            Table = table;
        }

        public bool Connect(string link)
        {
            MySqlConnection tmp = Connection;
            Connection = new MySqlConnection(link);
            return Connection.Equals(tmp);
        }

        public bool Disconnect()
        {
            if (Connection.State != ConnectionState.Open) return false;

            Connection.CancelQuery(0);
            Connection.Close();
            return true;
        }

        public bool WriteRow(ElementData value)
        {
            return WriteToDB(value, true);
        }

        public ElementData ReadRow(int id)
        {
            MySqlCommand cmd = new MySqlCommand("select * from @table where ID = @id;", Connection);
            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) return ElementData.EMPTY;
                reader.Read();
                return new PerishableElementData(int.Parse(reader[0].ToString()!),
                                                 reader[1].ToString()!,
                                                 int.Parse(reader[2].ToString()!),
                                                 int.Parse(reader[3].ToString()!),
                                                 DateTime.Parse(reader[4].ToString()!));
            }
        }

        public bool UpdateRow(ElementData value)
        {
            return WriteToDB(value, false);
        }

        public bool DeleteRow(int id)
        {
            if (Connection.State == ConnectionState.Closed) return false;

            MySqlCommand cmd = new MySqlCommand("delete from @table where ID = @id;", Connection);
            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            return cmd.ExecuteNonQuery() > 0;
        }

        public void Dispose()
        {
            if (Connection.State == ConnectionState.Open) Connection.Close();
            Connection.Dispose();
        }

        ~DBDataSource() {
            Dispose();
        }
    }
}
