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

            // here

            // check meridiems

            //// this is start time
            if (period.StartMeridiem == 1)
            { 
                period.StartHour += 12;
            }

            if (period.EndMeridiem == 1)
            {
                period.EndHour += 12;
            }

            period.Start = period.Start.AddHours(period.StartHour).AddMinutes(period.StartMinute);

            try
            {
                period.End = Convert.ToDateTime(period.End).AddHours(period.EndHour).AddMinutes(period.EndMinute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }

            // to here

            result = new WorkPeriodCRUD().Create(period);

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
            TimeSpan end = new TimeSpan(endHour, endMinute, 0);

            TimeSpan total = end.Subtract(start);

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

            

            LblTotal.Content = result;

        }
    }
}
