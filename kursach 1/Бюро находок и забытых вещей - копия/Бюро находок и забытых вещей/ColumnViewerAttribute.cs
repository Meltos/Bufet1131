using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бюро_находок_и_забытых_вещей
{
    internal class ColumnViewerAttribute : Attribute
    {
        public string ColumnName;

        public ColumnViewerAttribute(string v)
        {
            this.ColumnName = v;
        }
    }
}
