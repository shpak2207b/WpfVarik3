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
    /// Логика взаимодействия для MyInstrumentsWindow.xaml
    /// </summary>
    public partial class MyInstrumentsWindow : Window
    {
        private readonly AppDbContext _db = new();
        private readonly int _userId;
        public MyInstrumentsWindow(User user)
        {
            _userId = user.Id;
            InitializeComponent();
            LoadInventory();
        }
        public void LoadInventory()
        {
            MyInventoryGrid.ItemsSource = _db.Inventories.Where(i => i.UserId == _userId).ToList();
        }
        
    }
}
