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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly AppDbContext _db = new();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text.Trim();  
            var password = PasswordBox.Text.Trim();


            if(String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполнмите все поля");
                return;
            }
            if(_db.Users.Any(u => u.Login == login))
            {
                MessageBox.Show("Этот логин занят");
            }
            var newUser = new User
            {
                Login = login,
                Password = password,
                CreatedDate = DateTime.Now,
                Name = login,
                Role = "user"

            };
            _db.Users.Add(newUser);
            _db.SaveChanges();
            MessageBox.Show("success");
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new LoginWindow();
            window.Show();
            Close();
        }
    }
}
