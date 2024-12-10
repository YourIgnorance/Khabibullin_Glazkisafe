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
        public int CountRecords, CountPage, CurrentPage = 0;
        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public AgentsPage()
        {
            InitializeComponent();


            var currentService = Khabibullin_GlazkisafeEntities1.GetContext().Agent.ToList();

            for (int i = 0; i < currentService.Count; i++)
            {
                currentService[i].Title = currentService[i].Title.Replace("C", "С");
            }
            AgentsListView.ItemsSource = currentService;

            SortComboBox.SelectedIndex = 0;
            FiltrComboBox.SelectedIndex = 0;

            UpdateAgents();
        }
        private void UpdateAgents()
        {
            var currentAgents = Khabibullin_GlazkisafeEntities1.GetContext().Agent.ToList();

            if (SortComboBox.SelectedIndex == 0)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 1)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 2)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 3)
            {
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            }
            if (SortComboBox.SelectedIndex == 4)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }
            if (SortComboBox.SelectedIndex == 5)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (SortComboBox.SelectedIndex == 6)
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
            currentAgents = currentAgents.Where(p => p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.ToLower().Replace(" (", "").Replace(")", "").Replace(" ", "").Replace("-", "").Contains(TBoxSearch.Text.ToLower())).ToList();
            AgentsListView.ItemsSource = currentAgents.ToList();

            

            AgentsListView.ItemsSource = currentAgents;

            TableList = currentAgents;

            ChangePage(0, 0);
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

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }
        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }


        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }
        public void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }
            Boolean Ifupdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;

                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();

                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = $" из {CountRecords.ToString()}";

                AgentsListView.ItemsSource = CurrentPageList;

                AgentsListView.Items.Refresh();
            }
        }
    }
}
