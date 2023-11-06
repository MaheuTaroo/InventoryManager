namespace InventoryManager.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataSetNameAttribute : Attribute
    {
        public readonly string Field;

        public DataSetNameAttribute(string field)
        {
            Field = field;
        }
    }
}
