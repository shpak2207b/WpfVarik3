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
    public partial class AddEditWindow : Window
    {
        private Product _product;
        private readonly AppDbContext _db = new();
        public Product Product { get; set; }
        public AddEditWindow()
        {
            InitializeComponent();
            _product = new Product();
            loadComboBoxes();

        }
        public AddEditWindow(Product product)
        {
            _product = product;
            InitializeComponent();
            loadComboBoxes();
            loadProductData();

        }
        private void loadComboBoxes()
        {
            using (var context = new AppDbContext())
            {
                CategoryComboBox.ItemsSource = context.Categories.ToList();
                CategoryComboBox.DisplayMemberPath = "CategoryName";
                CategoryComboBox.SelectedValuePath = "CategoryId";

                SupplierComboBox.ItemsSource = context.Suppliers.ToList();
                SupplierComboBox.DisplayMemberPath = "SupplierName";
                SupplierComboBox.SelectedValuePath = "SupplierId";
            }
        }
        private void loadProductData()
        {
            NameTextBox.Text = _product.ProductName;
            PriceTextBox.Text = _product.Price.ToString();
            DiscountTextBox.Text = _product.Discount?.ToString() ?? "0";
            QuantityTextBox.Text = _product.StockQuantity.ToString();

            CategoryComboBox.SelectedValue = _product.CategoryId;
            SupplierComboBox.SelectedValue = _product.SupplierId;

        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                CategoryComboBox.SelectedItem == null ||
                SupplierComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля!");
                return;
            }

            // Проверяем числовые поля
            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) ||
                !decimal.TryParse(DiscountTextBox.Text, out decimal discount) ||
                !int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Проверьте числовые поля!");
                return;
            }

            using (var context = new AppDbContext())
            {
                if (_product.ProductId == 0) //Новый товар
                {
                    _product.ProductName = NameTextBox.Text;
                    _product.Price = price;
                    _product.Discount = discount;
                    _product.StockQuantity = quantity;
                    _product.CategoryId = (int)CategoryComboBox.SelectedValue;
                    _product.SupplierId = (int)SupplierComboBox.SelectedValue;
                    _product.Manufacturer = ((Supplier)SupplierComboBox.SelectedItem).SupplierName;
                    _product.CreatedDate = DateTime.Now;

                    context.Products.Add(_product);
                }
                else
                {
                    var existingProduct = context.Products.Find(_product.ProductId);
                    if (existingProduct != null)
                    {
                        existingProduct.ProductName = NameTextBox.Text;
                        existingProduct.Price = price;
                        existingProduct.Discount = discount;
                        existingProduct.StockQuantity = quantity;
                        existingProduct.CategoryId = (int)CategoryComboBox.SelectedValue;
                        existingProduct.SupplierId = (int)SupplierComboBox.SelectedValue;
                        existingProduct.Manufacturer = ((Supplier)SupplierComboBox.SelectedItem).SupplierName;
                    }
                }

                context.SaveChanges();
            }
            DialogResult = true;
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
