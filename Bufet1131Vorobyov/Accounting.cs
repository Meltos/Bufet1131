using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bufet1131Vorobyov
{
    [Serializable]
    public class Accounting
    {
        public int ID { get; set; }
        public Provider Provider { get; set; }
        public Food Food { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
    }
}
