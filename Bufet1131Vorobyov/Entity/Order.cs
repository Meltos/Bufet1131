using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bufet1131Vorobyov
{
    public class Order : INotifyPropertyChanged
    {
        private DateTime dateTime;
        private int count;
        private Menu menu;
        private Provider provider;
        private int cost;

        public int ID { get; set; }
        public int Cost 
        { 
            get => cost;
            set
            {
                cost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            }
        }
        public Food Food { get; set; }
        public DateTime DateTime
        {
            get => dateTime;
            set
            {
                dateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }
        public Menu Menu
        {
            get => menu;
            set
            {
                menu = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Menu"));
            }
        }
        public Provider Provider
        {
            get => provider;
            set
            {
                provider = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Provider"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
