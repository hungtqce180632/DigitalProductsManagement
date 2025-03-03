using System.Security.Principal;
using System.Windows;

namespace SubscriptionManagementWPF
{
    public partial class AccountDetailsWindow : Window
    {
        public Account Account { get; private set; }

        public AccountDetailsWindow()
        {
            InitializeComponent();
        }

        public AccountDetailsWindow(Account existingAccount) : this()
        {
            Account = existingAccount;
            EmailTextBox.Text = Account.Email;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Please enter an email.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Account == null)
                Account = new Account();
            Account.Email = EmailTextBox.Text.Trim();
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
