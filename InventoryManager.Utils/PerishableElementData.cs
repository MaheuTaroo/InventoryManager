namespace InventoryManager.Utils
{
    public class PerishableElementData : ElementData
    {
        [FieldName("ExpiresOn")]
        [DataSetName("Expiration Date")]
        public DateTime Expiration { get; }

        public PerishableElementData(int id, string name, int units, int quantity, DateTime expDate) : 
               base(id, name, units, quantity)
        {
            Expiration = expDate;
        }
    }
}
