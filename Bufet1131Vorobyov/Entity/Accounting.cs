using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bufet1131Vorobyov
{
    [Serializable]
    public class Accounting : INotifyPropertyChanged
    {
        private Provider provider;
        private DateTime dateTime;
        private int count;

        public int ID { get; set; }
        public Provider Provider
        {
            get => provider;
            set
            {
                provider = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Provider"));
            }
        }
        public Food Food { get; set; }
        public DateTime DateTime
        {
            get => dateTime;
            set
            {
                dateTime = value.Date;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));
            }
        }
        public int Count
        {
            get => count; set
            {
                count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
