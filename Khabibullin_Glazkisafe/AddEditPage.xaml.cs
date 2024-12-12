using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Khabibullin_Glazkisafe
{
    public partial class AddEditPage : Page
    {
        private AgentsPage _agentsPage = new AgentsPage();
        private Agent _currentAgents = new Agent();
        public AddEditPage(Agent SelectedAgents)
        {
            if (SelectedAgents != null)
                _currentAgents = SelectedAgents;

            DataContext = _currentAgents;

            
            InitializeComponent();
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                _currentAgents.Logo = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentAgents.Title))
                errors.AppendLine("Укажите наименование агента");

            if (string.IsNullOrWhiteSpace(_currentAgents.Address))
                errors.AppendLine("Укажите адрес агента");

            if (string.IsNullOrWhiteSpace(_currentAgents.DirectorName))
                errors.AppendLine("Укажите ФИО директора");

            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");

            if (string.IsNullOrWhiteSpace(_currentAgents.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");

            if (_currentAgents.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");

            if (string.IsNullOrWhiteSpace(_currentAgents.INN))
                errors.AppendLine("Укажите ИНН агента");

            if (string.IsNullOrWhiteSpace(_currentAgents.KPP))
                errors.AppendLine("Укажите КПП агента");

            if (string.IsNullOrWhiteSpace(_currentAgents.Phone))
                errors.AppendLine("Укажите телефон агента");

            else
            {
                string ph = _currentAgents.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "");

                if ((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11 || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgents.Email))
                errors.AppendLine("Укажите почту агента");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentAgents.ID == 0)
                Khabibullin_GlazkisafeEntities1.GetContext().Agent.Add(_currentAgents);

            try
            {
                Khabibullin_GlazkisafeEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;

            var currentProductSales = Khabibullin_GlazkisafeEntities1.GetContext().ProductSale.ToList();
            currentProductSales = currentProductSales.Where(p => p.AgentID == currentAgent.ID).ToList();

            if (currentProductSales.Count != 0)
            {
                MessageBox.Show($"Невозможно выполнить удаление, так как существует записи на эту услугу");
            }
            else
            {
                if (MessageBox.Show($"Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Khabibullin_GlazkisafeEntities1.GetContext().Agent.Remove(currentAgent);
                        Khabibullin_GlazkisafeEntities1.GetContext().SaveChanges();

                        _agentsPage.AgentsListView.ItemsSource = Khabibullin_GlazkisafeEntities1.GetContext().Agent.ToList();

                        _agentsPage.UpdateAgents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
    }
}
