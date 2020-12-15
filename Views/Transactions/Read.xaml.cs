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
    /// Interaction logic for Read.xaml
    /// </summary>
    public partial class Read : Window
    {
        public Read()
        {
            InitializeComponent();
        }
        
        public Read(Transaction transaction)
        {
            InitializeComponent();
            LblJob.Content = new JobCRUD().GetJobs().Where(x => x.JobId == transaction.JobId).First().Name;
            LblUser.Content = new UserCRUD().GetUsers().Where(x => x.UserId == transaction.UserId).First().Name;
            this.DataContext = transaction;

        }
    }
}
