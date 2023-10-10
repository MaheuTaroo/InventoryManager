namespace InventoryManager.Utils
{
    internal abstract class ElementData : IDisposable, IEquatable<ElementData>
    {
        public const ElementData EMPTY = null;
        protected bool isDisposed;

        public int ID { get; } 
        public string Descriptor { get; }
        public int Units { get; }
        public int Quantity { get; }
        public ExpirationType ExpirationType { get; }
        public DateTime ExpirationDate { get; }

        public abstract void Dispose();
        public virtual bool Equals(ElementData? other)
        {
            if (this == EMPTY && other == EMPTY) return true;

            return ID == other.ID &&
                   Descriptor.Equals(other.Descriptor) && 
                   Units == other.Units &&
                   Quantity == other.Quantity &&
                   ExpirationType == other.ExpirationType && 
                   ExpirationDate.Equals(other.ExpirationDate);
        }
    }
}
