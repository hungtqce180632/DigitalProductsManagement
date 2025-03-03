using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SubscriptionManagementWPF
{
    public partial class SubscriptionDetailsWindow : Window
    {
        public Customer Subscription { get; private set; }

        public SubscriptionDetailsWindow()
        {
            InitializeComponent();
            // Set default dates
            RegisterDatePicker.SelectedDate = DateTime.Now;
            LastActivityDatePicker.SelectedDate = DateTime.Now;
            PackageComboBox.SelectedIndex = 0;
            UpdateExpireDate();

            // Populate AccountComboBox from AccountManager; add a "None" option at the top.
            AccountComboBox.Items.Clear();
            AccountComboBox.Items.Add("None");
            foreach (var acc in AccountManager.Accounts)
            {
                AccountComboBox.Items.Add(acc.Email);
            }
            AccountComboBox.SelectedIndex = 0;
        }

        // Overloaded constructor for editing an existing subscription
        public SubscriptionDetailsWindow(Customer existingSubscription) : this()
        {
            Subscription = existingSubscription;
            NameTextBox.Text = Subscription.Name;
            // Set package selection based on the subscription's package
            for (int i = 0; i < PackageComboBox.Items.Count; i++)
            {
                ComboBoxItem item = PackageComboBox.Items[i] as ComboBoxItem;
                if (item != null && item.Content.ToString() == Subscription.SubscriptionPackage)
                {
                    PackageComboBox.SelectedIndex = i;
                    break;
                }
            }
            // Alternatively, if the ComboBox contains strings, do:
            foreach (var obj in PackageComboBox.Items)
            {
                if (obj is string str && str == Subscription.SubscriptionPackage)
                {
                    PackageComboBox.SelectedItem = str;
                    break;
                }
            }
            RegisterDatePicker.SelectedDate = Subscription.RegisterDay;
            UpdateExpireDate();
            LastActivityDatePicker.SelectedDate = Subscription.LastActivity;
            // Set account selection based on Subscription.Note
            if (!string.IsNullOrEmpty(Subscription.Note))
            {
                int index = AccountComboBox.Items.IndexOf(Subscription.Note);
                AccountComboBox.SelectedIndex = index >= 0 ? index : 0;
            }
        }

        private void PackageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateExpireDate();
        }

        private void RegisterDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateExpireDate();
        }

        private void UpdateExpireDate()
        {
            if (RegisterDatePicker.SelectedDate.HasValue && PackageComboBox.SelectedItem != null)
            {
                DateTime registerDate = RegisterDatePicker.SelectedDate.Value;
                int monthsToAdd = 0;
                string package = "";
                // Since PackageComboBox may contain ComboBoxItem or string, try both:
                if (PackageComboBox.SelectedItem is ComboBoxItem cbi)
                {
                    package = cbi.Content.ToString();
                }
                else
                {
                    package = PackageComboBox.SelectedItem.ToString();
                }
                switch (package)
                {
                    case "goi1":
                        monthsToAdd = 1;
                        break;
                    case "goi3":
                        monthsToAdd = 3;
                        break;
                    case "goi6":
                        monthsToAdd = 6;
                        break;
                    case "goi12":
                        monthsToAdd = 12;
                        break;
                }
                DateTime expireDate = registerDate.AddMonths(monthsToAdd);
                ExpireTextBox.Text = expireDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                ExpireTextBox.Text = "";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                PackageComboBox.SelectedItem == null ||
                !RegisterDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill in Name, Package, and Register Day.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Subscription == null)
            {
                Subscription = new Customer();
            }

            Subscription.Name = NameTextBox.Text.Trim();
            // Get selected package (works whether items are ComboBoxItem or string)
            string selectedPackage = "";
            if (PackageComboBox.SelectedItem is ComboBoxItem cbi)
                selectedPackage = cbi.Content.ToString();
            else
                selectedPackage = PackageComboBox.SelectedItem.ToString();
            Subscription.SubscriptionPackage = selectedPackage;
            Subscription.RegisterDay = RegisterDatePicker.SelectedDate.Value;
            // Compute expiry based on package duration
            int monthsToAdd = 0;
            switch (selectedPackage)
            {
                case "goi1":
                    monthsToAdd = 1;
                    break;
                case "goi3":
                    monthsToAdd = 3;
                    break;
                case "goi6":
                    monthsToAdd = 6;
                    break;
                case "goi12":
                    monthsToAdd = 12;
                    break;
            }
            Subscription.SubscriptionExpiry = Subscription.RegisterDay.AddMonths(monthsToAdd);
            Subscription.LastActivity = LastActivityDatePicker.SelectedDate.HasValue ? LastActivityDatePicker.SelectedDate.Value : Subscription.RegisterDay;
            // For account selection, if "None" is selected then leave Note empty.
            string accountSelection = AccountComboBox.SelectedItem.ToString();
            Subscription.Note = accountSelection != "None" ? accountSelection : "";

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
