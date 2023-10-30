using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Utils
{
    public interface IDataSource
    {
        public bool Connect(string link);
        public bool Disconnect();
        public bool WriteRow(ElementData value);
        public ElementData ReadRow(int id);
        public bool UpdateRow(int id, ElementData value);
        public bool DeleteRow(int id);
    }
}
