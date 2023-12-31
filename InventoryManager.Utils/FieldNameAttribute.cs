﻿namespace InventoryManager.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : Attribute
    {
        public readonly string Field;

        public FieldNameAttribute(string field)
        {
            Field = field;
        }
    }
}
