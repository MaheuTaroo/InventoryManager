using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Utils
{
    internal interface IDataSource : IDisposable
    {
        public bool Connect(string link);
        public bool Disconnect();
        public bool writeRow(ElementData value);
        public ElementData readRow(int id);
        public bool updateRow(int id, ElementData value);
        public bool deleteRow(int id);
    }
}
