namespace InventoryManager.Utils
{
    public abstract class ElementData : IEquatable<ElementData>
    {
        public const ElementData EMPTY = null;

        [FieldName("ID")]
        [DataSetName("ID")]
        public int ID { get; }

        [FieldName("Designation")]
        [DataSetName("Designation")]
        public string Descriptor { get; }

        [FieldName("Units")]
        [DataSetName("Units in Item")]
        public int AmountPerItem { get; }

        [FieldName("Quantity")]
        [DataSetName("Quantity")]
        public int Quantity { get; }

        public virtual bool Equals(ElementData? other)
        {
            if (this == EMPTY && other == EMPTY) return true;

            return ID == other!.ID;
        }

        protected ElementData(int id, string name, int units, int quantity)
        {
            ID = id;
            Descriptor = name;
            AmountPerItem = units;
            Quantity = quantity;
        }
    }
}
