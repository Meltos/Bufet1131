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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window, INotifyPropertyChanged
    {
        private Order selectedOrder;
        private Menu selectedMenu;
        private Provider selectedProvider;
        private DateTime dateTimeOrder;
        private int countOrder;
        private DB dB;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Menu> Menus { get; set; }
        public ObservableCollection<Food> FoodProviders { get; set; }
        public ObservableCollection<Food> FoodMenus { get; set; }
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                if (value != null)
                {
                    selectedOrder = value;
                    foreach (var food in FoodProviders)
                    {
                        if (food.ID == value.Food.ID)
                        {
                            Providers = food.Providers;
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Providers"));
                            break;
                        }
                    }
                    foreach (var food in FoodMenus)
                    {
                        if (food.ID == value.Food.ID)
                        {
                            Menus = food.Menus;
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Menus"));
                            break;
                        }
                    }
                    foreach (var provider in Providers)
                    {
                        if (provider.ID == value.Provider.ID)
                        {
                            SelectedProvider = provider;
                            break;
                        }
                    }
                    foreach (var menu in Menus)
                    {
                        if (menu.ID == value.Menu.ID)
                        {
                            SelectedMenu = menu;
                            break;
                        }
                    }
                    DateTimeOrder = value.DateTime.Date;
                    CountOrder = value.Count;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedOrder"));
                }
                
            }
        }
        public Menu SelectedMenu
        {
            get => selectedMenu;
            set
            {
                if (value != null)
                {
                    selectedMenu = value;
                    SelectedOrder.Menu = value;
                    EditOrder(SelectedOrder);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMenu"));
                }
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
                    SelectedOrder.Provider = value;
                    EditOrder(SelectedOrder);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProvider"));
                }
            }
        }
        public DateTime DateTimeOrder
        {
            get => dateTimeOrder;
            set
            {
                dateTimeOrder = value;
                SelectedOrder.DateTime = value.Date;
                EditOrder(SelectedOrder);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTimeOrder"));
            }
        }

        public int CountOrder
        {
            get => countOrder;
            set
            {
                countOrder = value;
                foreach (var food in FoodMenus)
                {
                    if (food.ID == SelectedOrder.Food.ID)
                    {
                        FoodSql foodSql = new FoodSql(dB);
                        int oldfoodcount = food.Count;
                        food.Count += SelectedOrder.Count;
                        food.Count -= value;
                        if (food.Count < 0)
                        {
                            MessageBox.Show("Количество блюд не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            food.Count = oldfoodcount;
                            return;
                        }
                        foodSql.EditFood(food);
                        break;
                    }
                }
                Не изменяетя итоговая стоимость!!!
                SelectedOrder.Count = value;
                SelectedOrder.Cost = SelectedOrder.Count * SelectedOrder.Food.Price;
                EditOrder(SelectedOrder);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountOrder"));
            }
        }

        private void EditOrder(Order selectedOrder)
        {
            OrderSql orderSql = new OrderSql(dB);
            orderSql.EditOrder(selectedOrder);
        }

        public AddOrder(DB dB)
        {
            InitializeComponent();
            Orders = new OrderSql(dB).GetData();
            FoodProviders = new FoodSql(dB).GetFoodProviders();
            FoodMenus = new FoodSql(dB).GetData();
            DataContext = this;
            this.dB = dB;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddNewOrder addNewOrder = new AddNewOrder(dB);
            addNewOrder.ShowDialog();
            if (addNewOrder.NewOrder == null)
                return;
            OrderSql orderSql = new OrderSql(dB);
            int id = orderSql.AddNewOrder(addNewOrder.NewOrder);
            addNewOrder.NewOrder.ID = id;
            Orders.Add(addNewOrder.NewOrder);
            FoodMenus = new FoodSql(dB).GetData();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FoodMenus"));
        }
    }
}
