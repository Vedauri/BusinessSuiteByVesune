using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for TimesWindow.xaml
    /// </summary>
    public partial class TimesWindow : Window
    {
        public TimesWindow()
        {
            InitializeComponent();
            BindDataGrid(null);
        }

        public void BindDataGrid(List<WorkPeriod> periods)
        {
            dgData.ItemsSource = FormatDataGrid(periods);
        }

        public List<WorkPeriod> FormatDataGrid(List<WorkPeriod> periods)
        {
            if (periods == null)
            {
                periods = new WorkPeriodCRUD().GetWorkPeriods();
            }

            for (int i = 0; i < periods.Count; i++)
            {
                periods[i].UserName = new UserCRUD().Read(periods[i].UserId).Name;
                periods[i].JobName = new JobCRUD().Read(periods[i].JobId).Name;

                DateTime periodStartDate = periods[i].Start;
                if (periods[i].End != null)
                {
                    DateTime periodEndDate = (DateTime)periods[i].End;

                    // set fields
                    periods[i].WorkPeriodTime = periodEndDate.Subtract(periods[i].Start);
                    periods[i].WorkPeriodReadable = periods[i].WorkPeriodTime.Value.Hours + "Hours and " + periods[i].WorkPeriodTime.Value.Minutes + " minutes";
                    periods[i].StartHour = periods[i].Start.Hour;
                    periods[i].StartMinute = periods[i].Start.Minute;
                    periods[i].EndHour = periodEndDate.Hour;
                    periods[i].EndMinute = periodEndDate.Minute;
                }
                else
                {
                    // end time is null, they are currently clocked in
                    // set fields
                    periods[i].WorkPeriodTime = null;
                    periods[i].WorkPeriodReadable = "Clocked In";
                    periods[i].StartHour = periods[i].Start.Hour;
                    periods[i].StartMinute = periods[i].Start.Minute;
                    periods[i].EndHour = 0;
                    periods[i].EndMinute = 0;
                }
            }
            return periods;
        }

        public WorkPeriod GetSelectedWorkPeriod()
        {
            WorkPeriod period = new WorkPeriod();
            if (dgData.SelectedItems.Count > 0)
            {
                period = (WorkPeriod)dgData.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select a work period from the grid to work with", "Notice");
            }
            return period;
        }

        public void TimeSearch(SearchWindow window)
        {
            int selectedUserId = Convert.ToInt32(window.CbSearchTimesUserId.SelectedValue.ToString());
            DateTime? selectedStartDate = window.DpSearchTimesStartDate.SelectedDate;
            DateTime? selectedEndDate = window.DpSearchTimesEndDate.SelectedDate;

            List<WorkPeriod> periods = new WorkPeriodCRUD().GetWorkPeriods();

            if (selectedUserId > 0)
            {
                periods.RemoveAll(x => x.UserId != selectedUserId);
            }

            if (selectedStartDate != null)
            {
                periods.RemoveAll(x => x.Start >= Convert.ToDateTime(selectedStartDate));
            }

            if (selectedEndDate != null)
            {
                periods.RemoveAll(x => x.End <= Convert.ToDateTime(selectedEndDate));
            }

            if (periods.Count > 0)
            {
                BindDataGrid(periods);
                window.Close();
            }
            else
            {
                MessageBox.Show("There are no results", "Information");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Views.SearchWindow window = new SearchWindow("6");
            window.Show();
            window.BtnGoSearch.Click += (s, ev) =>
            {
                TimeSearch(window);
            };
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedWorkPeriod().End != null)
            {
                Times.Update window = new Times.Update((WorkPeriod)dgData.SelectedItems[0]);
                window.Show();
                window.BtnSubmit.Click += (s, ev) =>
                {
                    BindDataGrid(null);
                };
            }
            else
            {
                MessageBox.Show("Unable to edit a work period that is not clocked out. Please clock out the user then edit the work period.", "Notice");
            }
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedWorkPeriod().End != null)
            {
                new Times.Read((WorkPeriod)dgData.SelectedItems[0]).Show();
            }
            else
            {
                MessageBox.Show("Unable to edit a work period that is not clocked out. Please clock out the user then edit the work period.", "Notice");
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Times.Create window = new Times.Create();
            window.Show();
            window.BtnSubmit.Click += (s, ev) =>
            {
                BindDataGrid(null);
            };
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

            dg.SelectionMode = DataGridSelectionMode.Extended;
        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedWorkPeriod().End != null)
            {
                new WorkPeriodCRUD().Destroy(GetSelectedWorkPeriod().WorkPeriodId);
                BindDataGrid(null);
            }
            else
            {
                MessageBox.Show("Unable to delete a work period that is not clocked out. Please clock out the user then delete the work period.", "Notice");
            }
        }
    }
}
