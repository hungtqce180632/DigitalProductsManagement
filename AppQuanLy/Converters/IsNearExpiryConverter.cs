using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SubscriptionManagementWPF
{
    public class IsNearExpiryConverter : IValueConverter
    {
        // Convert from date to highlight color
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime expiryDate)
            {
                TimeSpan timeLeft = expiryDate - DateTime.Now;
                
                // Already expired - red
                if (timeLeft.TotalDays < 0)
                    return new SolidColorBrush(Colors.Red);
                
                // Within 3 days - orange
                if (timeLeft.TotalDays <= 3)
                    return new SolidColorBrush(Colors.OrangeRed);
                
                // Within a week - yellow
                if (timeLeft.TotalDays <= 7)
                    return new SolidColorBrush(Colors.Gold);
                
                // Normal - default
                return DependencyProperty.UnsetValue;
            }
            
            return DependencyProperty.UnsetValue;
        }

        // Not used for one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ExpiryWarningTextConverter : IValueConverter
    {
        // Convert from date to warning text
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime expiryDate)
            {
                TimeSpan timeLeft = expiryDate - DateTime.Now;
                
                if (timeLeft.TotalDays < 0)
                    return $"Expired ({Math.Abs((int)timeLeft.TotalDays)} days ago)";
                    
                if (timeLeft.TotalDays <= 3)
                    return $"Expires in {(int)timeLeft.TotalDays} days!";
                    
                if (timeLeft.TotalDays <= 7)
                    return $"Expires soon ({(int)timeLeft.TotalDays} days)";
                
                return string.Empty;
            }
            
            return string.Empty;
        }

        // Not used for one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
