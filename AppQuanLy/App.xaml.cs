using System;
using System.Windows;

namespace SubscriptionManagementWPF
{
    public partial class App : Application
    {
        private SplashScreen splashScreen;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Show splash screen
            splashScreen = new SplashScreen();
            splashScreen.Show();
            
            // Start splash screen animation and loading process
            splashScreen.StartAsync().ContinueWith(t => 
            {
                Dispatcher.Invoke(() => 
                {
                    // Create and show main window
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    
                    // Close splash screen
                    splashScreen.Close();
                });
            });
            
            // Set startup URI to null since we're handling it manually
            StartupUri = null;
        }
    }
}
