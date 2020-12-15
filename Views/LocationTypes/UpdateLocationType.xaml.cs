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
    /// Interaction logic for UpdateLocationType.xaml
    /// </summary>
    public partial class UpdateLocationType : Window
    {
        public UpdateLocationType()
        {
            InitializeComponent();
        }

        public UpdateLocationType(LocationType locationType)
        {
            InitializeComponent();
            this.DataContext = locationType;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new LocationTypeCRUD().Update((LocationType)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Location type was updated", "Success");
            }
            else
            {
                MessageBox.Show("Unable to update location type", "Failure");
            }
        }
    }
}
