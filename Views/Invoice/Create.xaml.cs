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

namespace BusinessSuiteByVesune.Views.Invoice
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            Objects.Invoice invoice = new Objects.Invoice();
            this.DataContext = invoice;

            cbJobs.ItemsSource = new JobCRUD().GetJobs();
            cbJobs.DisplayMemberPath = "Name";
            cbJobs.SelectedValuePath = "JobId";

            cbInvoiceType.ItemsSource = new InvoiceCRUD().GetInvoiceTypes();
            cbInvoiceType.DisplayMemberPath = "InvoiceTypeName";
            cbInvoiceType.SelectedValuePath = "InvoiceTypeId";

            // move these to read and update

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new InvoiceCRUD().Create((Objects.Invoice)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Invoice was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create invoice", "Failure");
            }
        }
    }
}
