using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace SubscriptionManagementWPF
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public async Task StartAsync()
        {
            // Start animations
            Storyboard fadeInAnimation = (Storyboard)FindResource("FadeInAnimation");
            fadeInAnimation.Begin();
            
            Storyboard loadingAnimation = (Storyboard)FindResource("LoadingAnimation");
            loadingAnimation.Begin();
            
            // Load application data
            await Task.Run(() => LoadApplicationData());
            
            // Delay a bit to show splash screen
            await Task.Delay(2000);
        }
        
        private void LoadApplicationData()
        {
            // Simulate loading operations
            UpdateStatus("Initializing components...");
            Task.Delay(600).Wait();
            
            UpdateStatus("Loading configuration...");
            Task.Delay(500).Wait();
            
            UpdateStatus("Loading user data...");
            Task.Delay(700).Wait();
            
            UpdateStatus("Starting application...");
            Task.Delay(200).Wait();
        }
        
        private void UpdateStatus(string status)
        {
            Dispatcher.Invoke(() => StatusText.Text = status);
        }
    }
}
