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

namespace BusinessSuiteByVesune.Views.LocationTypes
{
    /// <summary>
    /// Interaction logic for CreateLocationType.xaml
    /// </summary>
    public partial class CreateLocationType : Window
    {
        public CreateLocationType()
        {
            InitializeComponent();
            this.DataContext = new LocationType();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new LocationTypeCRUD().Create((LocationType)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Location type was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create location type", "Failure");
            }
        }
    }
}
