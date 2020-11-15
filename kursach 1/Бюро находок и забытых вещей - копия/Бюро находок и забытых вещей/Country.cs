using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бюро_находок_и_забытых_вещей
{
    [Serializable]
    public class Country
    {
        [ColumnViewer("Country")]
        public string NameCountry { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}
