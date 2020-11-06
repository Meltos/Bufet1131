using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bufet1131Vorobyov
{
    [Serializable]
    public class Provider : INotifyPropertyChanged
    {
        private string name;
        private string phone;
        private string address;
        private string mail;

        public int ID { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Phone"));
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Address"));
            }
        }
        public string Mail
        {
            get => mail;
            set
            {
                mail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mail"));
            }
        }
        public int Status { get; set; }
        public ObservableCollection<Food> Foods { get; set; } = new ObservableCollection<Food>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
