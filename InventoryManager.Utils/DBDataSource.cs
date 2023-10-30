using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlTypes;

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

        public DBDataSource(string ipAddr, int port, string user, string pass, string db)
        {
            Connect($"server={ipAddr}:{port};uid={user};pwd={pass};database={db}");
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
            throw new NotImplementedException();
        }

        public ElementData ReadRow(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRow(int id, ElementData value)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRow(int id)
        {
            if (Connection.State == ConnectionState.Closed) return false;
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "delete from @table where ID = @id;";
            cmd.Parameters.AddWithValue("@table", Table);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            return cmd.ExecuteNonQuery() > 0;

        }

        public void Dispose()
        {
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        ~DBDataSource() {
            Dispose();
        }
    }
}
