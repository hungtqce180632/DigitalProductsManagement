using System;
using System.Globalization;
using System.Windows.Data;

namespace SubscriptionManagementWPF
{
    public class IsNearExpiryConverter : IValueConverter
    {
        // Returns true if (expiry date - now) is less than or equal to 7 days.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime expiry)
            {
                double daysLeft = (expiry - DateTime.Now).TotalDays;
                return daysLeft <= 7;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
