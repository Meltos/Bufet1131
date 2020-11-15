using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бюро_находок_и_забытых_вещей
{
    [Serializable]
    public class City
    {
        [ColumnViewer("City")]
        public string NameCity { get; set; }
        public Country Country { get; set; }
    }
}
