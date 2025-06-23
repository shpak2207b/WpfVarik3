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
            var user = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("idi nahu1");
                return;
            }
            if (user.Role.Name == "Admin")
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
    }
}
