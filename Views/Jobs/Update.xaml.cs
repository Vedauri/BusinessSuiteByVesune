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

namespace BusinessSuiteByVesune.Views.Jobs
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

        public Update(Job job)
        {
            InitializeComponent();
            this.DataContext = job;

            cbJobType.ItemsSource = new JobTypeCRUD().GetJobTypes();
            cbJobType.SelectedValuePath = "JobTypeId";
            cbJobType.DisplayMemberPath = "JobTypeName";

            cbJobStatusType.ItemsSource = new JobTypeCRUD().GetJobStatusTypes();
            cbJobStatusType.SelectedValuePath = "JobStatusTypeId";
            cbJobStatusType.DisplayMemberPath = "JobStatusTypeName";
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = new JobCRUD().Update((Job)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Job was updated", "Success");
            }
            else
            {
                MessageBox.Show("Job failed to update", "Failure");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
