using System;
using System.Reflection;
using Microsoft.Win32;

namespace SubscriptionManagementWPF
{
    public static class AutoStartManager
    {
        private const string registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string appName = "SubscriptionManagementWPF";

        public static bool ConfigureAutoStart(bool enable)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath, true))
                {
                    if (key == null)
                        return false;
                    if (enable)
                    {
                        string exePath = Assembly.GetExecutingAssembly().Location;
                        key.SetValue(appName, $"\"{exePath}\"");
                    }
                    else
                    {
                        if (key.GetValue(appName) != null)
                            key.DeleteValue(appName);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsAutoStartEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath, false))
                {
                    if (key == null)
                        return false;
                    object value = key.GetValue(appName);
                    return value != null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
