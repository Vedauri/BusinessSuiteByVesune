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

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            BindDataGrids();
        }

        public void BindDataGrids()
        {
            dgItemTypes.ItemsSource = new ItemTypeCRUD().GetItemTypes();
            dgJobTypes.ItemsSource = new JobTypeCRUD().GetJobTypes();
            dgLocationTypes.ItemsSource = new LocationTypeCRUD().GetLocationTypes();
        }

        public ItemType GetSelectedItemType()
        {
            ItemType selectedItemType = new ItemType();

            if (dgItemTypes.SelectedItems.Count > 0)
            {
                selectedItemType = (ItemType)dgItemTypes.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select an Item from the Item Types grid to work with", "Information");
            }

            return selectedItemType;
        }

        public JobType GetSelectedJobType()
        {
            JobType selectedJobType = new JobType();

            if (dgJobTypes.SelectedItems.Count > 0)
            {
                selectedJobType = (JobType)dgJobTypes.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select a Job from the Job Types grid to work with", "Information");
            }

            return selectedJobType;
        }

        public LocationType GetSelectedLocationType()
        {
            LocationType selectedLocationType = new LocationType();

            if (dgLocationTypes.SelectedItems.Count > 0)
            {
                selectedLocationType = (LocationType)dgLocationTypes.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select a Location from the Location Types grid to work with", "Information");
            }

            return selectedLocationType;
        }

        private void BtnCreateItemType_Click(object sender, RoutedEventArgs e)
        {
            ItemTypes.CreateItemTypeWindow window = new ItemTypes.CreateItemTypeWindow();
            window.Show();
            window.Closing += (s, ev) =>
            {
                BindDataGrids();
            };
        }

        private void BtnUpdateItemType_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedItemType().ItemTypeId > 0)
            {
                ItemTypes.UpdateItemTypeWindow window = new ItemTypes.UpdateItemTypeWindow(GetSelectedItemType());
                window.Show();
                window.Closing += (s, ev) =>
                {
                    BindDataGrids();
                };
            }
        }

        private void BtnCreateJobType_Click(object sender, RoutedEventArgs e)
        {
            JobTypes.CreateJobTypeWindow window = new JobTypes.CreateJobTypeWindow();
            window.Show();
            window.Closing += (s, ev) =>
            {
                BindDataGrids();
            };
        }

        private void BtnUpdateJobType_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedJobType().JobTypeId > 0)
            {
                JobTypes.UpdateJobTypeWindow window = new JobTypes.UpdateJobTypeWindow(GetSelectedJobType());
                window.Show();
                window.Closing += (s, ev) =>
                {
                    BindDataGrids();
                };
            }
        }

        private void BtnCreateLocationType_Click(object sender, RoutedEventArgs e)
        {
            LocationTypes.CreateLocationType window = new LocationTypes.CreateLocationType();
            window.Show();
            window.Closing += (s, ev) =>
            {
                BindDataGrids();
            };
        }

        private void BtnUpdateLocationType_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedLocationType().LocationTypeId > 0)
            {
                LocationTypes.UpdateLocationType window = new LocationTypes.UpdateLocationType(GetSelectedLocationType());
                window.Show();
                window.Closing += (s, ev) =>
                {
                    BindDataGrids();
                };
            }
        }
    }
}
