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
    /// Interaction logic for ReadChargeWindow.xaml
    /// </summary>
    public partial class ReadChargeWindow : Window
    {
        public ReadChargeWindow(int chargeId, int jobId)
        {
            InitializeComponent();
            Charge charge = new ChargeCRUD().Read(chargeId, jobId);
            this.DataContext = charge;
        }
    }
}
