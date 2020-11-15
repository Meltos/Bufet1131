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
        private DB dB;

        public ObservableCollection<Accounting> Accountings { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public Accounting SelectedAccounting
        {
            get => selectedAccounting;
            set
            {
                if (value != null)
                {
                    
                    selectedAccounting = value;
                    foreach (var food in Foods)
                    {
                        if (food.ID == value.Food.ID)
                        {
                            if (food.Providers.Count == 0)
                            {
                                ObservableCollection<Provider> providersnull = new ObservableCollection<Provider>();
                                providersnull.Add(value.Provider);
                                Providers = providersnull;
                                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Providers"));
                            }
                            Providers = food.Providers;
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Providers"));
                            foreach (var provider in food.Providers)
                            {
                                if (provider.ID == value.Provider.ID)
                                {
                                    SelectedProvider = provider;
                                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                                }
                            }
                        }
                    }
                    DateTimeAcc = value.DateTime.Date;
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
                dateTimeAcc = value.Date;
                SelectedAccounting.DateTime = value.Date;
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
                foreach (var food in Foods)
                {
                    if (food.ID == SelectedAccounting.Food.ID)
                    {
                        FoodSql foodSql = new FoodSql(dB);
                        int oldfoodcount = food.Count;
                        food.Count -= SelectedAccounting.Count;
                        food.Count += value;
                        if (food.Count < 0)
                        {
                            MessageBox.Show("Количество блюд не может быть отрицательным","Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                            food.Count = oldfoodcount;
                            return;
                        }
                        foodSql.EditFood(food);
                    }
                }
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
                if (value != null)
                {
                    selectedProvider = value;
                    SelectedAccounting.Provider = value;
                    EditAccounting(SelectedAccounting);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                }
            }
        }

        public AccountingProvider(DB dB)
        {
            InitializeComponent();
            Accountings = new AccountingSql(dB).GetData();
            Foods = new FoodSql(dB).GetFoodNullProviders();
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
            Foods = new FoodSql(dB).GetFoodProviders();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedAccounting == null)
                return;
            MessageBoxResult result = MessageBox.Show("Вы хотите удалить поставку, сохранив изменения количетсва еды?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNoCancel,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AccountingSql accountingSql = new AccountingSql(dB);
                accountingSql.RemoveAccounting(SelectedAccounting);
                Accountings.Remove(SelectedAccounting);
            }
            else if (result == MessageBoxResult.No)
            {
                foreach (var food in Foods)
                {
                    if (food.ID == SelectedAccounting.Food.ID)
                    {
                        FoodSql foodSql = new FoodSql(dB);
                        int oldfoodcount = food.Count;
                        food.Count -= SelectedAccounting.Count;
                        if (food.Count < 0)
                        {
                            MessageBox.Show("Количество блюд не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            food.Count = oldfoodcount;
                            return;
                        }
                        foodSql.EditFood(food);
                    }
                }
                AccountingSql accountingSql = new AccountingSql(dB);
                accountingSql.RemoveAccounting(SelectedAccounting);
                Accountings.Remove(SelectedAccounting);
            }
        }
    }
}
