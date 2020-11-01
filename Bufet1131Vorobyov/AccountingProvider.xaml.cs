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
    /// Логика взаимодействия для AccountingProvider.xaml
    /// </summary>
    public partial class AccountingProvider : Window, INotifyPropertyChanged
    {
        private Accounting selectedAccounting;
        private DateTime dateTimeAcc;
        private int countAcc;
        private Provider selectedProvider;
        private Food selectedFood;
        private DB dB;

        public ObservableCollection<Accounting> Accountings { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Food> ProviderFoods { get; set; }
        public Accounting SelectedAccounting
        {
            get => selectedAccounting;
            set
            {
                selectedAccounting = value;
            }
        }
        public DateTime DateTimeAcc
        {
            get => dateTimeAcc;
            set
            {
                dateTimeAcc = value;
                SelectedAccounting.DateTime = value;
                EditAccounting(SelectedAccounting);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTimeAcc"));
            }
        }

        private void EditAccounting(Accounting selectedAccounting)
        {
            AccountingSql accountingSql = new AccountingSql(dB);
            accountingSql.EditAccounting(selectedAccounting);
        }

        public int CountAcc
        {
            get => countAcc;
            set
            {
                countAcc = value;
                SelectedAccounting.Count = value;
                EditAccounting(SelectedAccounting);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountAcc"));
            }
        }
        public Provider SelectedProvider
        {
            get => selectedProvider;
            set
            {
                selectedProvider = value;
                SelectedAccounting.Provider = value;
                EditAccounting(SelectedAccounting);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
            }
        }
        public Food SelectedFood
        {
            get => selectedFood;
            set
            {
                selectedFood = value;
                SelectedAccounting.Food = value;
                EditAccounting(SelectedAccounting);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFood"));
            }
        }

        public AccountingProvider(DB dB)
        {
            InitializeComponent();
            Accountings = new AccountingSql(dB).GetData();
            Providers = new ProviderSql(dB).GetProvidersFood();
            DataContext = this;
            this.dB = dB;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
