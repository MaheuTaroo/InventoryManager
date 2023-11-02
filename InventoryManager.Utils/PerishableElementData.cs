namespace InventoryManager.Utils
{
    public class PerishableElementData : ElementData
    {
        [FieldName("ExpirationDate")]
        public DateTime ExpirationDate { get; }

        public PerishableElementData(int id, string name, int units, int quantity, DateTime expDate) : 
               base(id, name, units, quantity)
        {
            ExpirationDate = expDate;
        }
    }
}
