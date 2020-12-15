using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public TransactionsWindow()
        {
            InitializeComponent();
            BindDataGrid();
        }

        public Transaction GetSelectedTransaction()
        {
            Transaction transaction = new Transaction();
            if (dgData.SelectedItems.Count > 0)
            {
                transaction = (Transaction)dgData.SelectedItems[0];
            }
            else
            {
                MessageBox.Show("Select a transaction from the transaction grid to work with", "Notice");
            }
            return transaction;
        }

        public void BindDataGrid()
        {
            List<Transaction> transactions = new TransactionCRUD().GetTransactions();

            if (transactions.Count > 0)
            {
                foreach (Transaction trans in transactions)
                {
                    if (trans.JobId > 0)
                    {
                        trans.JobName = new JobCRUD().GetJobs().Where(x => x.JobId == trans.JobId).First().Name;
                    }
                    else
                    {
                        trans.JobName = "";
                    }

                    if (trans.UserId > 0)
                    {
                        trans.UserName = new UserCRUD().GetUsers().Where(x => x.UserId == trans.UserId).First().Name;
                    }
                    else
                    {
                        trans.UserName = "";
                    }
                }
            }

            dgData.ItemsSource = transactions;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = dgData;
            dg.SelectionMode = DataGridSelectionMode.Extended;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            StreamWriter swObj = new StreamWriter("exportToExcel.csv");
            swObj.WriteLine(Clipboardresult);
            swObj.Close();
            Process.Start("exportToExcel.csv");

            dg.SelectionMode = DataGridSelectionMode.Extended;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Views.Transactions.Create window = new Transactions.Create();
            window.Show();
            window.BtnSubmit.Click += (s, ev) =>
            {
                BindDataGrid();
            };
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = GetSelectedTransaction();

            if (transaction.TransactionId > 0)
            {
                Views.Transactions.Read window = new Transactions.Read(transaction);
                window.Show();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = GetSelectedTransaction();

            if (transaction.TransactionId > 0)
            {
                Views.Transactions.Update window = new Transactions.Update(transaction.TransactionId);
                window.Show();
                window.BtnSubmit.Click += (s, ev) =>
                {
                    BindDataGrid();
                };
            }
        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = GetSelectedTransaction();

            if (transaction.TransactionId > 0)
            {
                new TransactionCRUD().Destroy(transaction.TransactionId);
                BindDataGrid();
            }
        }
    }
}
