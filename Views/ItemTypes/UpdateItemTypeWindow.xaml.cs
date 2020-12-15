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
    /// Interaction logic for UpdateItemTypeWindow.xaml
    /// </summary>
    public partial class UpdateItemTypeWindow : Window
    {
        public UpdateItemTypeWindow()
        {
            InitializeComponent();
        }

        public UpdateItemTypeWindow(ItemType itemType)
        {
            InitializeComponent();
            this.DataContext = itemType;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new ItemTypeCRUD().Update((ItemType)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("Item type was updated", "Success");
            }
            else
            {
                MessageBox.Show("Unable to update Item Type", "Failure");
            }
        }
    }
}
