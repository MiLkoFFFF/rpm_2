using System;
using System.Windows;
using static SportRent.InventoryWindow;

namespace SportRent
{
    public partial class BookingWindow : Window
    {
        private InventoryItem selectedItem;

        public BookingWindow(InventoryItem item)
        {
            InitializeComponent();
            selectedItem = item;
            ItemNameTextBlock.Text = $"Товар: {selectedItem.Name}";
            UpdateCost();
        }

        private void HoursTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateCost();
        }

        private void UpdateCost()
        {
            if (int.TryParse(HoursTextBox.Text, out int hours) && hours > 0)
            {
                decimal totalCost = selectedItem.Price * hours;
                TotalCostTextBlock.Text = $"Стоимость: {totalCost:C}";
            }
            else
            {
                TotalCostTextBlock.Text = "Стоимость: 0.00";
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Вы забронировали: {selectedItem.Name} на {HoursTextBox.Text} часов. Общая стоимость: {TotalCostTextBlock.Text}");
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
