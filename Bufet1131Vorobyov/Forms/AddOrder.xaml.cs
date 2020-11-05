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
        public ObservableCollection<Food> Foods { get; set; }
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
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
                SelectedOrder.Count = value;
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
            DataContext = this;
            this.dB = dB;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
