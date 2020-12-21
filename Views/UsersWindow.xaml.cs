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
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            BindDataGrid(null);
        }
        
        public string GetWeeklyWorkPeriodReadableForUser(User user)
        {
            DateTime startOfWeek = DateTime.Now;
            DateTime endOfWeek = DateTime.Now;
            string readable = "";

            // get start of week
            while (startOfWeek.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                startOfWeek = startOfWeek.AddDays(-1);

            // get end of week
            while (endOfWeek.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                endOfWeek = endOfWeek.AddDays(1);

            // pull work periods between start and end of week
            List<WorkPeriod> workPeriods = new WorkPeriodCRUD().GetWorkPeriodsByUserId(user.UserId, startOfWeek, endOfWeek);

            if (workPeriods.Count > 0)
            {
                // remove any unfinished work periods
                workPeriods.RemoveAll(x => x.End == null);

                // if any work periods are left...
                if (workPeriods.Count > 0)
                {
                    TimeSpan total = new TimeSpan();

                    // look through the work periods...
                    foreach (WorkPeriod period in workPeriods)
                    {
                        // subtract the end date from the start date to get total hours in the work period
                        TimeSpan span = Convert.ToDateTime(period.End).Subtract(period.Start);
                        // add that result to the total timespan
                        total += span;
                    }

                    readable = total.Hours + " hours and " + total.Minutes + " minutes";
                }
                else
                {
                    readable = "No Hours, Clocked In";
                }
            }
            else
            {
                readable = "No Hours";
            }

            return readable;
        }

        public void BindDataGrid(List<User> users)
        {
            if (users == null)
            {
                users = new UserCRUD().GetUsers();
            }

            if (users.Count > 0)
            {
                foreach (User user in users)
                {
                    user.WeekHoursReadable = GetWeeklyWorkPeriodReadableForUser(user);
                }
            }

            dgData.ItemsSource = users;
        }

        public User GetSelectedUser()
        {
            User user = new User();
            if (dgData.SelectedItems.Count > 0)
            {
                user = (User)dgData.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select a user from the user grid to work with", "Information");
            }
            return user;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = dgData;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            StreamWriter swObj = new StreamWriter("exportToExcel.csv");
            swObj.WriteLine(Clipboardresult);
            swObj.Close();
            Process.Start("exportToExcel.csv");
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Users.Create window = new Users.Create();
            window.Show();
            window.Closing += (s, ev) =>
            {
                BindDataGrid(null);
            };
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedUser().UserId > 0)
            {
                Users.Read window = new Users.Read(GetSelectedUser());
                window.Show();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedUser().UserId > 0)
            {
                Users.Update window = new Users.Update(GetSelectedUser());
                window.Show();
                window.Closing += (s, ev) =>
                {
                    BindDataGrid(null);
                };
            }
        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this user? This will also delete all of their time entries.", "Warning", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // delete user
                int result = new UserCRUD().Destroy(GetSelectedUser().UserId);

                if (result > 0)
                {
                    MessageBox.Show("User was deleted", "Success");
                    // delete user time entires
                    int deleteEntriesResult = new WorkPeriodCRUD().DestroyAllWorkPeriodsByUserId(GetSelectedUser().UserId);

                    BindDataGrid(null);
                }
                else
                {
                    MessageBox.Show("Unable to delete user", "Failure");
                }

            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.Items.Count == 0)
            {
                MessageBox.Show("There are no records to search", "Information");
                return;
            }

            Views.SearchWindow window = new SearchWindow("4");
            window.Show();
            window.BtnGoSearch.Click += (s, ev) =>
            {
                UserSearch(window);
            };
        }

        public void UserSearch(SearchWindow window)
        {
            string name = window.TxtSearchUserName.Text;
            string pin = window.TxtSearchUserPin.Text;
            bool? working = window.ChkSearchUserWorking.IsChecked;

            List<User> users = new UserCRUD().GetUsers();

            if (!String.IsNullOrEmpty(name))
            {
                users.RemoveAll(x => x.Name.ToLower() != name.ToLower());
            }

            if (!String.IsNullOrEmpty(pin))
            {
                users.RemoveAll(x => x.Pin != pin);
            }

            if (working != null)
            {
                users.RemoveAll(x => x.Working != working);
            }

            if (users.Count > 0)
            {
                BindDataGrid(users);
                window.Close();
            }
            else
            {
                MessageBox.Show("There are no results", "Information");
            }
        }
    }
}
