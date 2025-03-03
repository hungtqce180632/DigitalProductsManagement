using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace SubscriptionManagementWPF
{
    public static class DataManager
    {
        // Exports the customer list as CSV to the specified file path.
        public static bool ExportCSV(List<Customer> customers, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Id,Name,SubscriptionPackage,RegisterDay,SubscriptionExpiry,LastActivity,Note");
                    foreach (var customer in customers)
                    {
                        writer.WriteLine($"{customer.Id},{customer.Name},{customer.SubscriptionPackage}," +
                                           $"{customer.RegisterDay:dd/MM/yyyy},{customer.SubscriptionExpiry:dd/MM/yyyy}," +
                                           $"{customer.LastActivity:dd/MM/yyyy},{customer.Note}");
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Exports the customer list as JSON to the specified file path.
        public static bool ExportJSON(List<Customer> customers, string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Imports customers from a CSV file at the specified file path.
        public static List<Customer>? ImportCSV(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;

                List<Customer> importedCustomers = new List<Customer>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine(); // Skip header
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        string[] parts = line.Split(',');
                        if (parts.Length >= 7)
                        {
                            Customer customer = new Customer
                            {
                                Id = int.Parse(parts[0]),
                                Name = parts[1],
                                SubscriptionPackage = parts[2],
                                RegisterDay = DateTime.ParseExact(parts[3], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                SubscriptionExpiry = DateTime.ParseExact(parts[4], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                LastActivity = DateTime.ParseExact(parts[5], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                Note = parts[6]
                            };
                            importedCustomers.Add(customer);
                        }
                    }
                }
                return importedCustomers;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Optionally, if you want backup/restore methods using JSON with a fixed file path, you can add:
        public static bool BackupData(List<Customer> customers)
        {
            try
            {
                string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
                // For example, save to a fixed file (adjust path as needed)
                File.WriteAllText("backup.json", json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Customer>? RestoreData()
        {
            try
            {
                if (File.Exists("backup.json"))
                {
                    string json = File.ReadAllText("backup.json");
                    var data = JsonSerializer.Deserialize<List<Customer>>(json);
                    return data;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
