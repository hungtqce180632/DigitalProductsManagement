using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SubscriptionManagementWPF
{
    public partial class RenewSubscriptionWindow : Window
    {
        private List<Customer> eligibleCustomers;
        public RenewSubscriptionWindow(List<Customer> customers)
        {
            InitializeComponent();
            eligibleCustomers = customers;
            RenewDataGrid.ItemsSource = eligibleCustomers;
            NewPackageComboBox.SelectedIndex = 0;
        }

        private void RenewButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenewDataGrid.SelectedItem is Customer selectedCustomer)
            {
                string newPackage = "";
                if (NewPackageComboBox.SelectedItem is ComboBoxItem cbi)
                    newPackage = cbi.Content.ToString();
                else
                    newPackage = NewPackageComboBox.SelectedItem.ToString();
                // Update the customer's subscription info:
                selectedCustomer.RegisterDay = DateTime.Now;
                int monthsToAdd = 0;
                switch (newPackage)
                {
                    case "goi1": monthsToAdd = 1; break;
                    case "goi3": monthsToAdd = 3; break;
                    case "goi6": monthsToAdd = 6; break;
                    case "goi12": monthsToAdd = 12; break;
                }
                selectedCustomer.SubscriptionExpiry = selectedCustomer.RegisterDay.AddMonths(monthsToAdd);
                selectedCustomer.SubscriptionPackage = newPackage;
                selectedCustomer.LastActivity = DateTime.Now;
                RenewDataGrid.Items.Refresh();
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a customer to renew.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
