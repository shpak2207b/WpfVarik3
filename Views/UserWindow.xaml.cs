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
        private readonly User _currentUser;
        public UserWindow(User user)
        {
            _currentUser = _db.Users.Include(u => u.Role).First(u => u.Id == user.Id);
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            InventoryGrid.ItemsSource = _db.Inventories.Include(i => i.User).ToList();
        }
        private Inventory? SelectedInventory => InventoryGrid.SelectedItem as Inventory;



        private void TakeButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedInventory == null)
            {
                MessageBox.Show("Выбеоите инструмент");
                return;
            }
            if (SelectedInventory.Status != Status.InStock)
            {
                MessageBox.Show("Этот инструмент уже занят");
                return;
            }
            SelectedInventory.Status = Status.Given;
            SelectedInventory.UserId = _currentUser.Id;
            _db.SaveChanges();
            LoadData();

        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedInventory == null)
            {
                MessageBox.Show("Выбеоите инструмент");
                return;
            }
            if (SelectedInventory.UserId != _currentUser.Id)
            {
                MessageBox.Show("Выберите свой инстркмент");
                return;
            }
            SelectedInventory.UserId = null;
            SelectedInventory.Status = Status.InStock;
            _db.SaveChanges();
            LoadData();
        }
        private void OpenMyInstrumentsButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new MyInstrumentsWindow(_currentUser);
            win.Show();
        }
    }
}
