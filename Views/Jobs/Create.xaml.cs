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
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            
            Job newJob = new Job();
            newJob.ScheduledDate = DateTime.Now;

            this.DataContext = newJob;

            cbJobType.ItemsSource = new JobTypeCRUD().GetJobTypes();
            cbJobType.SelectedValuePath = "JobTypeId";
            cbJobType.DisplayMemberPath = "JobTypeName";

            cbJobStatusType.ItemsSource = new JobTypeCRUD().GetJobStatusTypes();
            cbJobStatusType.SelectedValuePath = "JobStatusTypeId";
            cbJobStatusType.DisplayMemberPath = "JobStatusTypeName";
        }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Job currentJob = (Job)this.DataContext;
            int result = new JobCRUD().Create(currentJob);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Job was created", "Success");
            }
            else
            {
                MessageBox.Show("Failed to create job", "Failure");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
