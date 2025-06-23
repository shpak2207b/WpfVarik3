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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly AppDbContext _db = new();
        public Inventory Inventory { get; private set; }
        public EditWindow(Inventory inventory)
        {
            Inventory = inventory;
            InitializeComponent();
            StatusBox.ItemsSource = Enum.GetValues(typeof(Status));

            InventoryNumberBox.Text = Inventory.InventoryNumber.ToString();
            NameBox.Text = Inventory.Name;
            DescriptionBox.Text = Inventory.Description;
            TypeBox.Text = Inventory.Type;


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (StatusBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус!");
                return;
            }

            Inventory.InventoryNumber = Convert.ToInt32(InventoryNumberBox.Text);
            Inventory.Name = NameBox.Text.Trim();
            Inventory.Type = TypeBox.Text.Trim();
            Inventory.Description = DescriptionBox.Text.Trim();
            Inventory.Status = (Status)StatusBox.SelectedItem;

            _db.SaveChanges();
            DialogResult = true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
