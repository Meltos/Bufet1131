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
    /// Логика взаимодействия для AddNewOrder.xaml
    /// </summary>
    public partial class AddNewOrder : Window, INotifyPropertyChanged
    {
        private DB dB;
        private Menu selectedMenu;
        private Food selectedFood;

        public event PropertyChangedEventHandler PropertyChanged;

        public Order NewOrder { get; set; }
        public ObservableCollection<Menu> Menus { get; set; }
        public ObservableCollection<Food> MenuFoods { get; set; }
        public ObservableCollection<Provider> FoodProviders { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public Menu SelectedMenu
        {
            get => selectedMenu;
            set
            {
                if (value != null)
                {
                    selectedMenu = value;
                    label2.Visibility = Visibility.Collapsed;
                    comboBox2.Visibility = Visibility.Collapsed;
                    if (value.Foods.Count <= 0)
                    {
                        label1.Visibility = Visibility.Collapsed;
                        comboBox1.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        label1.Visibility = Visibility.Visible;
                        comboBox1.Visibility = Visibility.Visible;
                        MenuFoods = value.Foods;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuFoods"));
                    }
                }
            }
        }
        public Food SelectedFood
        {
            get => selectedFood;
            set
            {
                if (value != null)
                {
                    selectedFood = value;
                    foreach (var food in Foods)
                    {
                        if (value.ID == food.ID)
                        {
                            value.Providers = food.Providers;
                            break;
                        }
                    }
                    if (value.Providers.Count <= 0)
                    {
                        label2.Visibility = Visibility.Collapsed;
                        comboBox2.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        label2.Visibility = Visibility.Visible;
                        comboBox2.Visibility = Visibility.Visible;
                        FoodProviders = value.Providers;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FoodProviders"));
                    }
                }
                
            }
        }
        public Provider SelectedProvider { get; set; }
        public DateTime DateTimeNew { get; set; }
        public int CountNew { get; set; }

        public AddNewOrder(DB dB)
        {
            InitializeComponent();
            Foods = new FoodSql(dB).GetFoodProviders();
            Menus = new MenuSql(dB).GetPublicMenu();
            DateTimeNew = DateTime.Now;
            DataContext = this;
            this.dB = dB;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMenu == null)
            {
                MessageBox.Show("Не выбрано меню", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedFood == null || !MenuFoods.Contains(SelectedFood))
            {
                MessageBox.Show("Не выбрано блюдо", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (CountNew < 0)
            {
                MessageBox.Show("Введите количество блюд", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                NewOrder = new Order();
                NewOrder.DateTime = DateTimeNew;
                NewOrder.Menu = SelectedMenu;
                NewOrder.Food = SelectedFood;
                if (SelectedProvider == null)
                {
                    Provider provider = new Provider { ID = 0, Name = "", Status=2 };
                    provider.Foods.Add(NewOrder.Food);
                    NewOrder.Provider = provider;
                }
                else
                {
                    NewOrder.Provider = SelectedProvider;
                }
                NewOrder.Count = CountNew;
                NewOrder.Cost = NewOrder.Count * NewOrder.Food.Price;
                foreach (var food in Foods)
                {
                    if (food.ID == NewOrder.Food.ID)
                    {
                        FoodSql foodSql = new FoodSql(dB);
                        int oldfoodcount = food.Count;
                        food.Count -= NewOrder.Count;
                        if (food.Count < 0)
                        {
                            MessageBox.Show("Количество блюд не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            food.Count = oldfoodcount;
                            NewOrder = null;
                            return;
                        }
                        NewOrder.Food.Count -= NewOrder.Count;
                        foodSql.EditFood(food);
                        break;
                    }
                }
                Close();
            }
        }
    }
}
