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

namespace BusinessSuiteByVesune.Views.Times
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

        public Update(WorkPeriod period)
        {
            InitializeComponent();

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

            if (period.StartHour > 12)
            {
                period.StartHour -= 12;
                period.StartMeridiem = 1;
            }

            if (period.EndHour > 12)
            {
                period.EndHour -= 12;
                period.EndMeridiem = 1;
            }

            this.DataContext = period;

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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            WorkPeriod period = (WorkPeriod)this.DataContext;

            if (period.StartMeridiem == 1)
            {
                period.StartHour += 12;
            }

            if (period.EndMeridiem == 1)
            {
                period.EndHour += 12;
            }

            DateTime start = new DateTime(period.Start.Year, period.Start.Month, period.Start.Day, period.StartHour, period.StartMinute, 0);
            period.Start = new DateTime(start.Year, start.Month, start.Day, period.StartHour, period.StartMinute, 0);

            try
            {
                DateTime end = Convert.ToDateTime(period.End);
                period.End = new DateTime(end.Year, end.Month, end.Day, period.EndHour, period.EndMinute, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }

            result = new WorkPeriodCRUD().Update(period);

            if (result > 0)
            {
                MessageBox.Show("Work period for user was updated", "Success");
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to update work period for user", "Success");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
