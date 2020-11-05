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
    /// Логика взаимодействия для AddNewAccounting.xaml
    /// </summary>
    public partial class AddNewAccounting : Window, INotifyPropertyChanged
    {
        private DB dB;
        private Provider selectedProvider;

        public event PropertyChangedEventHandler PropertyChanged;

        public Accounting NewAccounting { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Food> ProviderFoodsAcc { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public Provider SelectedProvider
        {
            get => selectedProvider;
            set
            {
                selectedProvider = value;
                if (value.Foods.Count <= 0)
                {
                    label1.Visibility = Visibility.Collapsed;
                    comboBox1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    label1.Visibility = Visibility.Visible;
                    comboBox1.Visibility = Visibility.Visible;
                    ProviderFoodsAcc = value.Foods;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProviderFoodsAcc"));
                }
            }
        }
    
        public Food SelectedProvidersFood { get; set; }
        public DateTime DateTimeNewAcc { get; set; }
        public int CountNewAcc { get; set; }

        public AddNewAccounting(DB dB)
        {
            InitializeComponent();
            Providers = new ProviderSql(dB).GetProvidersFood();
            Foods = new FoodSql(dB).GetData();
            DateTimeNewAcc = DateTime.Now;
            DataContext = this;
            this.dB = dB;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProvider == null)
            {
                MessageBox.Show("Не выбран поставщик","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedProvidersFood == null)
            {
                MessageBox.Show("Не выбрано блюдо", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (CountNewAcc < 0)
            {
                MessageBox.Show("Введите количество блюд", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                NewAccounting = new Accounting();
                NewAccounting.DateTime = DateTimeNewAcc;
                NewAccounting.Provider = SelectedProvider;
                NewAccounting.Food = SelectedProvidersFood;
                NewAccounting.Count = CountNewAcc;
                foreach (var food in Foods)
                {
                    if (food.ID == NewAccounting.Food.ID)
                    {
                        FoodSql foodSql = new FoodSql(dB);
                        food.Count += NewAccounting.Count;
                        foodSql.EditFood(food);
                    }
                }
                Close();
            }
        }
    }
}
