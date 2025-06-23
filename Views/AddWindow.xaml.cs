using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly AppDbContext _db = new();
        public AddWindow()
        {
            InitializeComponent();
            StatusBox.ItemsSource = Enum.GetValues(typeof(Status));

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var number = Convert.ToInt32(InventoryNumberBox.Text.Trim());
            var name = NameBox.Text.Trim();
            var type = TypeBox.Text.Trim();
            var desc = DescriptionBox.Text.Trim();
            var addDate = DateOnly.FromDateTime(DateTime.Now);
            var status = (Status)StatusBox.SelectedItem;

            _db.Inventories.Add(new Inventory
            {
                InventoryNumber = number,
                Name = name,
                Type = type,
                Description = desc,
                PublicationDate = addDate,
                Status = status
            });
            _db.SaveChanges();
            Close();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
