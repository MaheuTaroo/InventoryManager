namespace InventoryManager.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : Attribute
    {
        public string Field;

        public FieldNameAttribute(string field)
        {
            Field = field;
        }
    }
}
