using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System.Collections.Generic;
using System.Windows;

namespace BusinessSuiteByVesune.Views.Jobs
{
    /// <summary>
    /// Interaction logic for Read.xaml
    /// </summary>
    public partial class Read : Window
    {
        public int JobId;

        public Read()
        {
            InitializeComponent();
        }

        public Read(Job job)
        {
            InitializeComponent();
            this.DataContext = job;
            this.JobId = job.JobId;
            BindGridData(job.JobId);

            cbJobType.ItemsSource = new JobTypeCRUD().GetJobTypes();
            cbJobType.SelectedValuePath = "JobTypeId";
            cbJobType.DisplayMemberPath = "JobTypeName";

            cbJobStatusType.ItemsSource = new JobTypeCRUD().GetJobStatusTypes();
            cbJobStatusType.SelectedValuePath = "JobStatusTypeId";
            cbJobStatusType.DisplayMemberPath = "JobStatusTypeName";
        }

        public void BindGridData(int jobId)
        {
            this.dgChargesData.ItemsSource = new ChargeCRUD().GetChargesByJobId(jobId);
        }

        public Charge GetSelectedCharge()
        {
            Charge charge = new Charge();

            if (dgChargesData.SelectedItems.Count > 0)
            {
                charge = (Charge)dgChargesData.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Please select a charge from the grid to work with", "Notice");
            }
            return charge;
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            Charge charge = GetSelectedCharge();

            if (charge.ChargeId > 0)
            {
                Charges.ReadChargeWindow window = new Charges.ReadChargeWindow(charge.ChargeId, charge.JobId);
                window.Show();
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Charges.CreateChargeWindow window = new Charges.CreateChargeWindow(this.JobId);
            window.Show();
            window.Closing += (s, ev) =>
            {
                BindGridData(this.JobId);
            };
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Charge charge = GetSelectedCharge();
            if (charge.ChargeId > 0)
            {
                Charges.UpdateChargeWindow window = new Charges.UpdateChargeWindow(charge.ChargeId, charge.JobId);
                window.Show();
                window.BtnSubmit.Click += (s, ev) =>
                {
                    BindGridData(charge.JobId);
                };
            }
        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            Charge selectedCharge = GetSelectedCharge();

            if (selectedCharge.ChargeId > 0)
            {
                new ChargeCRUD().Destroy(selectedCharge.JobId, selectedCharge.ChargeId);
                BindGridData(this.JobId);
                MessageBox.Show("Charge was deleted", "Notice");
            }
            else
            {
                MessageBox.Show("Select a charge to delete", "Notice");
            }
        }
    }
}
