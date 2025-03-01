using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using static Newtonsoft.Json.Formatting;

namespace SportRent
{
    public partial class InventoryWindow : Window
    {
        private List<InventoryItem> inventoryItems;

        public InventoryWindow()
        {
            InitializeComponent();
            LoadInventory();
        }

        public class InventoryItem
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        private void LoadInventory()
        {
            string filePath = "inventory.json";

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                inventoryItems = JsonConvert.DeserializeObject<List<InventoryItem>>(jsonData);
            }

            InventoryDataGrid.ItemsSource = inventoryItems;
        }

        private void SaveInventory()
        {
            string filePath = "inventory.json";

            string jsonData = JsonConvert.SerializeObject(inventoryItems, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);
        }


        private void InventoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem != null)
            {
                DeleteButton.IsEnabled = true;
                BookButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
                BookButton.IsEnabled = false;
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();

            if (addItemWindow.ShowDialog() == true)
            {
                InventoryItem newItem = new InventoryItem
                {
                    Name = addItemWindow.ItemName,
                    Price = addItemWindow.ItemPrice,
                    Quantity = addItemWindow.ItemQuantity
                };

                inventoryItems.Add(newItem);
                InventoryDataGrid.ItemsSource = null;
                InventoryDataGrid.ItemsSource = inventoryItems;

                SaveInventory();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem != null)
            {
                inventoryItems.Remove(InventoryDataGrid.SelectedItem as InventoryItem);
                InventoryDataGrid.ItemsSource = null;
                InventoryDataGrid.ItemsSource = inventoryItems;

                DeleteButton.IsEnabled = false;
                BookButton.IsEnabled = false;

                SaveInventory(); 
            }
        }

        private void BookItem_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem != null)
            {
                InventoryItem selectedItem = (InventoryItem)InventoryDataGrid.SelectedItem;
                BookingWindow bookingWindow = new BookingWindow(selectedItem);
                bookingWindow.ShowDialog();
            }
        }

    }
}
