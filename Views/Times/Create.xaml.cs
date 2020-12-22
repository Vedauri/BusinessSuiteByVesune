using BusinessSuiteByVesune.Objects;
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

namespace BusinessSuiteByVesune.Views.Times
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();

            WorkPeriod wp = new WorkPeriod()
            {
                Start = DateTime.Now,
                End = DateTime.Now,
            };

            this.DataContext = wp;

            CbUserId.ItemsSource = new UserCRUD().GetUsers();
            CbUserId.DisplayMemberPath = "Name";
            CbUserId.SelectedValuePath = "UserId";

            CbJobId.ItemsSource = new JobCRUD().GetJobs();
            CbJobId.DisplayMemberPath = "Name";
            CbJobId.SelectedValuePath = "JobId";

            List<CustomDropdownObject> meridiems = new MasterCRUD().GetMeridiems();

            CbStartMeridiem.ItemsSource = meridiems;
            CbStartMeridiem.DisplayMemberPath = "MemberPath";
            CbStartMeridiem.SelectedValuePath = "ValuePath";

            CbEndMeridiem.ItemsSource = meridiems;
            CbEndMeridiem.DisplayMemberPath = "MemberPath";
            CbEndMeridiem.SelectedValuePath = "ValuePath";
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            WorkPeriod period = (WorkPeriod)this.DataContext;

            if (period.UserId == 0)
            {
                MessageBox.Show("Select a user", "Notice");
                return;
            }

            if (period.Start == null)
            {
                MessageBox.Show("Select a start date", "Notice");
                return;
            }

            period.Start = period.Start.AddHours(period.StartHour).AddMinutes(period.StartMinute);

            if (Convert.ToBoolean(ChkClockedIn.IsChecked))
            {
                period.End = null;
            }
            else
            {
                if (period.End == null)
                {
                    MessageBox.Show("Select an end date", "Notice");
                    return;
                }

                if (period.Start.Date > Convert.ToDateTime(period.End).Date)
                {
                    // start date is later than the end date
                    MessageBox.Show("Start date is set after the end date. Please make sure the start date begins before end date", "Notice");
                    return;
                }

                if (period.Start.Date.Day != Convert.ToDateTime(period.End).Date.Day)
                {
                    MessageBox.Show("Unable to create a work period that exceeds one day. Please change the start and end date", "Notice");
                    return;
                }

                if (period.EndMeridiem == 1)
                {
                    period.EndHour += 12;
                }

                try
                {
                    period.End = Convert.ToDateTime(period.End).AddHours(period.EndHour).AddMinutes(period.EndMinute);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
            }

            if (period.StartMeridiem == 1)
            { 
                period.StartHour += 12;
            }


            // check if selected user is clocked in
            // if clocked in, clock him out of that current period, using the current time
            // then, clock him in to the current period by creating a new period
            // show dialouge saying that youre clocking him out and clocking him in to a new time period

            int selectedUserId = (int)CbUserId.SelectedValue;
            User currentUser = new UserCRUD().Read(selectedUserId);

            if (currentUser.Working && currentUser.WorkPeriodId > 0)
            {
                // this current user is working
                // do you want to clock them out and create new work period?
                MessageBoxResult messageBoxResult = MessageBox.Show("This user is currently clocked in. Creating this work period will clock them out of their current work period and create a new work period. Are you sure?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    // current work period end is now
                    WorkPeriod workPeriod = new WorkPeriodCRUD().Read(currentUser.WorkPeriodId);
                    workPeriod.End = DateTime.Now;
                    new WorkPeriodCRUD().Update(workPeriod);

                    // clock the user out
                    currentUser.WorkPeriodId = 0;
                    currentUser.Working = false;
                    int removeUserCurrentWorkPeriodId = new UserCRUD().Update(currentUser);
                }
            }

            result = new WorkPeriodCRUD().Create(period);

            if (period.End == null)
            {
                // update user to be clocked in to current period

                if (selectedUserId > 0 && result > 0)
                {
                    currentUser.WorkPeriodId = result;
                    currentUser.Working = true;

                    new UserCRUD().Update(currentUser);
                }
            }

            if (result > 0)
            {
                MessageBox.Show("Work period for user was created", "Success");
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to create work period for user", "Success");
            }
        }
        public void ReloadTotalTime_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnSubmit.IsEnabled = true;
            int startHour = 0;
            int endHour = 0;
            int startMinute = 0;
            int endMinute = 0;

            string result = "";

            int startMeridiem = Convert.ToInt32(CbStartMeridiem.SelectedValue);
            int endMeridiem = Convert.ToInt32(CbEndMeridiem.SelectedValue);

            try
            {
                startHour = String.IsNullOrEmpty(TxtStartHour.Text) == true ? 0 : Convert.ToInt32(TxtStartHour.Text);
                endHour = String.IsNullOrEmpty(TxtEndHour.Text) == true ? 0 : Convert.ToInt32(TxtEndHour.Text);
                startMinute = String.IsNullOrEmpty(TxtStartMinute.Text) == true ? 0 : Convert.ToInt32(TxtStartMinute.Text);
                endMinute = String.IsNullOrEmpty(TxtEndMinute.Text) == true ? 0 : Convert.ToInt32(TxtEndMinute.Text);
            }
            catch (Exception ex)
            {
                this.BtnSubmit.IsEnabled = false;
                MessageBox.Show(ex.Message.ToString(), "Error");
                return;
            }

            if (startHour > 12 || endHour > 12)
            {
                MessageBox.Show("Please check the start or end hour fields.", "Notice");
                return;
            }

            if (startMeridiem == 1)
            {
                startHour += 12;
            }

            if (endMeridiem == 1)
            {
                endHour += 12;
            }

            TimeSpan start = new TimeSpan(startHour, startMinute, 0);
            TimeSpan end = new TimeSpan(0, 0, 0);
            TimeSpan total = new TimeSpan(0, 0, 0);

            if (Convert.ToBoolean(ChkClockedIn.IsChecked))
            {
                // end date is null
                this.BtnSubmit.IsEnabled = true;
            }
            else
            {
                // end date is value
                end = new TimeSpan(endHour, endMinute, 0);
                total = end.Subtract(start);

                if (total.Hours < 0)
                {
                    this.BtnSubmit.IsEnabled = false;
                    result = total.Hours + " hours and " + total.Minutes + " minutes.";
                    LblTotal.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    this.BtnSubmit.IsEnabled = true;
                    result = total.Hours + " hours and " + total.Minutes + " minutes";
                    LblTotal.Foreground = new SolidColorBrush(Colors.Green);
                }
            }

            LblTotal.Content = result;

        }

        private void ChkClockedIn_Unchecked(object sender, RoutedEventArgs e)
        {
            TxtEndHour.IsEnabled = true;
            TxtEndMinute.IsEnabled = true;
            DpEndDate.IsEnabled = true;
            CbEndMeridiem.IsEnabled = true;
            LblTotal.Visibility = Visibility.Visible;
        }

        private void ChkClockedIn_Checked(object sender, RoutedEventArgs e)
        {
            TxtEndHour.IsEnabled = false;
            TxtEndMinute.IsEnabled = false;
            DpEndDate.IsEnabled = false;
            CbEndMeridiem.IsEnabled = false;
            LblTotal.Visibility = Visibility.Hidden;
            this.BtnSubmit.IsEnabled = true;
        }
    }
}
