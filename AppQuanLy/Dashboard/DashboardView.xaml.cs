using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SubscriptionManagementWPF.Dashboard
{
    public partial class DashboardView : UserControl
    {
        private readonly Color[] chartColors = new Color[]
        {
            Color.FromRgb(255, 215, 0),  // Gold
            Color.FromRgb(255, 165, 0),  // Orange
            Color.FromRgb(0, 191, 255),  // DeepSkyBlue
            Color.FromRgb(50, 205, 50),  // LimeGreen
        };
        
        public DashboardView()
        {
            InitializeComponent();
        }
        
        public void UpdateDashboard(IEnumerable<Customer> customers)
        {
            // Count totals
            int totalSubscriptions = customers.Count();
            int activeSubscriptions = customers.Count(c => c.SubscriptionExpiry > DateTime.Now);
            int expiringSoon = customers.Count(c => c.SubscriptionExpiry > DateTime.Now && 
                                               (c.SubscriptionExpiry - DateTime.Now).TotalDays <= 7);
            
            // Calculate monthly revenue
            long monthlyRevenue = CalculateMonthlyRevenue(customers);
            string formattedRevenue = monthlyRevenue >= 1000000 ? 
                $"{monthlyRevenue / 1000000:N1}M" :
                $"{monthlyRevenue / 1000:N0}K";
            
            // Update KPI cards
            TotalSubscriptionsText.Text = totalSubscriptions.ToString("N0");
            ActiveSubscriptionsText.Text = activeSubscriptions.ToString("N0");
            ExpiringSoonText.Text = expiringSoon.ToString("N0");
            MonthlyRevenueText.Text = formattedRevenue;
            
            // Create package distribution chart
            CreatePackageDistributionChart(customers);
            
            // Create expiry timeline
            CreateExpiryTimeline(customers);
        }
        
        private long CalculateMonthlyRevenue(IEnumerable<Customer> customers)
        {
            // Simplified calculation assuming all packages are paid monthly
            int countGoi1 = customers.Count(c => c.SubscriptionPackage == "goi1" && c.SubscriptionExpiry > DateTime.Now);
            int countGoi3 = customers.Count(c => c.SubscriptionPackage == "goi3" && c.SubscriptionExpiry > DateTime.Now);
            int countGoi6 = customers.Count(c => c.SubscriptionPackage == "goi6" && c.SubscriptionExpiry > DateTime.Now);
            int countGoi12 = customers.Count(c => c.SubscriptionPackage == "goi12" && c.SubscriptionExpiry > DateTime.Now);

            // Convert to monthly equivalents
            long monthlyRevenue = 
                (countGoi1 * 71000) + 
                (countGoi3 * 195000 / 3) +
                (countGoi6 * 360000 / 6) +
                (countGoi12 * 660000 / 12);
                
            return monthlyRevenue;
        }
        
        private void CreatePackageDistributionChart(IEnumerable<Customer> customers)
        {
            // Clear canvas
            PackageDistributionCanvas.Children.Clear();
            PackageLegend.Children.Clear();
            
            // Count by package
            int countGoi1 = customers.Count(c => c.SubscriptionPackage == "goi1");
            int countGoi3 = customers.Count(c => c.SubscriptionPackage == "goi3");
            int countGoi6 = customers.Count(c => c.SubscriptionPackage == "goi6");
            int countGoi12 = customers.Count(c => c.SubscriptionPackage == "goi12");
            
            int[] counts = { countGoi1, countGoi3, countGoi6, countGoi12 };
            string[] labels = { "goi1", "goi3", "goi6", "goi12" };
            
            // Skip drawing if no data
            if (counts.Sum() == 0) return;
            
            // Calculate pie chart
            double canvasSize = Math.Min(PackageDistributionCanvas.Width, PackageDistributionCanvas.Height);
            if (double.IsNaN(canvasSize) || canvasSize <= 0) canvasSize = 200; // Default if not yet rendered
            
            double radius = canvasSize / 2.5;
            double centerX = PackageDistributionCanvas.ActualWidth / 2;
            double centerY = PackageDistributionCanvas.ActualHeight / 2;
            
            double startAngle = 0;
            
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] == 0) continue; // Skip empty segments
                
                // Calculate angle
                double percentage = (double)counts[i] / counts.Sum();
                double angle = percentage * 360;
                
                // Create pie segment
                Path path = new Path();
                path.Fill = new SolidColorBrush(chartColors[i % chartColors.Length]);
                
                PathGeometry pathGeometry = new PathGeometry();
                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(centerX, centerY);
                
                // Create arc
                double endAngle = startAngle + angle;
                Point arcStart = new Point(
                    centerX + radius * Math.Cos(startAngle * Math.PI / 180),
                    centerY + radius * Math.Sin(startAngle * Math.PI / 180));
                
                Point arcEnd = new Point(
                    centerX + radius * Math.Cos(endAngle * Math.PI / 180),
                    centerY + radius * Math.Sin(endAngle * Math.PI / 180));
                
                pathFigure.Segments.Add(new LineSegment(arcStart, true));
                pathFigure.Segments.Add(new ArcSegment(
                    arcEnd,
                    new Size(radius, radius),
                    0,
                    angle > 180,
                    SweepDirection.Clockwise,
                    true));
                
                pathFigure.Segments.Add(new LineSegment(new Point(centerX, centerY), true));
                
                pathGeometry.Figures.Add(pathFigure);
                path.Data = pathGeometry;
                
                // Add tooltip with info
                ToolTip tooltip = new ToolTip();
                tooltip.Content = $"{labels[i]}: {counts[i]} ({percentage:P1})";
                path.ToolTip = tooltip;
                
                // Add animation on hover
                path.MouseEnter += (s, e) => {
                    path.Opacity = 0.8;
                    path.RenderTransform = new ScaleTransform(1.05, 1.05);
                    path.RenderTransformOrigin = new Point(0.5, 0.5);
                };
                path.MouseLeave += (s, e) => {
                    path.Opacity = 1.0;
                    path.RenderTransform = null;
                };
                
                PackageDistributionCanvas.Children.Add(path);
                
                // Update for next segment
                startAngle = endAngle;
                
                // Add legend item
                StackPanel legendItem = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5, 0, 5, 0) };
                
                Rectangle colorBox = new Rectangle {
                    Width = 15,
                    Height = 15,
                    Fill = new SolidColorBrush(chartColors[i % chartColors.Length]),
                    Margin = new Thickness(0, 0, 