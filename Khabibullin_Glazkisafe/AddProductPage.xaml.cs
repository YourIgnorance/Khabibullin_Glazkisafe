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

namespace Khabibullin_Glazkisafe
{
    public partial class AddProductPage : Window
    {
        private Agent _currentAgents = new Agent();
        private ProductSale _currentProductSale = new ProductSale();
        DateTime? ProductDate;
        public AddProductPage(Agent selectedAgent)
        {
            InitializeComponent();

            if (selectedAgent != null)
                _currentAgents = selectedAgent;

            var currentProductSale = Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.ToList();

            DataContext = _currentProductSale;

            ProductDate = null;

            currentProductSale = currentProductSale.Where(p => p.AgentID == _currentAgents.ID).ToList();

            var currentProducts = Khabibullin_GlazkisafeEntities1.GetContext().Product.ToList();
            currentProducts = currentProducts.Where(p => p.Title.ToLower().Contains(TBoxSearhProduct.Text.ToLower())).ToList();
            ProductComboBox.ItemsSource = currentProducts.Select(p => p.Title);
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TBoxSearhProduct.Text = "";
        }
        public void UpdateProducts()
        {
            var currentProductSales = Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.ToList();

            currentProductSales = currentProductSales.Where(p => p.GetProductName.ToLower().Contains(TBoxSearhProduct.Text.ToLower())).ToList();

            ProductComboBox.ItemsSource = currentProductSales.Select(p => p.GetProductName);
        }

        private void TBoxSearhProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var productList = Khabibullin_GlazkisafeEntities1.GetContext().Product.ToList();
            int productCount = 1;
            StringBuilder errors = new StringBuilder();

            if (ProductComboBox.SelectedIndex < 0)
                errors.AppendLine("Укажите наименование продукта");
            if(ProductDate == null)
                errors.AppendLine("Укажите дату продукта");
            if (int.TryParse(TBoxCountProduct.Text, out int value))
            {
                if (value <= 0)
                {
                    errors.AppendLine("Количество продукции должно быть больше 0!");
                }
                else
                {
                    productCount = value;
                }
            }
            else
            {
                errors.AppendLine("Значение количества продукции указано неверно!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            productList = productList.Where(p => p.Title.ToLower().Contains(ProductComboBox.SelectedItem.ToString().ToLower())).ToList();

            var productID = productList.Select(p => p.ID);

            _currentProductSale.AgentID = _currentAgents.ID;
            _currentProductSale.ProductID = ProductComboBox.SelectedIndex + 1;
            _currentProductSale.SaleDate = (DateTime)ProductDate;
            _currentProductSale.ProductCount = productCount;

            
            Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.Add(_currentProductSale);

            try
            {
                Khabibullin_GlazkisafeEntities1.GetContext().SaveChanges();

                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void CloseWindow()
        {
            this.Close();
        }

        private void dtPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductDate = (DateTime)(((DatePicker)sender).SelectedDate);
        }
    }
}