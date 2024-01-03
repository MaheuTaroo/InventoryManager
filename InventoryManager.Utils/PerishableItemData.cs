namespace InventoryManager.Utils
{
    public class PerishableItemData : ElementData
    {
        [DataSetName("Designation")]
        public string Designation { get; }

        [DataSetName("Units in Item")]
        public int Units { get; }

        [DataSetName("Unit Type")]
        public UnitType UnitType { get; }

        [DataSetName("Quantity")]
        public int Quantity { get; }

        [DataSetName("Expiration Date")]
        public DateTime ExpiresOn { get; }

        public PerishableItemData(int id, string name, int units, UnitType type, int quantity, DateTime expDate) : 
               base(id)
        {
            Designation = name;
            Units = units;
            UnitType = type;
            Quantity = quantity;
            ExpiresOn = expDate;
        }
    }
}
