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
            InventoryGrid.ItemsSource = _db.Inventories.Include(i => i.User).ToList();
            UserGrid.ItemsSource = _db.Users.Include(u => u.Role).ToList();
        }

        private Inventory? SelectedInventory => InventoryGrid.SelectedItem as Inventory;
        private User? SelectedUser => UserGrid.SelectedItem as User;

        private void TakeButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedInventory == null || SelectedUser == null)
            {
                MessageBox.Show("Выберите инвентарь и пользователя");
                return;
            }
            SelectedInventory.UserId = SelectedUser.Id;
            SelectedInventory.Status = Status.Given;
            _db.SaveChanges();
            LoadData();

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedInventory == null)
            {
                MessageBox.Show("Выберите машинк");
                return;
            }

            SelectedInventory.UserId = null;
            SelectedInventory.Status = Status.InStock;
            _db.SaveChanges();
            LoadData();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddWindow();
            wnd.ShowDialog();
            LoadData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedInventory == null)
            {
                MessageBox.Show("Выберите инвентарь");
                return;
            }
            var wnd = new EditWindow(SelectedInventory);
            wnd.ShowDialog();
            LoadData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedInventory == null)
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
                _db.Inventories.Remove(SelectedInventory);
                _db.SaveChanges();

                MessageBox.Show("Запись удалена!");
                LoadData(); // Обновляем таблицу
            }
        }
    }
}
