namespace InventoryManager.Utils
{
    public interface IDataSource
    {
        public bool Connect(string link);
        public bool Disconnect();
        public bool WriteRow(ref ElementData value);
        public ElementData ReadRow(int id);
        public bool UpdateRow(ref ElementData value);
        public bool DeleteRow(int id);
    }
}
