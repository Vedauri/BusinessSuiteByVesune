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

namespace BusinessSuiteByVesune.Views.JobTypes
{
    /// <summary>
    /// Interaction logic for CreateJobTypeWindow.xaml
    /// </summary>
    public partial class CreateJobTypeWindow : Window
    {
        public CreateJobTypeWindow()
        {
            InitializeComponent();
            this.DataContext = new JobType();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new JobTypeCRUD().Create((JobType)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Job type was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create job type", "Failure");
            }
        }
    }
}
