using System.Collections.ObjectModel;

namespace SubscriptionManagementWPF
{
    public static class AccountManager
    {
        public static ObservableCollection<Account> Accounts { get; } = new ObservableCollection<Account>();
    }
}
