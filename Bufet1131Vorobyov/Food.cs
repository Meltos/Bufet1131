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
    public class Food : INotifyPropertyChanged
    {
        private string name;
        private int count;
        private int price;
        private string description;
        private string pathIMG;

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
        public int Count
        { 
            get => count;
            set
            {
                count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }
        public int Price
        { 
            get => price;
            set
            {
                price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        public string Description
        { 
            get => description;
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }
        public string PathIMG
        { 
            get => pathIMG;
            set
            {
                pathIMG = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PathIMG"));
            }
        }
        public ObservableCollection<Menu> Menus { get; set; } = new ObservableCollection<Menu>();
        public ObservableCollection<Provider> Providers { get; set; } = new ObservableCollection<Provider>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
