using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using SubscriptionManagementWPF.Controls;

namespace SubscriptionManagementWPF.Views
{
    public partial class DashboardView : UserControl
    {
        private List<double> revenueData = new List<double>() { 32, 42, 51, 49, 63, 58, 70 };
        
        public DashboardView()
        {
            InitializeComponent();
            Loaded += DashboardView_Loaded;
        }

        private void DashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStatistics();
            DrawRevenueChart();
            AnimateCards();
            InitializeFeaturedProduct();
        }

        private void LoadStatistics()
        {
            // In a production implementation, these would come from your data source
            // For this example, we'll use the data from your MainWindow.cs
            
            // Get data from main window or from a central data service
            var customers = GetCustomers();
            
            if (customers != null && customers.Any())
            {
                // Active subscriptions calculation
                int activeSubscriptions = customers.Count(c => c.SubscriptionExpiry > DateTime.Now);
                ActiveSubscriptionsValue.Text = activeSubscriptions.ToString();
                
                // Revenue calculation
                long revenue = CalculateRevenue(customers);
                RevenueValue.Text = string.Format("{0:N0}", revenue);
                
                // Accounts calculation - Get the number of unique accounts from Note property
                int uniqueAccounts = customers.Select(c => c.Note).Distinct().Count();
                AccountsValue.Text = uniqueAccounts.ToString();
                
                // Upcoming renewals calculation
                int upcomingRenewals = customers.Count(c => 
                    (c.SubscriptionExpiry - DateTime.Now).TotalDays <= 7 && 
                    (c.SubscriptionExpiry - DateTime.Now).TotalDays >= 0);
                RenewalsValue.Text = upcomingRenewals.ToString();
            }
        }

        private List<Customer> GetCustomers()
        {
            // In a real application, this would come from your database or service
            // For this example, we'll create a simplified approach
            try
            {
                // Try to access the main window's customers collection
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    // Get customers from the ItemsSource of the DataGrid
                    if (mainWindow.CustomerDataGrid?.ItemsSource is IEnumerable<Customer> customers)
                    {
                        return customers.ToList();
                    }
                }
                
                // If we can't access it directly, use dummy data for preview
                return new List<Customer>
                {
                    new Customer
                    {
                        Id = 1,
                        Name = "Alice",
                        SubscriptionPackage = "goi1",
                        RegisterDay = DateTime.Now.AddDays(-30),
                        SubscriptionExpiry = DateTime.Now.AddDays(1),
                        LastActivity = DateTime.Now,
                        Note = "aaa@gmail.com"
                    },
                    new Customer
                    {
                        Id = 2,
                        Name = "Bob",
                        SubscriptionPackage = "goi3",
                        RegisterDay = DateTime.Now.AddDays(-60),
                        SubscriptionExpiry = DateTime.Now.AddDays(30),
                        LastActivity = DateTime.Now,
                        Note = "bbb@gmail.com"
                    },
                    new Customer
                    {
                        Id = 3,
                        Name = "Charlie",
                        SubscriptionPackage = "goi6",
                        RegisterDay = DateTime.Now.AddDays(-90),
                        SubscriptionExpiry = DateTime.Now.AddDays(90),
                        LastActivity = DateTime.Now,
                        Note = "ccc@gmail.com"
                    }
                };
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        private long CalculateRevenue(List<Customer> customers)
        {
            long total = 0;
            foreach (var customer in customers)
            {
                switch (customer.SubscriptionPackage)
                {
                    case "goi1":
                        total += 71000;
                        break;
                    case "goi3":
                        total += 195000;
                        break;
                    case "goi6":
                        total += 360000;
                        break;
                    case "goi12":
                        total += 660000;
                        break;
                }
            }
            return total;
        }

        private void DrawRevenueChart()
        {
            if (revenueData.Count == 0)
                return;

            // Get canvas dimensions
            double chartWidth = RevenueChart.ActualWidth > 0 ? RevenueChart.ActualWidth : 500;
            double chartHeight = RevenueChart.ActualHeight > 0 ? RevenueChart.ActualHeight : 200;

            // Calculate spacing and scaling
            double xSpacing = chartWidth / (revenueData.Count - 1);
            double maxValue = revenueData.Max();
            double yScale = chartHeight / maxValue;

            // Create path geometry for line chart
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            
            // Start point at the first data point
            pathFigure.StartPoint = new Point(0, chartHeight - revenueData[0] * yScale);
            
            // Create segment collection for line segments
            var segments = new PolyLineSegment();
            
            // Create path geometry for area (with fill)
            var areaGeometry = new PathGeometry();
            var areaFigure = new PathFigure();
            areaFigure.StartPoint = new Point(0, chartHeight); // Start at bottom left
            
            // Add point at the first data value
            areaFigure.Segments.Add(new LineSegment(new Point(0, chartHeight - revenueData[0] * yScale), true));
            
            // Create area segment collection
            var areaSegments = new PolyLineSegment();

            // Add points for each data value
            for (int i = 0; i < revenueData.Count; i++)
            {
                double x = i * xSpacing;
                double y = chartHeight - revenueData[i] * yScale;
                
                segments.Points.Add(new Point(x, y));
                areaSegments.Points.Add(new Point(x, y));
            }
            
            // Add segments to the figures
            pathFigure.Segments.Add(segments);
            pathGeometry.Figures.Add(pathFigure);
            
            // Complete the area with bottom points to close the shape
            areaFigure.Segments.Add(areaSegments);
            areaFigure.Segments.Add(new LineSegment(new Point((revenueData.Count - 1) * xSpacing, chartHeight), true));
            areaFigure.Segments.Add(new LineSegment(new Point(0, chartHeight), true));
            
            // Set IsClosed to true to create a closed shape for the area
            areaFigure.IsClosed = true;
            areaGeometry.Figures.Add(areaFigure);
            
            // Set the geometries to the paths
            ChartPath.Data = pathGeometry;
            ChartAreaPath.Data = areaGeometry;
            
            // Animate the chart
            AnimateChart();
        }

        private void AnimateChart()
        {
            // Create animation for stroke dash array to create drawing effect
            var dashArray = new DoubleCollection { ChartPath.Data.Bounds.Width + ChartPath.Data.Bounds.Height, ChartPath.Data.Bounds.Width + ChartPath.Data.Bounds.Height };
            ChartPath.StrokeDashArray = dashArray;
            
            var animation = new DoubleAnimation
            {
                From = ChartPath.Data.Bounds.Width + ChartPath.Data.Bounds.Height,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            
            ChartPath.BeginAnimation(Path.StrokeDashOffsetProperty, animation);
            
            // Animate the area fill with opacity
            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 0.5,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            
            ChartAreaPath.BeginAnimation(Path.OpacityProperty, opacityAnimation);
        }

        private void AnimateCards()
        {
            // Add a slight delay between card animations
            AnimateCardWithDelay(ActiveSubscriptionsCard, 0);
            AnimateCardWithDelay(RevenueCard, 0.1);
            AnimateCardWithDelay(AccountsCard, 0.2);
            AnimateCardWithDelay(RenewalsCard, 0.3);
        }

        private void AnimateCardWithDelay(UIElement element, double delayInSeconds)
        {
            element.Opacity = 0;
            element.RenderTransform = new TranslateTransform(0, 20);
            
            // Create animations
            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(delayInSeconds)
            };
            
            var translateAnimation = new DoubleAnimation
            {
                From = 20,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(delayInSeconds)
            };
            
            // Apply animations
            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            ((TranslateTransform)element.RenderTransform).BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        private void InitializeFeaturedProduct()
        {
            // In a real application, you'd load a real 3D model
            // For this example, we'll use the default cube in the viewer
            
            // Add a rotation animation to the product viewer
            FeaturedProductViewer.Width = double.NaN; // Auto width
            FeaturedProductViewer.Height = 200;
        }
    }
}