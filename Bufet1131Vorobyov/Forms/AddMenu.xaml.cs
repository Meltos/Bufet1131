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
    /// Логика взаимодействия для AddMenu.xaml
    /// </summary>
    public partial class AddMenu : Window, INotifyPropertyChanged
    {

        private Menu selectedMenu;
        private string name;
        private readonly DB dB;

        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<Menu> Menus { get; set; }
        public ObservableCollection<Food> MenuFoods { get; set; }
        public Food SelectedFood { get; set; }
        public Food SelectedFoodMenu { get; set; }
        public Menu SelectedMenu
        {
            get => selectedMenu;
            set
            {
                if (value != null)
                {
                    selectedMenu = value;
                    Foods = value.Foods;
                    MenuFoods = GetNoMenuFoods(value);
                    NameMenu = selectedMenu.Name;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuFoods"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMenu"));
                }    
            }
        }
        public string NameMenu
        { 
            get => name;
            set 
            { 
                name = value;
                SelectedMenu.Name = value;
                EditMenu(SelectedMenu);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameMenu"));
            }
        }

        private void EditMenu(Menu selectedMenu)
        {
            MenuSql menuSql = new MenuSql(dB);
            menuSql.EditMenu(selectedMenu);
        }

        private ObservableCollection<Food> GetNoMenuFoods(Menu value)
        {
            return new FoodSql(new DB()).GetNoMenuFoods(value);
        }

        public AddMenu(DB dB)
        {
            InitializeComponent();
            Menus = new MenuSql(dB).GetData();
            DataContext = this;
            this.dB = dB;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Menu newmenu = new Menu { Name = ""};
            MenuSql menuSql = new MenuSql(dB);
            int id = menuSql.AddNewMenu(newmenu);
            newmenu.ID = id;
            Menus.Add(newmenu);
            MessageBox.Show("Новое меню создано.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedMenu == null)
                return;
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить меню?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MenuSql menuSql = new MenuSql(dB);
                menuSql.RemoveMenu(SelectedMenu);
                Menus.Remove(SelectedMenu);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFood == null || SelectedMenu.Foods.Contains(SelectedFood))
                return;
            SelectedMenu.Foods.Add(SelectedFood);
            MenuSql menuSql = new MenuSql(dB);
            menuSql.AddFoodMenu(SelectedFood, SelectedMenu);
            ChangeSelectedMenu();
        }

        private void ChangeSelectedMenu()
        {
            MenuFoods = GetNoMenuFoods(SelectedMenu);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuFoods"));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectedFoodMenu == null)
                return;
            MenuSql menuSql = new MenuSql(dB);
            menuSql.RemoveFoodMenu(SelectedFoodMenu, SelectedMenu);
            SelectedMenu.Foods.Remove(SelectedFoodMenu);
            ChangeSelectedMenu();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            if (SelectedMenu == null)
                return;
            MenuSql menuSql = new MenuSql(dB);
            menuSql.PublicMenu(SelectedMenu);
            MessageBox.Show("Меню опубликовано.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (SelectedMenu == null)
                return;
            MenuSql menuSql = new MenuSql(dB);
            menuSql.NoPublicMenu(SelectedMenu);
            MessageBox.Show("Меню распубликовано.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
