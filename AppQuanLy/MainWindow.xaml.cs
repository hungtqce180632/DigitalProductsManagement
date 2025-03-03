using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Net.Http;
using System.Threading.Tasks;

namespace SubscriptionManagementWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Customer> customers;
        private ObservableCollection<Message> messages;
        private int nextCustomerId = 4; // Starting from 4 for new subscriptions
        private string MessengerTemplate = "";


        public MainWindow()
        {
            InitializeComponent();

            // Initialize sample customers (dates in dd/MM/yyyy)
            customers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Alice",
                    SubscriptionPackage = "goi1",
                    RegisterDay = new DateTime(2025, 09, 01),
                    SubscriptionExpiry = new DateTime(2025, 10, 01),
                    LastActivity = new DateTime(2025, 09, 01),
                    Note = "aaa@gmail.com"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Bob",
                    SubscriptionPackage = "goi3",
                    RegisterDay = new DateTime(2025, 09, 01),
                    SubscriptionExpiry = new DateTime(2025, 12, 01),
                    LastActivity = new DateTime(2025, 09, 01),
                    Note = "bbb@gmail.com"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Charlie",
                    SubscriptionPackage = "goi6",
                    RegisterDay = new DateTime(2025, 09, 01),
                    SubscriptionExpiry = new DateTime(2026, 03, 01),
                    LastActivity = new DateTime(2025, 09, 01),
                    Note = "aaa@gmail.com"
                }
            };
            CustomerDataGrid.ItemsSource = customers;

            // Messenger integration
            messages = Messenger.Messages;
            MessageDataGrid.ItemsSource = messages;

            // Set the Auto-Start checkbox state
            AutoStartCheckBox.IsChecked = AutoStartManager.IsAutoStartEnabled();

            // Initialize sample accounts
            if (AccountManager.Accounts.Count == 0)
            {
                // Add some sample accounts
                AccountManager.Accounts.Add(new Account { Email = "aaa@gmail.com", CustomerCount = 0 });
                AccountManager.Accounts.Add(new Account { Email = "bbb@gmail.com", CustomerCount = 0 });
            }
            AccountDataGrid.ItemsSource = AccountManager.Accounts;

            // Update account counts based on current customers
            UpdateAccountCounts();
            // Update money statistics
            UpdateMoneyStatistics();
        }

        #region Subscription Management

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.Trim();
            IEnumerable<Customer> results;
            if (DateTime.TryParse(searchTerm, out DateTime searchDate))
            {
                results = customers.Where(c => c.RegisterDay.Date == searchDate.Date);
            }
            else
            {
                results = customers.Where(c =>
                    c.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.SubscriptionPackage.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            CustomerDataGrid.ItemsSource = new ObservableCollection<Customer>(results);
        }

        // Inside your MainWindow class:
        private void ExportAccounts_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Export Accounts as CSV"
            };
            if (dlg.ShowDialog() == true)
            {
                bool success = AccountDataManager.ExportAccounts(new List<Account>(AccountManager.Accounts), dlg.FileName);
                MessageBox.Show(success ? "Accounts exported successfully." : "Export failed.", "Accounts Export", MessageBoxButton.OK);
            }
        }

        private void ImportAccounts_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Import Accounts from CSV"
            };
            if (dlg.ShowDialog() == true)
            {
                var importedAccounts = AccountDataManager.ImportAccounts(dlg.FileName);
                if (importedAccounts != null)
                {
                    AccountManager.Accounts.Clear();
                    foreach (var acc in importedAccounts)
                    {
                        AccountManager.Accounts.Add(acc);
                    }
                    AccountDataGrid.Items.Refresh();
                    MessageBox.Show("Accounts imported successfully.", "Accounts Import", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Import failed or file not found.", "Accounts Import", MessageBoxButton.OK);
                }
            }
        }

        // Event handler for the Save Configuration button.
        private void SaveMessengerConfig_Click(object sender, RoutedEventArgs e)
        {
            MessengerConfig.PageAccessToken = PageAccessTokenTextBox.Text.Trim();
            MessengerConfig.DefaultRecipientId = DefaultRecipientIdTextBox.Text.Trim();
            MessengerStatusTextBlock.Text = "Messenger configuration saved.";
        }

        // Event handler for the Refresh Messages button.
        private async void RefreshMessengerMessages_Click(object sender, RoutedEventArgs e)
        {
            // Call the FacebookMessengerService to load messages.
            string result = await FacebookMessengerService.LoadMessagesAsync();
            MessengerStatusTextBlock.Text = result;
        }

        // Event handler to save a message template.
        private void SaveTemplate_Click(object sender, RoutedEventArgs e)
        {
            MessengerTemplate = ComposeMessageTextBox.Text.Trim();
            if (string.IsNullOrEmpty(MessengerTemplate))
            {
                MessageBox.Show("Please enter a message template.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Template saved.", "Messenger Chat", MessageBoxButton.OK, MessageBoxImage.Information);
            // Also, populate the RecipientListBox with accounts that have a Messenger ID.
            // For demonstration, we’ll use the Account's Email (or you can add a MessengerId property to Account).
            RecipientListBox.Items.Clear();
            foreach (var acc in AccountManager.Accounts)
            {
                // Here you might want to show accounts that are “active” for messaging.
                RecipientListBox.Items.Add(acc.Email);
            }
        }

        // Event handler to send the saved template to selected recipients.
        private async void SendTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MessengerTemplate))
            {
                MessageBox.Show("No template is saved. Please compose and save a template first.", "Messenger Chat", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (RecipientListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one recipient.", "Messenger Chat", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int sentCount = 0;
            foreach (var item in RecipientListBox.SelectedItems)
            {
                // In a real scenario, you’d convert the account’s email (or stored MessengerId) to the recipient’s PSID.
                // For this demo, we assume the item (an email) is the recipient ID.
                string recipientId = item.ToString();
                bool sent = await FacebookMessengerService.SendMessageAsync(recipientId, MessengerTemplate);
                if (sent) sentCount++;
            }
            MessageBox.Show($"Sent template to {sentCount} recipient(s).", "Messenger Chat", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RefreshCustomerList_Click(object sender, RoutedEventArgs e)
        {
            CustomerDataGrid.ItemsSource = customers;
        }

        private void UpdateAccountCounts()
        {
            // For each account, count how many customers have Note equal to the account email.
            foreach (var account in AccountManager.Accounts)
            {
                account.CustomerCount = customers.Count(c => c.Note == account.Email);
            }
            AccountDataGrid.Items.Refresh();
        }

        private void CheckRenewalReminders_Click(object sender, RoutedEventArgs e)
        {
            string reminders = "";
            DateTime now = DateTime.Now;
            foreach (var customer in customers)
            {
                TimeSpan diff = customer.SubscriptionExpiry - now;
                if (diff.TotalDays <= 7 && diff.TotalDays >= 0)
                {
                    reminders += $"Reminder: {customer.Name}'s subscription ({customer.SubscriptionPackage}) expires on {customer.SubscriptionExpiry:dd/MM/yyyy} (in {diff.Days} days).\n";
                }
                else if (diff.TotalDays < 0)
                {
                    reminders += $"Alert: {customer.Name}'s subscription ({customer.SubscriptionPackage}) expired on {customer.SubscriptionExpiry:dd/MM/yyyy}.\n";
                }
            }
            if (string.IsNullOrWhiteSpace(reminders))
            {
                reminders = "No upcoming expirations.";
            }
            MessageBox.Show(reminders, "Renewal Reminders", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddSubscription_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionDetailsWindow addWindow = new SubscriptionDetailsWindow();
            if (addWindow.ShowDialog() == true)
            {
                Customer newCustomer = addWindow.Subscription;
                newCustomer.Id = nextCustomerId++;
                customers.Add(newCustomer);
                MessageBox.Show("Subscription added successfully.", "Add Subscription", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateAccountCounts();
                UpdateMoneyStatistics();
            }
        }

        private void EditSubscription_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem is Customer selectedCustomer)
            {
                // Create a copy for editing
                Customer customerCopy = new Customer
                {
                    Id = selectedCustomer.Id,
                    Name = selectedCustomer.Name,
                    SubscriptionPackage = selectedCustomer.SubscriptionPackage,
                    RegisterDay = selectedCustomer.RegisterDay,
                    SubscriptionExpiry = selectedCustomer.SubscriptionExpiry,
                    LastActivity = selectedCustomer.LastActivity,
                    Note = selectedCustomer.Note
                };

                SubscriptionDetailsWindow editWindow = new SubscriptionDetailsWindow(customerCopy);
                if (editWindow.ShowDialog() == true)
                {
                    selectedCustomer.Name = editWindow.Subscription.Name;
                    selectedCustomer.SubscriptionPackage = editWindow.Subscription.SubscriptionPackage;
                    selectedCustomer.RegisterDay = editWindow.Subscription.RegisterDay;
                    selectedCustomer.SubscriptionExpiry = editWindow.Subscription.SubscriptionExpiry;
                    selectedCustomer.LastActivity = editWindow.Subscription.LastActivity;
                    selectedCustomer.Note = editWindow.Subscription.Note;
                    CustomerDataGrid.Items.Refresh();
                    MessageBox.Show("Subscription updated successfully.", "Edit Subscription", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateAccountCounts();
                    UpdateMoneyStatistics();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteSubscription_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem is Customer selectedCustomer)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete subscription for {selectedCustomer.Name}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    customers.Remove(selectedCustomer);
                    MessageBox.Show("Subscription deleted successfully.", "Delete Subscription", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateAccountCounts();
                    UpdateMoneyStatistics();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // When Update Activity Log is clicked, open the renewal window.
        private void UpdateActivityLog_Click(object sender, RoutedEventArgs e)
        {
            // Filter customers whose SubscriptionExpiry is within 7 days or already expired.
            var renewables = customers.Where(c => (c.SubscriptionExpiry - DateTime.Now).TotalDays <= 7).ToList();
            if (renewables.Count == 0)
            {
                MessageBox.Show("No customers are near expiration.", "Renewal", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            RenewSubscriptionWindow renewWindow = new RenewSubscriptionWindow(renewables);
            if (renewWindow.ShowDialog() == true)
            {
                // The window updates the selected customer directly.
                CustomerDataGrid.Items.Refresh();
                MessageBox.Show("Customer subscription renewed.", "Renewal", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateMoneyStatistics();
            }
        }

        #endregion

        #region Messenger Integration

        private void SimulateIncomingMessage_Click(object sender, RoutedEventArgs e)
        {
            string content = IncomingMessageTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                Messenger.SimulateIncomingMessage(content);
                IncomingMessageTextBox.Clear();
                MessageDataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Enter a message content.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void SendAutoReply_Click(object sender, RoutedEventArgs e)
        {
            if (MessageDataGrid.SelectedItem is Message selectedMessage)
            {
                // In a real-world scenario, you would extract the recipient's PSID from your stored data.
                // Here we use a placeholder recipient ID.
                string recipientId = "RECIPIENT_PSID_PLACEHOLDER";
                string autoReplyText = "Thank you for reaching out. We will get back to you shortly.";

                bool sent = await FacebookMessengerService.SendMessageAsync(recipientId, autoReplyText);
                if (sent)
                {
                    MessageBox.Show("Auto-reply sent via Facebook Messenger.", "Messenger", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to send auto-reply via Facebook Messenger.", "Messenger", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageDataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a message to send an auto-reply.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MarkAllMessagesRead_Click(object sender, RoutedEventArgs e)
        {
            Messenger.MarkAllAsRead();
            MessageDataGrid.Items.Refresh();
        }

        #endregion

        #region Data Handling & Exporting

        private void BackupData_Click(object sender, RoutedEventArgs e)
        {
            bool success = DataManager.BackupData(customers.ToList());
            DataStatusTextBlock.Text = success ? "Backup successful." : "Backup failed.";
        }

        private void RestoreData_Click(object sender, RoutedEventArgs e)
        {
            var restoredData = DataManager.RestoreData();
            if (restoredData != null)
            {
                customers.Clear();
                foreach (var cust in restoredData)
                {
                    customers.Add(cust);
                }
                CustomerDataGrid.Items.Refresh();
                DataStatusTextBlock.Text = "Data restored successfully.";
                UpdateAccountCounts();
                UpdateMoneyStatistics();
            }
            else
            {
                DataStatusTextBlock.Text = "Restore failed or no backup file found.";
            }
        }

        private void ExportCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            dlg.Title = "Export Subscriptions as CSV";
            if (dlg.ShowDialog() == true)
            {
                bool success = DataManager.ExportCSV(customers.ToList(), dlg.FileName);
                DataStatusTextBlock.Text = success ? "Exported CSV successfully." : "CSV export failed.";
            }
        }

        private void ExportJSON_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "JSON Files (*.json)|*.json";
            dlg.Title = "Export Subscriptions as JSON";
            if (dlg.ShowDialog() == true)
            {
                bool success = DataManager.ExportJSON(customers.ToList(), dlg.FileName);
                DataStatusTextBlock.Text = success ? "Exported JSON successfully." : "JSON export failed.";
            }
        }

        private void ImportCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            dlg.Title = "Import Subscriptions from CSV";
            if (dlg.ShowDialog() == true)
            {
                var importedData = DataManager.ImportCSV(dlg.FileName);
                if (importedData != null)
                {
                    customers.Clear();
                    foreach (var cust in importedData)
                    {
                        customers.Add(cust);
                    }
                    CustomerDataGrid.Items.Refresh();
                    DataStatusTextBlock.Text = "Imported CSV data successfully.";
                    UpdateAccountCounts();
                    UpdateMoneyStatistics();
                }
                else
                {
                    DataStatusTextBlock.Text = "CSV import failed or file not found.";
                }
            }
        }

        #endregion

        #region Settings

        private void AutoStartCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool success = AutoStartManager.ConfigureAutoStart(true);
            if (!success)
            {
                MessageBox.Show("Failed to enable auto-start.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoStartCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            bool success = AutoStartManager.ConfigureAutoStart(false);
            if (!success)
            {
                MessageBox.Show("Failed to disable auto-start.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Account Inventory Management

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountDetailsWindow addAcc = new AccountDetailsWindow();
            if (addAcc.ShowDialog() == true)
            {
                // Check if account already exists
                if (AccountManager.Accounts.Any(a => a.Email.Equals(addAcc.Account.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("This account already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AccountManager.Accounts.Add(addAcc.Account);
                    AccountDataGrid.Items.Refresh();
                    UpdateMoneyStatistics();
                }
            }
        }

        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem is Account selectedAccount)
            {
                AccountDetailsWindow editAcc = new AccountDetailsWindow(selectedAccount);
                if (editAcc.ShowDialog() == true)
                {
                    selectedAccount.Email = editAcc.Account.Email;
                    AccountDataGrid.Items.Refresh();
                    // Also update the Note field in subscriptions if needed.
                    foreach (var cust in customers)
                    {
                        if (cust.Note == selectedAccount.Email)
                        {
                            cust.Note = editAcc.Account.Email;
                        }
                    }
                    CustomerDataGrid.Items.Refresh();
                    UpdateMoneyStatistics();
                }
            }
            else
            {
                MessageBox.Show("Please select an account to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem is Account selectedAccount)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete account '{selectedAccount.Email}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Before deletion, check if any subscription uses this account.
                    if (customers.Any(c => c.Note == selectedAccount.Email))
                    {
                        MessageBox.Show("Cannot delete account; it is assigned to one or more subscriptions.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    AccountManager.Accounts.Remove(selectedAccount);
                    AccountDataGrid.Items.Refresh();
                    UpdateMoneyStatistics();
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region Money Stastic

        // Update the Money Statistics on the Money Stastic tab.
        private void UpdateMoneyStatistics()
        {
            // Calculate total profit from subscriptions.
            int countGoi1 = customers.Count(c => c.SubscriptionPackage == "goi1");
            int countGoi3 = customers.Count(c => c.SubscriptionPackage == "goi3");
            int countGoi6 = customers.Count(c => c.SubscriptionPackage == "goi6");
            int countGoi12 = customers.Count(c => c.SubscriptionPackage == "goi12");

            long profitGoi1 = countGoi1 * 71000;
            long profitGoi3 = countGoi3 * 195000;
            long profitGoi6 = countGoi6 * 360000;
            long profitGoi12 = countGoi12 * 660000;

            long totalSubscriptionsProfit = profitGoi1 + profitGoi3 + profitGoi6 + profitGoi12;

            // Calculate account cost: every account costs 200,000 VND.
            int accountCount = AccountManager.Accounts.Count;
            long totalAccountCost = accountCount * 200000;

            long netProfit = totalSubscriptionsProfit - totalAccountCost;

            // Update the textblocks on the Money Stastic tab.
            SubscriptionsProfitTextBlock.Text = "Total Subscriptions Profit: " + totalSubscriptionsProfit.ToString("N0") + " VND";
            AccountCostTextBlock.Text = "Total Account Cost: " + totalAccountCost.ToString("N0") + " VND";
            NetProfitTextBlock.Text = "Net Profit: " + netProfit.ToString("N0") + " VND";
        }

        private void RefreshStatistics_Click(object sender, RoutedEventArgs e)
        {
            UpdateMoneyStatistics();
        }

        #endregion

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Play the 3D rotation animation when tabs change
            if (sender is TabControl && e.Source is TabControl)
            {
                Storyboard tabTransition = (Storyboard)FindResource("TabTransition");
                tabTransition.Begin();
            }
        }
    }
}
