using BusinessSuiteByVesune.Objects;
using BusinessSuiteByVesune.CRUD;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow(string searchType)
        {
            InitializeComponent();

            this.TxtSearchType.Text = searchType;
            this.DataContext = new Search();

            if (searchType.Length > 0)
            {
                LoadDataSources();
            }

            switch (searchType)
            {
                case "1":
                    gInventory.Visibility = Visibility.Visible;
                    break;
                case "2":
                    gOrders.Visibility = Visibility.Visible;
                    break;
                case "3":
                    gJobs.Visibility = Visibility.Visible;
                    break;
                case "4":
                    gUsers.Visibility = Visibility.Visible;
                    break;
                case "5":
                    gInvoice.Visibility = Visibility.Visible;
                    break;
                case "6":
                    gTimes.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public void LoadDataSources()
        {
            CbOrderStatusId.ItemsSource = new OrderCRUD().GetOrderStatuses();
            CbOrderStatusId.DisplayMemberPath = "OrderStatusName";
            CbOrderStatusId.SelectedValuePath = "OrderStatusId";

            CbSearchInvoiceStatus.ItemsSource = new InvoiceCRUD().GetInvoiceTypes();
            CbSearchInvoiceStatus.DisplayMemberPath = "InvoiceTypeName";
            CbSearchInvoiceStatus.SelectedValuePath = "InvoiceTypeId";

            CbSearchItemItemTypeId.ItemsSource = new ItemTypeCRUD().GetItemTypes();
            CbSearchItemItemTypeId.DisplayMemberPath = "ItemTypeName";
            CbSearchItemItemTypeId.SelectedValuePath = "ItemTypeId";
            
            CbSearchItemLocationTypeId.ItemsSource = new LocationTypeCRUD().GetLocationTypes();
            CbSearchItemLocationTypeId.DisplayMemberPath = "LocationTypeName";
            CbSearchItemLocationTypeId.SelectedValuePath = "LocationTypeId";

            CbSearchJobJobStatusId.ItemsSource = new JobTypeCRUD().GetJobStatusTypes();
            CbSearchJobJobStatusId.DisplayMemberPath = "JobStatusTypeName";
            CbSearchJobJobStatusId.SelectedValuePath = "JobStatusTypeId";

            CbSearchJobJobTypeId.ItemsSource = new JobTypeCRUD().GetJobTypes();
            CbSearchJobJobTypeId.DisplayMemberPath = "JobTypeName";
            CbSearchJobJobTypeId.SelectedValuePath = "JobTypeId";

            CbSearchTimesUserId.ItemsSource = new UserCRUD().GetUsers();
            CbSearchTimesUserId.DisplayMemberPath = "Name";
            CbSearchTimesUserId.SelectedValuePath = "UserId";
        }

        private void BtnCancelSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnGoSearch_Click(object sender, RoutedEventArgs e)
        {
            // handled in parent window
        }
    }
}
