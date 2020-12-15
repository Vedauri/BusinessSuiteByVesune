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

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for ControlPanelWindow.xaml
    /// </summary>
    public partial class ControlPanelWindow : Window
    {
        public ControlPanelWindow()
        {
            InitializeComponent();
            CalMain.DisplayDate = DateTime.Now;
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            new InventoryWindow().Show();
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            new OrdersWindow().Show();
        }

        private void BtnJobs_Click(object sender, RoutedEventArgs e)
        {
            new JobsWindow().Show();
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            new UsersWindow().Show();
        }

        private void BtnManager_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void BtnInvoices_Click(object sender, RoutedEventArgs e)
        {
            new InvoicesWindow().Show();
        }

        private void BtnTimes_Click(object sender, RoutedEventArgs e)
        {
            new TimesWindow().Show();
        }
    }
}
