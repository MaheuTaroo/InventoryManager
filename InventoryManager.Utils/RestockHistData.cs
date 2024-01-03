using MySql.Data.MySqlClient;

namespace InventoryManager.Utils
{
    public class RestockHistData : ElementData
    {
        [DataSetName("Date of Sale")]
        public DateTime BuyDate { get; }

        [DataSetName("Total Price")]
        public decimal TotalPrice { get; }

        [DataSetName("Items")]
        public List<SaleElementData> SaleItems { get; }

        public RestockHistData(int id, DateTime saleDate) :
               base(id)
        {
            BuyDate = saleDate;
            using (MySqlCommand cmd = new MySqlCommand("select * from sale_items where SaleID = @id", UtilData.Source!.Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    SaleItems = new List<SaleElementData>();
                    while (reader.Read())
                    {
                        SaleItems.Add(new SaleElementData(reader.GetInt32(1), reader.GetInt32(2), reader.GetDecimal(3)));
                    }
                }
            }
            TotalPrice = SaleItems.Sum(el => el.TotalPrice);
        }
    }
}
