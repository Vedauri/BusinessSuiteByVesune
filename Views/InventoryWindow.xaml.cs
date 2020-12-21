using BusinessSuiteByVesune.CRUD;
using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BusinessSuiteByVesune.Views
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            InitializeComponent();
            BindDataGrid();
        }

        public void BindDataGrid()
        {
            List<Item> items = new ItemCRUD().GetAllItems();
            List<ItemType> itemTypes = new ItemTypeCRUD().GetItemTypes();
            List<LocationType> locationType = new LocationTypeCRUD().GetLocationTypes();

            foreach (var item in items)
            {
                if (item.ItemTypeId > 0)
                {
                    item.ItemTypeName = itemTypes.Where(x => x.ItemTypeId == item.ItemTypeId).First().ItemTypeName;
                }
                else
                {
                    item.ItemTypeName = "None";
                }

                if (item.LocationTypeId > 0)
                {
                    item.LocationTypeName = locationType.Where(x => x.LocationTypeId == item.LocationTypeId).First().LocationTypeName;
                }
                else
                {
                    item.LocationTypeName = "None";
                }
            }

            this.dgData.ItemsSource = items;
        }

        public void BindDataGrid(List<Item> items)
        {
            this.dgData.ItemsSource = items;
        }

        public bool DataSelected()
        {
            if (dgData.SelectedItems.Count > 0)
            {
                Item currentItem = (Item)dgData.SelectedItems[0];

                return currentItem.ItemId > 0;
            }
            else
            {
                MessageBox.Show("Select an item from the grid to work with", "Notice");
                return false;
            }
        }

        public void InventorySearch(SearchWindow window)
        {

            string name = window.TxtSearchItemName.Text;
            string price = window.TxtSearchItemPrice.Text;
            string quantity = window.TxtSearchItemQuantity.Text;
            string skuNumber = window.TxtSearchItemSkuNumber.Text;
            string itemTypeId = window.CbSearchItemItemTypeId.Text;
            string locationTypeId = window.CbSearchItemLocationTypeId.Text;

            List<Item> items = new ItemCRUD().GetAllItems();

            if (!String.IsNullOrEmpty(name))
            {
                items.RemoveAll(x => x.Name.ToLower() != name.ToLower());
            }

            if (!String.IsNullOrEmpty(price))
            {
                items.RemoveAll(x => x.Price != Convert.ToDecimal(price));
            }

            if (!String.IsNullOrEmpty(quantity))
            {
                items.RemoveAll(x => x.Quantity != Convert.ToInt32(quantity));
            }

            if (!String.IsNullOrEmpty(skuNumber))
            {
                items.RemoveAll(x => x.SkuNumber != skuNumber);
            }

            if (!String.IsNullOrEmpty(itemTypeId))
            {
                items.RemoveAll(x => x.ItemTypeId != Convert.ToInt32(itemTypeId));
            }

            if (!String.IsNullOrEmpty(locationTypeId))
            {
                items.RemoveAll(x => x.LocationTypeId != Convert.ToInt32(locationTypeId));
            }

            if (items.Count > 0)
            {
                BindDataGrid(items);
                window.Close();
            }
            else
            {
                MessageBox.Show("There are no results", "Information");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.Items.Count == 0)
            {
                MessageBox.Show("There are no records to search", "Information");
                return;
            }

            Views.SearchWindow window = new SearchWindow("1");
            window.Show();
            window.BtnGoSearch.Click += (s, ev) =>
            {
                InventorySearch(window);
            };
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

            dg.SelectionMode = DataGridSelectionMode.Single;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Inventory.Create window = new Inventory.Create();
            window.Show();
            window.Closing += (s, ev) =>
            {
                this.BindDataGrid();
            };
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            if (DataSelected())
            {
                Item selectedItem = (Item)dgData.SelectedItems[0];
                new Inventory.Read(selectedItem).Show();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DataSelected())
            {
                Item selectedItem = (Item)dgData.SelectedItems[0];
                Inventory.Update window = new Inventory.Update(selectedItem);
                window.Show();
                window.Closing += (s, ev) =>
                {
                    this.BindDataGrid();
                };
            }
        }

        private void BtnDestroy_Click(object sender, RoutedEventArgs e)
        {
            if (DataSelected())
            {
                Item selectedItem = (Item)dgData.SelectedItems[0];
                new ItemCRUD().Destroy(selectedItem.ItemId);
                this.BindDataGrid();
            }
        }
    }
}
