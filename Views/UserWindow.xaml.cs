using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly AppDbContext _db = new();
        private readonly User _currentUser = new();
        public UserWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public UserWindow(User user)
        {
            _currentUser = _db.Users.First(u => u.Id == user.Id);
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ProductsGrid.ItemsSource = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToList();
        }

        private Product? SelectedProduct => ProductsGrid.SelectedItem as Product;


        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrdersWindow(_currentUser);
            window.Show();
            Close();
        }
    }
}
