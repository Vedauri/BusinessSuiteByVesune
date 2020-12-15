using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
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

namespace BusinessSuiteByVesune.Views.Orders
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
        }

        public Update(Order order)
        {
            InitializeComponent();
            this.DataContext = order;

            CbOrderStatus.ItemsSource = new OrderCRUD().GetOrderStatuses();
            CbOrderStatus.SelectedValuePath = "OrderStatusId";
            CbOrderStatus.DisplayMemberPath = "OrderStatusName";
            
            CbJobId.ItemsSource = new JobCRUD().GetJobs();
            CbJobId.SelectedValuePath = "JobId";
            CbJobId.DisplayMemberPath = "Name";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Order currentOrder = (Order)this.DataContext;
            int result = new OrderCRUD().Update(currentOrder);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Order was updated", "Success");
            }
            else
            {
                MessageBox.Show("Failed to update order", "Failure");
            }
        }
    }
}
