using System;
using System.Windows;

namespace SubscriptionManagementWPF
{
    public partial class AddSubscriptionWindow : Window
    {
        public Customer NewCustomer { get; private set; }

        public AddSubscriptionWindow()
        {
            InitializeComponent();
            // Set default dates to today
            ExpiryDatePicker.SelectedDate = DateTime.Now;
            LastActivityDatePicker.SelectedDate = DateTime.Now;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PackageTextBox.Text) ||
                !ExpiryDatePicker.SelectedDate.HasValue ||
                !LastActivityDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewCustomer = new Customer
            {
                Name = NameTextBox.Text.Trim(),
                SubscriptionPackage = PackageTextBox.Text.Trim(),
                SubscriptionExpiry = ExpiryDatePicker.SelectedDate.Value,
                LastActivity = LastActivityDatePicker.SelectedDate.Value
            };

            // Let the main window assign a new ID.
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
