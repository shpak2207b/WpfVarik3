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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly AppDbContext _db = new();
        private readonly User _currentUser = new();
        public AdminWindow(User user)
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


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddEditWindow();
            wnd.ShowDialog();
            LoadData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }
            var wnd = new AddEditWindow(SelectedProduct);
            wnd.ShowDialog();

            _db.Entry(SelectedProduct).Reload(); //важно лдя обновления грида!!
            LoadData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
                return;
            }

            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить эту запись?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _db.Products.Remove(SelectedProduct);
                _db.SaveChanges();

                MessageBox.Show("Запись удалена!");
                LoadData(); // Обновляем таблицу
            }
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrdersWindow(_currentUser);
            window.Show();
            Close();
        }
    }
}
