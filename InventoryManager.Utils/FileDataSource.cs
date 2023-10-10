using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Utils
{
    internal class ExcelDataSource : IDataSource
    {
        string location;
        bool isDisposed;

        public bool Connect(string link)
        {
            return false;
        }

        public ElementData readRow(int id)
        {
            if (isDisposed) throw new ObjectDisposedException("FileDataSource");

            return ElementData.EMPTY;
        }

        public bool Disconnect()
        {
            return false;
        }

        public void Dispose()
        {

        }

        public bool writeRow(ElementData value)
        {
            throw new NotImplementedException();
        }

        public bool updateRow(int id, ElementData value)
        {
            throw new NotImplementedException();
        }

        public bool deleteRow(int id)
        {
            throw new NotImplementedException();
        }
    }
}
