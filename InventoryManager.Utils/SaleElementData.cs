using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;

namespace InventoryManager.Utils
{
    public class SaleElementData : IEquatable<SaleElementData>
    {
        [DataSetName("Item ID")]
        public int ItemID { get; }

        [DataSetName("Amount")]
        public int Amount { get; }

        [DataSetName("Total Price")]
        public decimal TotalPrice { get; }

        public SaleElementData(int item, int amount, decimal price)
        {
            ItemID = item;
            Amount = amount;
            TotalPrice = price;
        }

        public bool Equals(SaleElementData? other) => 
            other != null && ItemID == other.ItemID && Amount == other.Amount && TotalPrice == other.TotalPrice;
    }
}
