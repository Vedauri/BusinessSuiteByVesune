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

namespace BusinessSuiteByVesune.Views.Inventory
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
        }

        public Update(Item item)
        {
            InitializeComponent();
            this.DataContext = item;

            CbItemType.ItemsSource = new ItemTypeCRUD().GetItemTypes();
            CbItemType.DisplayMemberPath = "ItemTypeName";
            CbItemType.SelectedValuePath = "ItemTypeId";

            CbLocationTypes.ItemsSource = new LocationTypeCRUD().GetLocationTypes();
            CbLocationTypes.DisplayMemberPath = "LocationTypeName";
            CbLocationTypes.SelectedValuePath = "LocationTypeId";
        }

        public bool IsValidForm()
        {
            string errorText = "";

            if (String.IsNullOrEmpty(txtName.Text))
            {
                errorText += " The Name field is required.";
                return false;
            }

            if (String.IsNullOrEmpty(txtQuantity.Text))
            {
                errorText += " The Quantity field is required.";
                return false;
            }

            return true;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;

            if (this.IsValidForm())
            {
                result = new ItemCRUD().Update((Item)this.DataContext);
            }

            if (result > 0)
            {
                this.Close();
                MessageBox.Show("The item has been updated.", "Success");
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
