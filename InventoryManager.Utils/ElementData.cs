using MySql.Data.MySqlClient;

namespace InventoryManager.Utils
{
    public abstract class ElementData : IEquatable<ElementData>
    {
        public const ElementData EMPTY = null;

        [DataSetName("ID")]
        public int ID { get; }

        protected ElementData(int id)
        {
            ID = id;
        }

        public static ElementData FromReader(string table, MySqlDataReader reader) 
        {
            return table switch
            {
                "items" => new PerishableItemData(reader.GetInt32(0),
                                                  reader.GetString(1),
                                                  int.Parse(reader.GetString(2)),
                                                  UnitTypes.From(reader.GetInt32(3)),
                                                  reader.GetInt32(4),
                                                  reader.GetMySqlDateTime(5).GetDateTime()),
                "restock_hist" => new RestockHistData(reader.GetInt32(0),
                                                      reader.GetDateTime(1)),
                _ => EMPTY
            };
        }

        public virtual bool Equals(ElementData? other)
        {
            if (other == null) return this == null;

            return ID == other.ID;
        }
    }
}
