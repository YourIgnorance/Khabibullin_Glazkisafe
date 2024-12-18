using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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
    public partial class HistorySales : Window
    {

        private Agent _currentAgents = new Agent();

        public HistorySales(Agent selectedAgent)
        {
            InitializeComponent();

            if (selectedAgent != null)
                _currentAgents = selectedAgent;


            var currentProduct = Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.ToList();

            currentProduct = currentProduct.Where(p => p.AgentID == _currentAgents.ID).ToList();
            SalesListView.ItemsSource = currentProduct;

            UpdateProducts();
        }
        public void UpdateProducts()
        {
            var currentProductSales = Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.ToList();
            currentProductSales = currentProductSales.Where(p => p.AgentID == _currentAgents.ID).ToList();

            SalesListView.ItemsSource = currentProductSales;
        }
        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Khabibullin_GlazkisafeEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                SalesListView.ItemsSource = Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.ToList();
                UpdateProducts();
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SalesListView.SelectedItems.Count < 1)
            {
                MessageBox.Show("Удаление невозможно! Выберите хотя бы одну реализацию!");
            }
            else
            {
                if (MessageBox.Show($"Вы точно хотите выполнить удаление {SalesListView.SelectedItems.Count} реализаций?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        List<ProductSale> productSale = new List<ProductSale>(SalesListView.SelectedItems.OfType<ProductSale>());
                        foreach (ProductSale productSaleItem in productSale)
                        {
                            Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.Remove(productSaleItem);
                        }
                        Khabibullin_GlazkisafeEntities1.GetContext().SaveChanges();

                        UpdateProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductPage window = new AddProductPage(_currentAgents);
            bool? result = window.ShowDialog();
            if (result == true)
            {
                UpdateProducts();
            }
            else
            {
                UpdateProducts();
            }
        }
    }
}
