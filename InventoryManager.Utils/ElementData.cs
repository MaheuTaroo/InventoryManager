namespace InventoryManager.Utils
{
    public abstract class ElementData : IEquatable<ElementData>
    {
        public const ElementData EMPTY = null;

        [FieldName("ID")]
        public int ID { get; }

        [FieldName("Designation")]
        public string Descriptor { get; }

        [FieldName("UnitsPerItem")]
        public int Units { get; }

        [FieldName("Quantity")]
        public int Quantity { get; }

        public virtual bool Equals(ElementData? other)
        {
            if (this == EMPTY && other == EMPTY) return true;

            return ID == other.ID;
        }

        protected ElementData(int id, string name,  int units, int quantity)
        {
            ID = id;
            Descriptor = name;
            Units = units;
            Quantity = quantity;
        }
    }
}
