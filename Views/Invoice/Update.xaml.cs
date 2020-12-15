using BusinessSuiteByVesune.CRUD;
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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
        }

        public Update(Objects.Invoice invoice)
        {
            InitializeComponent();
            this.DataContext = invoice;

            cbJobs.ItemsSource = new JobCRUD().GetJobs();
            cbJobs.DisplayMemberPath = "Name";
            cbJobs.SelectedValuePath = "JobId";

            cbInvoiceType.ItemsSource = new InvoiceCRUD().GetInvoiceTypes();
            cbInvoiceType.DisplayMemberPath = "InvoiceTypeName";
            cbInvoiceType.SelectedValuePath = "InvoiceTypeId";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new InvoiceCRUD().Update((Objects.Invoice)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Invoice was updated", "Success");
            }
            else
            {
                MessageBox.Show("Unable to update invoice", "Failure");
            }
        }
    }
}
