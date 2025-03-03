using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SubscriptionManagementWPF
{
    public static class AccountDataManager
    {
        // Exports the account list as CSV to the specified file path.
        public static bool ExportAccounts(List<Account> accounts, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Header line
                    writer.WriteLine("Email,CustomerCount");
                    foreach (var account in accounts)
                    {
                        writer.WriteLine($"{account.Email},{account.CustomerCount}");
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Imports accounts from a CSV file at the specified file path.
        public static List<Account>? ImportAccounts(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;

                List<Account> importedAccounts = new List<Account>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine(); // skip header
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        string[] parts = line.Split(',');
                        if (parts.Length >= 2)
                        {
                            Account account = new Account
                            {
                                Email = parts[0],
                                CustomerCount = int.Parse(parts[1])
                            };
                            importedAccounts.Add(account);
                        }
                    }
                }
                return importedAccounts;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
