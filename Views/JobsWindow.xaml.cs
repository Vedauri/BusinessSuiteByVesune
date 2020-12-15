using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for JobsWindow.xaml
    /// </summary>
    public partial class JobsWindow : Window
    {
        public JobsWindow()
        {
            InitializeComponent();
            BindDataGrid(null);
        }

        public void BindDataGrid(List<Job> jobs)
        {
            if (jobs == null)
            {
                jobs = new JobCRUD().GetJobs();
            }


            List<JobStatusType> statusTypes = new JobTypeCRUD().GetJobStatusTypes();
            List<JobType> jobTypes = new JobTypeCRUD().GetJobTypes();

            foreach (var job in jobs)
            {
                if (job.JobStatusTypeId > 0)
                {
                    job.JobStatusTypeName = statusTypes.Where(x => x.JobStatusTypeId == job.JobStatusTypeId).First().JobStatusTypeName;
                }
                else
                {
                    job.JobStatusTypeName = "None";
                }

                if (job.JobTypeId > 0)
                {
                    job.JobTypeName = jobTypes.Where(x => x.JobTypeId == job.JobTypeId).First().JobTypeName;
                }
                else
                {
                    job.JobTypeName = "None";
                }
            }

            this.dgData.ItemsSource = jobs;
        }

        public Job GetSelectedJob()
        {
            Job selectedJob = new Job();
            if (dgData.SelectedItems.Count > 0)
            {
                selectedJob = (Job)dgData.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select a job from the Job Grid to work with", "Information");
            }

            return selectedJob;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Jobs.Create window = new Jobs.Create();
            window.Show();
            window.Closing += (s, ev) =>
            {
                this.BindDataGrid(null);
            };
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedJob().JobId > 0)
            {
                Jobs.Read window = new Jobs.Read(GetSelectedJob());
                window.Show();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedJob().JobId > 0)
            {
                Jobs.Update window = new Jobs.Update(GetSelectedJob());
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
            if (GetSelectedJob().JobId > 0)
            {
                MessageBoxResult mbResult = MessageBox.Show("Deleting this Job will remove all job relations to invoices, orders, time entries and job charges. Are you sure?", "Warning", MessageBoxButton.YesNo);
                if (mbResult == MessageBoxResult.Yes)
                {
                    result = new JobCRUD().Destroy(GetSelectedJob().JobId);

                    if (result > 0)
                    {
                        BindDataGrid(null);
                        MessageBox.Show("The job and it's charges were deleted. All invoices, orders and time periods are no longer assigned to this job", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete job and it's charges; something went wrong.", "Failure");
                    }

                }
            }
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

            dg.SelectionMode = DataGridSelectionMode.Single;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Views.SearchWindow window = new SearchWindow("3");
            window.Show();
            window.BtnGoSearch.Click += (s, ev) =>
            {
                JobSearch(window);
            };
        }

        public void JobSearch(SearchWindow window)
        {
            string name = window.TxtSearchJobName.Text;
            string balanceDue = window.TxtSearchJobBalanceDue.Text;
            string city = window.TxtSearchJobCity.Text;
            bool? mobile = window.ChkSearchJobMobile.IsChecked;
            string statusId = window.CbSearchJobJobStatusId.Text;
            string scheduledDate = window.DpSearchJobScheduledDate.Text;

            List<Job> jobs = new JobCRUD().GetJobs();

            if (!String.IsNullOrEmpty(name))
            {
                jobs.RemoveAll(x => x.Name.ToLower() != name.ToLower());
            }

            if (!String.IsNullOrEmpty(balanceDue))
            {
                jobs.RemoveAll(x => x.BalanceDue != Convert.ToDecimal(balanceDue));
            }

            if (!String.IsNullOrEmpty(city))
            {
                jobs.RemoveAll(x => x.City != city);
            }

            if (mobile != null)
            {
                jobs.RemoveAll(x => x.Mobile != mobile);
            }

            if (!String.IsNullOrEmpty(statusId))
            {
                jobs.RemoveAll(x => x.JobStatusTypeId != Convert.ToInt32(statusId));
            }

            if (scheduledDate.Length > 0)
            {
                jobs.RemoveAll(x => x.ScheduledDate != Convert.ToDateTime(scheduledDate));
            }

            if (jobs.Count > 0)
            {
                BindDataGrid(jobs);
                window.Close();
            }
            else
            {
                MessageBox.Show("There are no results", "Information");
            }
        }
    }
}
