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

namespace BusinessSuiteByVesune.Views.Users
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            this.DataContext = new User();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            result = new UserCRUD().Create((Objects.User)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("User was created", "Success");
            }
            else
            {
                MessageBox.Show("Unable to create user", "Failure");
            }
        }

        public bool IsValidForm()
        {
            if (TxtName.Text.Length == 0)
            {
                TxtName.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Enter the new user's name", "Notice");
                return false;
            }

            if (TxtPin.Text.Length == 0)
            {
                TxtName.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Enter the new user's pin", "Notice");
                return false;
            }

            if (TxtWorkRate.Text.Length == 0)
            {
                TxtName.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Enter the new user's work rate or enter 0 if no rate is to be added", "Notice");
                return false;
            }

            return true;
        }
    }
}
