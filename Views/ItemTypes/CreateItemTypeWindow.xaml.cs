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

namespace BusinessSuiteByVesune.Views.ItemTypes
{
    /// <summary>
    /// Interaction logic for CreateItemTypeWindow.xaml
    /// </summary>
    public partial class CreateItemTypeWindow : Window
    {
        public CreateItemTypeWindow()
        {
            InitializeComponent();
            this.DataContext = new ItemType();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new ItemTypeCRUD().Create((ItemType)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Item type was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create Item Type", "Failure");
            }
        }
    }
}
