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

namespace BusinessSuiteByVesune.Views.Charges
{
    /// <summary>
    /// Interaction logic for UpdateChargeWindow.xaml
    /// </summary>
    public partial class UpdateChargeWindow : Window
    {
        public UpdateChargeWindow(int chargeId, int jobId)
        {
            InitializeComponent();
            Charge charge = new ChargeCRUD().Read(chargeId, jobId);
            this.DataContext = charge;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Charge charge = (Charge)this.DataContext;
            int result = 0;

            try
            {
                result = new ChargeCRUD().Update(charge);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }

            if (result > 0)
            {
                MessageBox.Show("Charge was updated", "Success");
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to update charge", "Failure");
            }
        }
    }
}
