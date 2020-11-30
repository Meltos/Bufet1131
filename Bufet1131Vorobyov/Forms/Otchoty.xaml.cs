using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bufet1131Vorobyov
{
    /// <summary>
    /// Логика взаимодействия для Otchoty.xaml
    /// </summary>
    public partial class Otchoty : Window, INotifyPropertyChanged
    {
        private DB dB;
        private DateTime selectedStart;
        private DateTime selectedEnd;
        private Food selectedFoodProvider;
        private Provider selectedProvider;
        private Provider selectedProviderFood;
        private Food selectedFoodMenu;
        private Menu selectedMenu;

        public event PropertyChangedEventHandler PropertyChanged;
        public int CountAccounting { get; set; }
        public int CountOrder { get; set; }
        public ObservableCollection<Provider> FoodProviders { get; set; }
        public ObservableCollection<Food> MenuFoods { get; set; }
        public ObservableCollection<Menu> Menus { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Food> ProviderFoods { get; set; }
        public ObservableCollection<Accounting> Accountings { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public Menu SelectedMenu
        {
            get => selectedMenu;
            set
            {
                selectedMenu = value;
                label22.Visibility = Visibility.Collapsed;
                comboBox22.Visibility = Visibility.Collapsed;
                label23.Visibility = Visibility.Collapsed;
                comboBox23.Visibility = Visibility.Collapsed;
                ProviderFoods = null;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuFoods"));
                Food nullfood = new Food();
                SelectedFoodProvider = nullfood;
                CountOrder = 0;
                CountAccounting = 0;
                if (value.Foods.Count > 0)
                {
                    ProviderFoods = value.Foods;
                    label22.Visibility = Visibility.Visible;
                    comboBox22.Visibility = Visibility.Visible;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuFoods"));
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMenu"));
            }
        }
        public Food SelectedFoodMenu
        {
            get => selectedFoodMenu;
            set
            {
                selectedFoodMenu = value;
            }
        }
        public Provider SelectedProviderFood
        {
            get => selectedProviderFood;
            set
            {
                selectedProviderFood = value;
            }
        }
        public Provider SelectedProvider
        {
            get => selectedProvider;
            set
            {
                if (value != null)
                {
                    selectedProvider = value;
                    label1.Visibility = Visibility.Collapsed;
                    comboBox1.Visibility = Visibility.Collapsed;
                    ProviderFoods = null;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProviderFoods"));
                    Food nullfood = new Food();
                    SelectedFoodProvider = nullfood;
                    CountOrder = 0;
                    CountAccounting = 0;
                    if (value.Foods.Count > 0)
                    {
                        ProviderFoods = value.Foods;
                        label1.Visibility = Visibility.Visible;
                        comboBox1.Visibility = Visibility.Visible;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProviderFoods"));
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                }
            }
        }
        public Food SelectedFoodProvider
        {
            get => selectedFoodProvider;
            set
            {
                if (value != null)
                {
                    selectedFoodProvider = value;
                    ViewOtchyot();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFoodProvider"));
                }
            }
        }
        public DateTime SelectedStart
        {
            get => selectedStart;
            set
            {
                selectedStart = value;
                ViewOtchyot();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedStart"));
            }
        }
        public DateTime SelectedEnd
        {
            get => selectedEnd;
            set
            {
                selectedEnd = value;
                ViewOtchyot();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedEnd"));
            }
        }

        private void ViewOtchyot()
        {
            if (SelectedFoodProvider == null || SelectedProvider == null)
                return;
            CountOrder = 0;
            CountAccounting = 0;
            foreach (var acc in Accountings)
            {
                if (acc.Food.ID == SelectedFoodProvider.ID && acc.Provider.ID == SelectedProvider.ID && acc.DateTime >= SelectedStart && acc.DateTime <= SelectedEnd)
                {
                    CountAccounting += acc.Count;
                }
            }
            foreach (var order in Orders)
            {
                if (order.Food.ID == SelectedFoodProvider.ID && order.Provider.ID == SelectedProvider.ID && order.DateTime >= SelectedStart && order.DateTime <= SelectedEnd)
                {
                    CountOrder += order.Count;
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountAccounting"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountOrder"));
        }

        public Otchoty(DB dB)
        {
            InitializeComponent();
            Providers = new ProviderSql(dB).GetData();
            Accountings = new AccountingSql(dB).GetData();
            Orders = new OrderSql(dB).GetData();
            SelectedEnd = DateTime.Now;
            DataContext = this;
            this.dB = dB;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
