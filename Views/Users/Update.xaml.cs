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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
        }

        public Update(User user)
        {
            InitializeComponent();
            this.DataContext = user;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int result = new UserCRUD().Update((User)this.DataContext);
            if (result > 0)
            {
                this.Close();
                MessageBox.Show("User was updated", "Success");
            }
            else
            {
                MessageBox.Show("Unable to updated user", "Failure");
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
