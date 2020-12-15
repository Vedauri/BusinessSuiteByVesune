using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for WorkPeriodWindow.xaml
    /// </summary>
    public partial class WorkPeriodWindow : Window
    {
        public User CurrentUser = new User();

        public WorkPeriodWindow()
        {
            InitializeComponent();
        }

        public WorkPeriodWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            UpdateScreenUI(user.Working);
            this.DataContext = user;
        }

        public void UpdateScreenUI(bool working)
        {
            List<Job> jobs = new JobCRUD().GetJobs();

            if (working)
            {
                WorkPeriod wp = new WorkPeriodCRUD().GetWorkPeriodByUserIdAndWorkPeriodId(CurrentUser.UserId, CurrentUser.WorkPeriodId);

                this.GbJobComboBox.Visibility = Visibility.Hidden;
                this.GbJobLabel.Visibility = Visibility.Visible;

                this.LblJobName.Content = jobs.Where(x => x.JobId == wp.JobId).First().Name;

                this.BtnClock.Content = "Clock Out";
            }
            else
            {
                this.GbJobComboBox.Visibility = Visibility.Visible;
                this.GbJobLabel.Visibility = Visibility.Hidden;

                this.CbJobId.ItemsSource = jobs;
                this.CbJobId.DisplayMemberPath = "Name";
                this.CbJobId.SelectedValuePath = "JobId";

                this.BtnClock.Content = "Clock In";
            }
        }

        private void BtnClock_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            User currentUser = (User)this.DataContext;

            if (currentUser.WorkPeriodId > 0)
            {
                // clock out
                WorkPeriodCRUD crud = new WorkPeriodCRUD();
                WorkPeriod period = crud.Read(currentUser.WorkPeriodId);
                period.End = DateTime.Now;
                int clockOutIsComplete = crud.Update(period);

                if (clockOutIsComplete > 0)
                {
                    currentUser.WorkPeriodId = 0;
                    currentUser.Working = false;
                    new UserCRUD().Update(currentUser);
                }
                UpdateScreenUI(false);
            }
            else
            {
                if (CbJobId.SelectedItem != null)
                {
                    // clock in
                    WorkPeriod period = new WorkPeriod();
                    try
                    { 
                        period.UserId = Convert.ToInt32(TxtUserId.Text);
                        period.Start = DateTime.Now;
                        period.End = null;
                        period.JobId = Convert.ToInt32(CbJobId.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Select a job to work on", "Notice");
                        return;
                    }

                    result = new WorkPeriodCRUD().Create(period);
                    this.TxtWorkPeriodId.Text = result.ToString();

                    currentUser.WorkPeriodId = result;
                    currentUser.Working = true;
                    new UserCRUD().Update(currentUser);

                    UpdateScreenUI(true);
                }
                else
                {
                    MessageBox.Show("Select a job to work on", "Notice");
                }
            }

            
        }
    }
}
