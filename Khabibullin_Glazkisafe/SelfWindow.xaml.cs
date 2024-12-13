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
    /// <summary>
    /// Логика взаимодействия для SelfWindow.xaml
    /// </summary>
    public partial class SelfWindow : Window
    {
        private Agent _currentAgents = new Agent();
        public SelfWindow(int max)
        {
            InitializeComponent();

            TBPriority.Text = max.ToString();
        }

        private void EditPriorityButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
