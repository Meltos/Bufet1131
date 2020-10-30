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
    /// Логика взаимодействия для AddFood.xaml
    /// </summary>
    public partial class AddFood : Window, INotifyPropertyChanged
    {
        private Food selectedFood;
        private string nameFood;
        private int countFood;
        private int priceFood;
        private string descriptionFood;
        private string pathIMG;
        private readonly DB dB;

        public ObservableCollection<Food> Foods { get; set; }
        public Food SelectedFood
        {
            get => selectedFood;
            set
            {
                if (value != null)
                {
                    selectedFood = value;
                    NameFood = SelectedFood.Name;
                    CountFood = SelectedFood.Count;
                    PriceFood = SelectedFood.Price;
                    DescriptionFood = SelectedFood.Description;
                    PathIMG = SelectedFood.PathIMG;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedFood"));
                }
            }
        }
        public string NameFood 
        {
            get => nameFood;
            set
            {
                nameFood = value;
                SelectedFood.Name = value;
                EditFood(SelectedFood);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameFood"));
            }
        }
        public int CountFood
        {
            get => countFood;
            set
            {
                countFood = value;
                SelectedFood.Count = value;
                EditFood(SelectedFood);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountFood"));
            }
        }
        public int PriceFood
        {
            get => priceFood;
            set
            {
                priceFood = value;
                SelectedFood.Price = value;
                EditFood(SelectedFood);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PriceFood"));
            }

        }
        public string DescriptionFood
        {
            get => descriptionFood;
            set
            {
                descriptionFood = value;
                SelectedFood.Description = value;
                EditFood(SelectedFood);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescriptionFood"));
            }
        }
        public string PathIMG
        {
            get => pathIMG;
            set
            {
                pathIMG = value;
                SelectedFood.PathIMG = value;
                EditFood(SelectedFood);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PathIMG"));
            }
        }

        private void EditFood(Food selectedFood)
        {
            FoodSql foodSql = new FoodSql(dB);
            foodSql.EditFood(selectedFood);
        }

        public AddFood(DB dB)
        {
            InitializeComponent();

            Foods = new FoodSql(dB).GetData();
            DataContext = this;
            this.dB = dB;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Food newfood = new Food();
            newfood.PathIMG = "";
            FoodSql foodSql = new FoodSql(dB);
            int id = foodSql.AddNewFood(newfood);
            newfood.ID = id;
            Foods.Add(newfood);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedFood == null)
                return;
            MessageBoxResult result = MessageBoxResult.No;
            if (SelectedFood.Menus.Count != 0)
            {
                var menustring = string.Join(", ", SelectedFood.Menus.Select(s => s.Name));
                result = MessageBox.Show($"Это блюдо есть в этих меню: {menustring}. Вы точно хотите удалить блюдо?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);
            }
            else
            {
                result = MessageBox.Show("Вы точно хотите удалить блюдо?",
                                         "Предупреждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            }
            if (result == MessageBoxResult.Yes)
            {
                FoodSql foodSql = new FoodSql(dB);
                foodSql.RemoveFood(SelectedFood);
                Foods.Remove(SelectedFood);
            }
        }
    }
}
