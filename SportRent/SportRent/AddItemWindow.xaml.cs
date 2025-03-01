using System.Windows;

namespace SportRent
{
    public partial class AddItemWindow : Window
    {
        public string ItemName { get; private set; }
        public decimal ItemPrice { get; private set; }
        public int ItemQuantity { get; private set; }

        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ItemPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(ItemQuantityTextBox.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(ItemPriceTextBox.Text, out decimal price) ||
                !int.TryParse(ItemQuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Стоимость и количество должны быть числовыми значениями.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ItemName = ItemNameTextBox.Text;
            ItemPrice = price;
            ItemQuantity = quantity;

            DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
