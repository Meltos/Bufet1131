using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бюро_находок_и_забытых_вещей
{
    [Serializable]
    public class Advertisement
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public Category Category { get; set; }
        public SubCategory Subcategory { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }
        public DateTime Time { get; set; }
        public string Discovered { get; set; }
        public string ContactPerson { get; set; }
    }
}
