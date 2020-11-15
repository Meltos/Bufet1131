using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginatorSample
{
    public class Human : IViewRow
    {
        public string FName { get; set; }
        public string LName { get; set; }

        public string GetColumn(int i)
        {
            switch (i)
            {
                case 0: return LName;
                case 1: return FName;
            }
            return null;
        }
    }
}
