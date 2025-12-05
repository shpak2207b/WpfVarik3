using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using WpfVarik3.Models;

namespace WpfVarik3.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        private User _currentUser;
        public OrdersWindow(User user)
        {
            _currentUser = user;
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (var context = new AppDbContext())
            {
                var orders = context.Orders
                    .Include(o => o.Product)
                    .Include(o => o.User)
                    .ToList();

                OrdersListView.ItemsSource = orders;
            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new AddEditOrderWindow();
            editWindow.Closed += (s, args) => LoadOrders();
            editWindow.Show();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ!");
                return;
            }

            var selectedOrder = (Order)OrdersListView.SelectedItem;
            var editWindow = new AddEditOrderWindow(selectedOrder);
            editWindow.Closed += (s, args) => LoadOrders();
            editWindow.Show();
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem == null) return;

            var selectedOrder = (Order)OrdersListView.SelectedItem;

            if (MessageBox.Show("Удалить заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    var order = context.Orders.Find(selectedOrder.OrderId);
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
                LoadOrders();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == _currentUser.Login && u.Password == _currentUser.Password);
                AdminWindow window = new AdminWindow(user);
                window.Show();
                this.Close();
            }
        }
    }
}
