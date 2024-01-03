using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace InventoryManager.Utils
{
    public static class UnitTypes
    {
        public readonly static ReadOnlyCollection<UnitType> types;

        static UnitTypes()
        {
            using (MySqlCommand cmd = new MySqlCommand("select * from unit_types order by ID ascending;", UtilData.Source!.Connection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                List<UnitType> tmp = new List<UnitType>();
                while (reader.Read())
                    tmp.Add(new UnitType(reader.GetInt32(0), reader.GetString(1)));

                types = tmp.AsReadOnly();
            }
        }

        public static UnitType From(int id) => id > types.Count - 1 ? throw new ArgumentException("Invalid ID") 
                                                                    : types[id];
    }

    public record UnitType
    {
        public static readonly UnitType? NONE = null;

        [DataSetName("ID")]
        public int ID { get; }

        [DataSetName("Type")]
        public string Designation { get; }

        public UnitType(int id, string type)
        {
            ID = id;
            Designation = type;
        }
    }
}
