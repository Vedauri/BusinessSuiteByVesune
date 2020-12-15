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
    /// Interaction logic for CreateChargeWindow.xaml
    /// </summary>
    public partial class CreateChargeWindow : Window
    {
        public CreateChargeWindow()
        {
            InitializeComponent();
        }

        public CreateChargeWindow(int jobId)
        {
            InitializeComponent();
            Charge charge = new Charge();
            charge.JobId = jobId;
            this.DataContext = charge;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = new ChargeCRUD().Create((Charge)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Charge was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create charge", "Failure");
            }
        }
    }
}
