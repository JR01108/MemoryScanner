using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryScanner
{
    public class FileStruct
    {
        public string name;
        public double size;
        public double percent;
        public bool isCatalog;

        public FileStruct(string _name, double _size, double _percent, bool _isCatalog)
        {
            name = _name;
            size = _size;
            percent = _percent;
            this.isCatalog = _isCatalog;
        }
    }
}
