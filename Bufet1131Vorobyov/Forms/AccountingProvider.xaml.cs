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
        private DateTime secondFilterDate;
        private DateTime firstFilterDate;
        private Food selectedFilterFood;
        private Provider selectedFilterProvider;

        public ObservableCollection<Accounting> AllAccountings { get; set; }
        public ObservableCollection<Food> FoodsFilter { get; set; }
        public ObservableCollection<Provider> ProvidersFilter { get; set; }
        public ObservableCollection<Accounting> Accountings { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public Accounting SelectedAccounting
        {
            get => selectedAccounting;
            set
            {
                if (value != null  )
                {
                    selectedAccounting = value;
                    if (value.ID != 0)
                    {
                        foreach (var food in Foods)
                        {
                            if (food.ID == value.Food.ID)
                            {

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
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedAccounting"));
                }
            }
        }
        public DateTime DateTimeAcc
        {
            get => dateTimeAcc;
            set
            {
                if (SelectedAccounting.ID != 0)
                {
                    dateTimeAcc = value.Date;
                    SelectedAccounting.DateTime = value.Date;
                    EditAccounting(SelectedAccounting);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTimeAcc"));
                }
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
                if (SelectedAccounting.ID != 0)
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
                                MessageBox.Show("Количество блюд не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        }
        public Provider SelectedProvider
        {
            get => selectedProvider;
            set
            {
                if (value != null && SelectedAccounting.ID != 0)
                {
                    selectedProvider = value;
                    SelectedAccounting.Provider = value;
                    EditAccounting(SelectedAccounting);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                }
            }
        }
        public Provider SelectedFilterProvider
        {
            get => selectedFilterProvider;
            set
            {
                selectedFilterProvider = value;
                label1.Visibility = Visibility.Collapsed;
                comboBox1.Visibility = Visibility.Collapsed;
                FoodsFilter = value.Foods;
                if (value.ID != 0)
                {
                    label1.Visibility = Visibility.Visible;
                    comboBox1.Visibility = Visibility.Visible;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FoodsFilter"));
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFilterProvider"));
            }
        }

        private void FilterStart()
        {
            ObservableCollection<Accounting> accountingsfilter = new ObservableCollection<Accounting>();
            if (SelectedFilterProvider == null)
            {
                foreach (var acc in AllAccountings)
                {
                    if (acc.DateTime.Date >= FirstFilterDate.Date && acc.DateTime.Date <= SecondFilterDate.Date)
                    {
                        accountingsfilter.Add(acc);
                    }
                }
            }
            else if (SelectedFilterFood != null && FoodsFilter.Contains(SelectedFilterFood))
            {
                foreach (var acc in AllAccountings)
                {
                    if (acc.Provider.Name.Contains(SelectedFilterProvider?.Name) && acc.Food.Name.Contains(SelectedFilterFood?.Name) && acc.DateTime.Date >= FirstFilterDate.Date && acc.DateTime.Date <= SecondFilterDate.Date)
                    {
                        accountingsfilter.Add(acc);
                    }
                }
            }
            else
            {
                foreach (var acc in AllAccountings)
                {
                    if (acc.Provider.Name.Contains(SelectedFilterProvider?.Name) && acc.DateTime.Date >= FirstFilterDate.Date && acc.DateTime.Date <= SecondFilterDate.Date)
                    {
                        accountingsfilter.Add(acc);
                    }
                }
            }
            Accountings = accountingsfilter;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Accountings"));
        }

        public Food SelectedFilterFood
        {
            get => selectedFilterFood;
            set
            {
                selectedFilterFood = value;
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFilterFood"));
            }
        }
        public DateTime FirstFilterDate
        {
            get => firstFilterDate;
            set
            {
                firstFilterDate = value;
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstFilterDate"));
            }
        }
        public DateTime SecondFilterDate
        {
            get => secondFilterDate;
            set
            {
                secondFilterDate = value;
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SecondFilterDate"));
            }
        }

        public AccountingProvider(DB dB)
        {
            InitializeComponent();
            Accountings = new AccountingSql(dB).GetData();
            AllAccountings = new AccountingSql(dB).GetData();
            Foods = new FoodSql(dB).GetFoodNullProviders();
            ProvidersFilter = new ProviderSql(dB).GetNullProviderFood();
            FirstFilterDate = DateTime.MinValue;
            SecondFilterDate = DateTime.Now;
            DataContext = this;
            this.dB = dB;
            label1.Visibility = Visibility.Collapsed;
            comboBox1.Visibility = Visibility.Collapsed;
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
            AllAccountings.Add(addNewAccounting.NewAccounting);
            Foods = new FoodSql(dB).GetFoodProviders();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
            FilterStart();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedAccounting == null)
                return;
            MessageBoxResult result = MessageBox.Show("Вы хотите удалить поставку, сохранив изменения количества блюда?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNoCancel,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AccountingSql accountingSql = new AccountingSql(dB);
                accountingSql.RemoveAccounting(SelectedAccounting);
                Accountings.Remove(SelectedAccounting);
                AllAccountings.Remove(SelectedAccounting);
                Accounting nullaccounting = new Accounting { ID = 0  };
                SelectedAccounting = nullaccounting;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedAccounting"));
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
                AllAccountings.Remove(SelectedAccounting);
                Accounting nullaccounting = new Accounting { ID = 0 };
                SelectedAccounting = nullaccounting;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedAccounting"));
            }
        }
    }
}
