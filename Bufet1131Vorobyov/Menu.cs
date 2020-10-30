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
    public class Menu : INotifyPropertyChanged
    {
        private string name;

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
        public int Status { get; set; }
        public ObservableCollection<Food> Foods { get; set; } = new ObservableCollection<Food>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
