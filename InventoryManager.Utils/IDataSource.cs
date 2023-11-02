namespace InventoryManager.Utils
{
    public interface IDataSource
    {
        public bool Connect(string link);
        public bool Disconnect();
        public bool WriteRow(ElementData value);
        public ElementData ReadRow(int id);
        public bool UpdateRow(ElementData value);
        public bool DeleteRow(int id);
    }
}
