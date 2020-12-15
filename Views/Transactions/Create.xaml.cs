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

namespace BusinessSuiteByVesune.Views.Transactions
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            this.DataContext = new Transaction();

            CbJob.ItemsSource = new JobCRUD().GetJobs();
            CbJob.DisplayMemberPath = "Name";
            CbJob.SelectedValuePath = "JobId";

            CbUsers.ItemsSource = new UserCRUD().GetUsers();
            CbUsers.DisplayMemberPath = "Name";
            CbUsers.SelectedValuePath = "UserId";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = (Transaction)this.DataContext;
            int result = new TransactionCRUD().Create(transaction);
            if (result > 0)
            {
                MessageBox.Show("Transaction was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create transaction", "Failure");
            }
        }
    }
}
