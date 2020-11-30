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
    /// Логика взаимодействия для AddProvider.xaml
    /// </summary>
    public partial class AddProvider : Window, INotifyPropertyChanged
    {
        private Provider selectedProvider;
        private string name;
        private string phoneProvider;
        private string addressProvider;
        private string mailProvider;
        private readonly DB dB;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Food> ProviderFoods { get; set; }
        public Food SelectedFood { get; set; }
        public Food SelectedFoodProvider { get; set; }
        public Provider SelectedProvider
        {
            get => selectedProvider;
            set
            {
                if (value != null)
                {
                    selectedProvider = value;
                    Foods = value.Foods;
                    ProviderFoods = GetNoProviderFoods(value);
                    NameProvider = selectedProvider.Name;
                    PhoneProvider = selectedProvider.Phone;
                    AddressProvider = selectedProvider.Address;
                    MailProvider = selectedProvider.Mail;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProviderFoods"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                }
            }
        }
        public string NameProvider
        {
            get => name;
            set
            {
                name = value;
                SelectedProvider.Name = value;
                EditProvider(SelectedProvider);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameProvider"));
            }
        }
        public string PhoneProvider
        {
            get => phoneProvider;
            set
            {
                phoneProvider = value;
                SelectedProvider.Phone = value;
                EditProvider(SelectedProvider);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhoneProvider"));
            }
        }
        public string AddressProvider
        {
            get => addressProvider;
            set
            {
                addressProvider = value;
                SelectedProvider.Address = value;
                EditProvider(SelectedProvider);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AddressProvider"));
            }
        }
        public string MailProvider
        {
            get => mailProvider;
            set
            {
                mailProvider = value;
                SelectedProvider.Mail = value;
                EditProvider(SelectedProvider);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MailProvider"));
            }
        }

        private void EditProvider(Provider selectedProvider)
        {
            ProviderSql providerSql = new ProviderSql(dB);
            providerSql.EditProvider(selectedProvider);
        }

        private ObservableCollection<Food> GetNoProviderFoods(Provider value)
        {
            return new FoodSql(new DB()).GetNoProviderFoods(value);
        }

        public AddProvider(DB dB)
        {
            InitializeComponent();
            Providers = new ProviderSql(dB).GetData();
            DataContext = this;
            this.dB = dB;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Provider newprovider = new Provider { Name = "", Mail = "", Address = "", Phone = ""};
            ProviderSql providerSql = new ProviderSql(dB);
            int id = providerSql.AddNewProvider(newprovider);
            newprovider.ID = id;
            Providers.Add(newprovider);
            MessageBox.Show("Новый поставщик создан.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedProvider == null)
                return;
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить поставщика?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ProviderSql providerSql = new ProviderSql(dB);
                providerSql.RemoveProvider(SelectedProvider);
                Providers.Remove(SelectedProvider);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFood == null || SelectedProvider.Foods.Contains(SelectedFood))
                return;
            SelectedProvider.Foods.Add(SelectedFood);
            ProviderSql providerSql = new ProviderSql(dB);
            providerSql.AddFoodProvider(SelectedFood, SelectedProvider);
            ChangeSelectedProvider();
        }

        private void ChangeSelectedProvider()
        {
            ProviderFoods = GetNoProviderFoods(SelectedProvider);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProviderFoods"));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectedFoodProvider == null)
                return;
            ProviderSql providerSql = new ProviderSql(dB);
            providerSql.RemoveFoodProvider(SelectedFoodProvider, SelectedProvider);
            SelectedProvider.Foods.Remove(SelectedFoodProvider);
            ChangeSelectedProvider();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            AccountingProvider accountingProvider = new AccountingProvider(dB);
            accountingProvider.ShowDialog();
        }
    }
}
