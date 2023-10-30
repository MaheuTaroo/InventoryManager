using MySql.Data.MySqlClient;

namespace InventoryManager.Utils
{
    public class Settings
    {
        private IDataSource? src;

        public Settings(IDataSource source) 
        {
            src = source;
        }
    }
}