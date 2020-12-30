using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using BusinessSuiteByVesune.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BusinessSuiteByVesune
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoLogin();
        }

        public void DoLogin()
        {
            foreach (ItemsControl item in this.MainMenu.Items)
            {
                item.Visibility = Visibility.Collapsed;
            }

            Views.LoginWindow window = new LoginWindow();
            this.CpCurrentScreenContent.Content = window.Content;

            window.BtnLogin.Click += (s, ev) =>
            {
                ValidateLogin(window.txtPassword.Password);
            };
        }

        public void ValidateLogin(string pin)
        {
            if (pin == "ADMIN69")
            {
                AdminLogin();
            }
            else
            {
                EmployeeLogin(pin);
            }
        }

        public void AdminLogin()
        {
            ResetMenuButtons();
            foreach (ItemsControl item in this.MainMenu.Items)
            {
                item.Visibility = Visibility.Visible;
            }
            Views.ControlPanelWindow cpWindow = new ControlPanelWindow();
            this.CpCurrentScreenContent.Content = cpWindow.Content;
            this.miHome.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Admin Panel";
        }

        public void EmployeeLogin(string pin)
        {
            User user = new UserCRUD().Login(pin);

            if (user.UserId > 0)
            {
                WorkPeriodWindow wpWindow = new WorkPeriodWindow(user);
                this.CpCurrentScreenContent.Content = wpWindow.Content;
                wpWindow.BtnDone.Click += (s1, ev1) =>
                {
                    DoLogin();
                };
            }
            else
            {
                MessageBox.Show("Your pin was not recognized. Please enter a valid employee pin. If you are unable to login, contact your supervisor to change your pin.", "Notice");
            }
        }

        public void ResetMenuButtons()
        {
            this.miInventory.Background = Brushes.DimGray;
            this.miOrders.Background = Brushes.DimGray;
            this.miJobs.Background = Brushes.DimGray;
            this.miUsers.Background = Brushes.DimGray;
            this.miManager.Background = Brushes.DimGray;
            this.miInvoices.Background = Brushes.DimGray;
            this.miHome.Background = Brushes.DimGray;
            this.miTransactions.Background = Brushes.DimGray;
            this.miTimes.Background = Brushes.DimGray;
        }

        private void miInventory_Click(object sender, RoutedEventArgs e)
        {
            Views.InventoryWindow window = new Views.InventoryWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miInventory.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Inventory Management";
        }

        private void miOrders_Click(object sender, RoutedEventArgs e)
        {
            Views.OrdersWindow window = new OrdersWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miOrders.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Order Tracking";
        }

        private void miJobs_Click(object sender, RoutedEventArgs e)
        {
            Views.JobsWindow window = new JobsWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miJobs.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Jobs and Charges";
        }

        private void miUsers_Click(object sender, RoutedEventArgs e)
        {
            Views.UsersWindow window = new UsersWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miUsers.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "User Manager";
        }

        private void miManager_Click(object sender, RoutedEventArgs e)
        {
            Views.ManagerWindow window = new ManagerWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miManager.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Types Manager";
        }

        private void miInvoices_Click(object sender, RoutedEventArgs e)
        {
            Views.InvoicesWindow window = new InvoicesWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miInvoices.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Invoices";
        }

        private void miTimes_Click(object sender, RoutedEventArgs e)
        {
            Views.TimesWindow window = new TimesWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miTimes.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Time Entry Management";
        }

        private void miLogout_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.I && (Keyboard.Modifiers == ModifierKeys.Control))
            {
                Views.InventoryWindow window = new Views.InventoryWindow();
                this.CpCurrentScreenContent.Content = window.Content;
                ResetMenuButtons();
                this.miInventory.Background = Brushes.LightSlateGray;
                this.LblContentTitle.Content = "Inventory Management";
            }
        }

        private void miHome_Click(object sender, RoutedEventArgs e)
        {
            Views.ControlPanelWindow window = new Views.ControlPanelWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miHome.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Admin Panel";
        }

        private void miTransactions_Click(object sender, RoutedEventArgs e)
        {
            Views.TransactionsWindow window = new TransactionsWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miTransactions.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Manage Transactions";
        }

        private void miCalculator_Click(object sender, RoutedEventArgs e)
        {
            Views.CalculatorWindow window = new CalculatorWindow();
            this.CpCurrentScreenContent.Content = window.Content;
            ResetMenuButtons();
            this.miCalculator.Background = Brushes.LightSlateGray;
            this.LblContentTitle.Content = "Calculator";
        }
    }
}
