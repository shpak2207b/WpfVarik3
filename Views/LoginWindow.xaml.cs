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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AppDbContext _db = new();

        public LoginWindow()
        {
            InitializeComponent();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Text;
            var user = _db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("неверный логин или пароль");
                return;
            }
            if (user.Role == "admin")
            {
                var window = new AdminWindow(user);
                window.Show();
            }
            else
            {
                var window = new UserWindow(user);
                window.Show();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
                var window = new RegisterWindow();
                window.Show();
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuestLoginButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserWindow();
            window.Show();
        }
    }
}
