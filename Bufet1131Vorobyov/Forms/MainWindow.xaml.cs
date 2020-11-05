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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bufet1131Vorobyov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        DB dB;
        private Menu selectedMenu;

        public ObservableCollection<Menu> Menus { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public Menu SelectedMenu
        { 
            get => selectedMenu;
            set
            {
                if (value != null)
                {
                    selectedMenu = value;
                    Foods = value.Foods;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            dB = new DB();
            Menus = new MenuSql(dB).GetPublicMenu();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddMenu addMenu = new AddMenu(dB);
            addMenu.ShowDialog();
            Menus = new MenuSql(dB).GetPublicMenu();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Menus"));
            Foods = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddFood addFood = new AddFood(dB);
            addFood.ShowDialog();
            Menus = new MenuSql(dB).GetPublicMenu();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Menus"));
            Foods = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            AddProvider addProvider = new AddProvider(dB);
            addProvider.ShowDialog();
            Menus = new MenuSql(dB).GetPublicMenu();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Menus"));
            Foods = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            AddOrder addOrder = new AddOrder(dB);
            addOrder.ShowDialog();
            Menus = new MenuSql(dB).GetPublicMenu();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Menus"));
            Foods = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
        }
    }
}
