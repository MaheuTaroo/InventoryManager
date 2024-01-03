using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Utils
{
    public static class UtilData
    {
        private static DBDataSource? _source = null;
        public static DBDataSource? Source
        { 
            get => _source; 
            set
            {
                if (value != null && value.CanConnect())
                    _source = value;
            }
        } 
    }
}
