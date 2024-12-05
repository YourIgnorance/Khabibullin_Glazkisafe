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
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        public AgentsPage()
        {
            InitializeComponent();

            var currentService = Khabibullin_GlazkisafeEntities.GetContext().Agent.ToList();

            AgentsListView.ItemsSource = currentService;

            SortComboBox.SelectedIndex = 0;
            FiltrComboBox.SelectedIndex = 0;

            UpdateAgents();
        }
        private void UpdateAgents()
        {
            var currentAgents = Khabibullin_GlazkisafeEntities.GetContext().Agent.ToList();


            if (SortComboBox.SelectedIndex == 0)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 1)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 2)
            {
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            }
            if (SortComboBox.SelectedIndex == 3)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }
            if (SortComboBox.SelectedIndex == 4)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 5)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }

            if (FiltrComboBox.SelectedIndex == 0)
            {
                currentAgents = currentAgents.Where(p => (p.GetAgentType.Contains("МФО") ||p.GetAgentType.Contains("ООО") || p.GetAgentType.Contains("ОАО") || p.GetAgentType.Contains("ЗАО") || p.GetAgentType.Contains("ПАО") ||p.GetAgentType.Contains("МКК"))).ToList();
            }
            if (FiltrComboBox.SelectedIndex == 1)
            {
                currentAgents = currentAgents.Where(p => p.GetAgentType.Contains("МФО")).ToList();
            }
            if (FiltrComboBox.SelectedIndex == 2)
            {
                currentAgents = currentAgents.Where(p => (p.GetAgentType.Contains("ООО"))).ToList();
            }
            if (FiltrComboBox.SelectedIndex == 3)
            {
                currentAgents = currentAgents.Where(p => (p.GetAgentType.Contains("ЗАО"))).ToList();
            }
            if (FiltrComboBox.SelectedIndex == 4)
            {
                currentAgents = currentAgents.Where(p => (p.GetAgentType.Contains("МКК"))).ToList();
            }
            if (FiltrComboBox.SelectedIndex == 5)
            {
                currentAgents = currentAgents.Where(p => (p.GetAgentType.Contains("ОАО"))).ToList();
            }
            if (FiltrComboBox.SelectedIndex == 6)
            {
                currentAgents = currentAgents.Where(p => (p.GetAgentType.Contains("ПАО"))).ToList();
            }
            currentAgents = currentAgents.Where(p => p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            AgentsListView.ItemsSource = currentAgents.ToList();
        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void FiltrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
