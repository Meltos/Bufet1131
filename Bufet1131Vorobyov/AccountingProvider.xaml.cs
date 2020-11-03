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
                if (value != null)
                {
                    selectedAccounting = value;
                    SelectedProvider = value.Provider;
                    SelectedFood = value.Food;
                    DateTimeAcc = value.DateTime;
                    CountAcc = value.Count;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedAccounting"));
                }
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
                if (value.Foods.Count <= 0)
                {
                    label1.Visibility = Visibility.Collapsed;
                    comboBox1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    label1.Visibility = Visibility.Visible;
                    comboBox1.Visibility = Visibility.Visible;
                    ProviderFoods = value.Foods;
                    SelectedAccounting.Provider = value;
                    EditAccounting(SelectedAccounting);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                }
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
            AddNewAccounting addNewAccounting = new AddNewAccounting(dB);
            addNewAccounting.ShowDialog();
            if (addNewAccounting.NewAccounting == null)
                return;
            AccountingSql accountingSql = new AccountingSql(dB);
            int id = accountingSql.AddNewAccounting(addNewAccounting.NewAccounting);
            addNewAccounting.NewAccounting.ID = id;
            Accountings.Add(addNewAccounting.NewAccounting);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedAccounting == null)
                return;
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить поставку?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AccountingSql accountingSql = new AccountingSql(dB);
                accountingSql.RemoveAccounting(SelectedAccounting);
                Accountings.Remove(SelectedAccounting);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
