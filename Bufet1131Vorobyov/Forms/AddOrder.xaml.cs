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
        private Menu selectedFilterMenu;
        private Food selectedFilterFood;
        private Provider selectedFilterProvider;
        private DateTime firstFilterDate;
        private DateTime secondFilterDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Order> AllOrders { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<Menu> MenusFilter { get; set; }
        public ObservableCollection<Food> FoodsFilter { get; set; }
        public ObservableCollection<Provider> ProvidersFilter { get; set; }
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
                    if (value.ID != 0)
                    {
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
                        if (Providers.Count < 1)
                        {
                            Provider nprovider = new Provider { ID = 0, Status = -1, Name = "" };
                            Providers.Add(nprovider);
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Providers"));
                        }
                        else
                        {
                            foreach (var provider in Providers)
                            {
                                if (provider.ID == value.Provider.ID)
                                {
                                    SelectedProvider = provider;
                                    break;
                                }
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
                    }
                    
                    
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedOrder"));
                }
            }
        }
        public Menu SelectedMenu
        {
            get => selectedMenu;
            set
            {
                if (value != null && SelectedOrder.ID != 0)
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
                if (value != null && SelectedOrder.ID != 0)
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
                if (SelectedOrder.ID != 0)
                {
                    dateTimeOrder = value;
                    SelectedOrder.DateTime = value.Date;
                    EditOrder(SelectedOrder);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTimeOrder"));
                }
            }
        }

        public int CountOrder
        {
            get => countOrder;
            set
            {
                if (SelectedOrder.ID != 0)
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
                    SelectedOrder.Count = value;
                    SelectedOrder.Cost = SelectedOrder.Count * SelectedOrder.Food.Price;
                    EditOrder(SelectedOrder);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountOrder"));
                }
            }
        }
        public Menu SelectedFilterMenu
        {
            get => selectedFilterMenu;
            set
            {
                selectedFilterMenu = value;
                label1.Visibility = Visibility.Collapsed;
                comboBox1.Visibility = Visibility.Collapsed;
                FoodsFilter = null;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FoodsFilter"));
                FoodsFilter = value.Foods;
                if (value.ID != 0)
                {
                    label1.Visibility = Visibility.Visible;
                    comboBox1.Visibility = Visibility.Visible;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FoodsFilter"));
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFilterMenu"));
            }
        }
        public Food SelectedFilterFood
        {
            get => selectedFilterFood;
            set
            {
                selectedFilterFood = value;
                label2.Visibility = Visibility.Collapsed;
                comboBox2.Visibility = Visibility.Collapsed;
                if (value != null)
                {
                    foreach (var food in Foods)
                    {
                        if (value.ID == food.ID)
                        {
                            value.Providers = food.Providers;
                            break;
                        }
                    }
                    ProvidersFilter = value.Providers;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProvidersFilter"));
                }
                else
                {
                    ProvidersFilter = null;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProvidersFilter"));
                    FilterStart();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFilterFood"));
                    return;
                }

                if (value.ID != 0 && value.Providers.Count > 1)
                {
                    label2.Visibility = Visibility.Visible;
                    comboBox2.Visibility = Visibility.Visible;
                }
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFilterFood"));
            }
        }
        public Provider SelectedFilterProvider
        {
            get => selectedFilterProvider;
            set
            {
                selectedFilterProvider = value;
                FilterStart();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFilterProvider"));
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

        private void FilterStart()
        {
            ObservableCollection<Order> orderfilter = new ObservableCollection<Order>();
            if (SelectedFilterMenu == null)
            {
                foreach (var order in AllOrders)
                {
                    if (order.DateTime.Date >= FirstFilterDate.Date && order.DateTime.Date <= SecondFilterDate.Date)
                    {
                        orderfilter.Add(order);
                    }
                }
            }
            else if (SelectedFilterFood != null && FoodsFilter.Contains(SelectedFilterFood) && SelectedFilterProvider == null)
            {
                foreach (var order in AllOrders)
                {
                    if (order.Menu.Name.Contains(SelectedFilterMenu?.Name) && order.Food.Name.Contains(SelectedFilterFood?.Name) && order.DateTime.Date >= FirstFilterDate.Date && order.DateTime.Date <= SecondFilterDate.Date)
                    {
                        orderfilter.Add(order);
                    }
                }
            }
            else if (SelectedFilterProvider != null && ProvidersFilter.Contains(SelectedFilterProvider))
            {
                foreach (var order in AllOrders)
                {
                    if (order.Menu.Name.Contains(SelectedFilterMenu?.Name) && order.Provider.Name.Contains(SelectedFilterProvider?.Name) && order.Food.Name.Contains(SelectedFilterFood?.Name) && order.DateTime.Date >= FirstFilterDate.Date && order.DateTime.Date <= SecondFilterDate.Date)
                    {
                        orderfilter.Add(order);
                    }
                }
            }
            else
            {
                foreach (var order in AllOrders)
                {
                    if (order.Menu.Name.Contains(SelectedFilterMenu?.Name) && order.DateTime.Date >= FirstFilterDate.Date && order.DateTime.Date <= SecondFilterDate.Date)
                    {
                        orderfilter.Add(order);
                    }
                }
            }
            Orders = orderfilter;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Orders"));
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
            AllOrders = new OrderSql(dB).GetData();
            FoodProviders = new FoodSql(dB).GetFoodNullProviders();
            FoodMenus = new FoodSql(dB).GetFoodNullMenus();
            MenusFilter = new MenuSql(dB).GetNullMenuFoods();
            Foods = new FoodSql(dB).GetNullFoodProviders();
            SecondFilterDate = DateTime.Now;
            FirstFilterDate = DateTime.MinValue;
            DataContext = this;
            this.dB = dB;
            label1.Visibility = Visibility.Collapsed;
            comboBox1.Visibility = Visibility.Collapsed;
            label2.Visibility = Visibility.Collapsed;
            comboBox2.Visibility = Visibility.Collapsed;
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
            AllOrders.Add(addNewOrder.NewOrder);
            FoodMenus = new FoodSql(dB).GetData();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FoodMenus"));
            FilterStart();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
                return;
            MessageBoxResult result = MessageBox.Show("Вы хотите удалить заказ, сохранив изменения количетсва еды?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNoCancel,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                OrderSql orderSql = new OrderSql(dB);
                orderSql.RemoveOrder(SelectedOrder);
                Orders.Remove(SelectedOrder);
                AllOrders.Remove(SelectedOrder);
                Order nullorder = new Order { ID=0};
                SelectedOrder = nullorder;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedOrder"));
            }
            else if (result == MessageBoxResult.No)
            {
                foreach (var food in FoodMenus)
                {
                    if (food.ID == SelectedOrder.Food.ID)
                    {
                        FoodSql foodSql = new FoodSql(dB);
                        food.Count += SelectedOrder.Count;
                        foodSql.EditFood(food);
                    }
                }
                OrderSql orderSql = new OrderSql(dB);
                orderSql.RemoveOrder(SelectedOrder);
                Orders.Remove(SelectedOrder);
                AllOrders.Remove(SelectedOrder);
                Order nullorder = new Order { ID = 0 };
                SelectedOrder = nullorder;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedOrder"));
            }
        }
    }
}
