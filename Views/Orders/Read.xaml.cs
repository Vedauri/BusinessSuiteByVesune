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
    /// Interaction logic for Read.xaml
    /// </summary>
    public partial class Read : Window
    {
        public Read()
        {
            InitializeComponent();
        }

        public Read(Order order)
        {
            InitializeComponent();
            this.DataContext = order;

            this.TxtOrderStatus.Content = new OrderCRUD().GetOrderStatuses().Where(x => x.OrderStatusId == order.OrderStatusId).First().OrderStatusName;
            this.LblJobName.Content = new JobCRUD().GetJobs().Where(x => x.JobId == order.JobId).First().Name;
        }
    }
}
