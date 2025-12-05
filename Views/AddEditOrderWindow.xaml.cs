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
    /// Логика взаимодействия для AddEditOrderWindow.xaml
    /// </summary>
    public partial class AddEditOrderWindow : Window
    {
        private Order _order;
        private bool _edit;

        public AddEditOrderWindow()
        {
            InitializeComponent();
            _order = new Order();
            _edit = false;
            LoadData();
        }

        public AddEditOrderWindow(Order order)
        {
            InitializeComponent();
            _order = order;
            _edit = true;
            LoadData();
            LoadOrderData();
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                ProductComboBox.ItemsSource = context.Products.ToList();


                StatusComboBox.SelectedIndex = 0;

                OrderDatePicker.SelectedDate = DateTime.Now;
            }
        }

        private void LoadOrderData()
        {
            using (var context = new AppDbContext())
            {

                var order = context.Orders
                    .Include(o => o.Product)
                    .FirstOrDefault(o => o.OrderId == _order.OrderId);

                if (order != null)
                {
                    ProductComboBox.Text = order.Product.ProductName;
                    StatusComboBox.Text = order.Status;
                    AddressTextBox.Text = order.DeliveryAddress;
                    OrderDatePicker.SelectedDate = order.OrderDate;
                    DeliveryDatePicker.SelectedDate = order.DeliveryDate;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(AddressTextBox.Text))
            {
                MessageBox.Show("Заполните все нужные поля");
                return;
            }
            if (OrderDatePicker.SelectedDate > DeliveryDatePicker.SelectedDate)
            {
                MessageBox.Show("Дата заказа позднее доставки");
                return;
            }

            using (var context = new AppDbContext())
            {
                if (!_edit)
                {
                    context.Orders.Add(_order);
                }
                else
                {
                    _order = context.Orders.Find(_order.OrderId);
                }

                _order.ProductId = ((Product)ProductComboBox.SelectedItem).ProductId;
                _order.Status = StatusComboBox.Text;
                _order.DeliveryAddress = AddressTextBox.Text;
                _order.OrderDate = OrderDatePicker.SelectedDate ?? DateTime.Now;
                _order.DeliveryDate = DeliveryDatePicker.SelectedDate;
                _order.UserId = 1;

                context.SaveChanges();
            }

            MessageBox.Show("Успешно сохранено");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
