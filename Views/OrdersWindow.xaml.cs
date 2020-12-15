using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
            BindDataGrid(null);
        }

        public void BindDataGrid(List<Order> orders)
        {
            if (orders == null)
            {
                orders = new OrderCRUD().GetOrders();
            }

            if (orders.Count > 0)
            {
                List<OrderStatus> orderStatuses = new OrderCRUD().GetOrderStatuses();
                List<Job> jobs = new JobCRUD().GetJobs();

                foreach (Order ord in orders)
                {
                    if (ord.OrderStatusId > 0)
                    {
                        if (orderStatuses.Count > 0)
                        {
                            ord.OrderStatusName = orderStatuses.Where(x => x.OrderStatusId == ord.OrderStatusId).First().OrderStatusName;
                        }
                        else
                        {
                            ord.OrderStatusName = "None";
                        }
                    }
                    else
                    {
                        ord.OrderStatusName = "None";
                    }

                    if (ord.JobId > 0)
                    {
                        if (jobs.Count > 0)
                        {
                            ord.JobName = jobs.Where(x => x.JobId == ord.JobId).First().Name;
                        }
                        else
                        {
                            ord.JobName = "None";
                        }
                    }
                    else
                    {
                        ord.JobName = "None";
                    }
                }
            }

            dgData.ItemsSource = orders;
        }

        public Order GetSelectedOrder()
        {
            Order order = new Order();

            if (dgData.SelectedItems.Count > 0)
            {
                order = (Order)dgData.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Please select an order from the Order Grid to work with", "Information");
            }
            return order;
        }

        public void OrdersSearch(SearchWindow window)
        {
            string name = window.TxtSearchOrderName.Text;
            string price = window.TxtSearchOrderPrice.Text;
            string quantity = window.TxtSearchOrderQuantity.Text;
            string statusId = window.CbOrderStatusId.Text;

            List<Order> order = new OrderCRUD().GetOrders();

            if (!String.IsNullOrEmpty(name))
            {
                order.RemoveAll(x => x.Name.ToLower() != name.ToLower());
            }

            if (!String.IsNullOrEmpty(price))
            {
                order.RemoveAll(x => x.Price != Convert.ToDecimal(price));
            }

            if (!String.IsNullOrEmpty(quantity))
            {
                order.RemoveAll(x => x.Quantity != Convert.ToInt32(quantity));
            }

            if (!String.IsNullOrEmpty(statusId))
            {
                order.RemoveAll(x => x.OrderStatusId != Convert.ToInt32(statusId));
            }

            if (order.Count > 0)
            {
                BindDataGrid(order);
                window.Close();
            }
            else
            {
                MessageBox.Show("There are no results", "Information");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Views.SearchWindow window = new SearchWindow("2");
            window.Show();
            window.BtnGoSearch.Click += (s, ev) =>
            {
                OrdersSearch(window);
            };
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = dgData;
            dg.SelectionMode = DataGridSelectionMode.Extended;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            StreamWriter swObj = new StreamWriter("exportToExcel.csv");
            swObj.WriteLine(Clipboardresult);
            swObj.Close();
            Process.Start("exportToExcel.csv");

            dg.SelectionMode = DataGridSelectionMode.Extended;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Orders.Create window = new Orders.Create();
            window.Show();
            window.Closing += (s, ev) =>
            {
                BindDataGrid(null);
            };
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedOrder().OrderId > 0)
            {
                Orders.Read window = new Orders.Read(GetSelectedOrder());
                window.Show();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedOrder().OrderId > 0)
            {
                Orders.Update window = new Orders.Update(GetSelectedOrder());
                window.Show();
                window.Closing += (s, ev) =>
                {
                    BindDataGrid(null);
                };
            }
        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;

            if (GetSelectedOrder().OrderId > 0)
            {
                result = new OrderCRUD().Destroy(GetSelectedOrder().OrderId);
            }

            if (result > 0)
            {
                BindDataGrid(null);
            }
            else
            {
                MessageBox.Show("Order was deleted", "Success");
            }
        }
    }
}
