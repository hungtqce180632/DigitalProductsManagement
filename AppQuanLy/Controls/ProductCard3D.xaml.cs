using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SubscriptionManagementWPF.Controls
{
    public partial class ProductCard3D : UserControl
    {
        // Dependency properties
        public static readonly DependencyProperty ProductNameProperty = DependencyProperty.Register(
            "ProductName", typeof(string), typeof(ProductCard3D), new PropertyMetadata("Digital Product"));
            
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register(
            "Price", typeof(string), typeof(ProductCard3D), new PropertyMetadata("VND 0"));
            
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(ProductCard3D), new PropertyMetadata("No description available."));
            
        public static readonly DependencyProperty IsFeaturedProperty = DependencyProperty.Register(
            "IsFeatured", typeof(bool), typeof(ProductCard3D), new PropertyMetadata(false, OnIsFeaturedChanged));

        // Events
        public event EventHandler<EventArgs> OnViewDetails;
        
        // Properties
        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }
        
        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        
        public bool IsFeatured
        {
            get { return (bool)GetValue(IsFeaturedProperty); }
            set { SetValue(IsFeaturedProperty, value); }
        }
        
        // Constructor
        public ProductCard3D()
        {
            InitializeComponent();
            Loaded += ProductCard3D_Loaded;
        }
        
        private void ProductCard3D_Loaded(object sender, RoutedEventArgs e)
        {
            // Update UI with dependency property values
            TitleTextBlock.Text = ProductName;
            PriceTextBlock.Text = Price;
            DescriptionTextBlock.Text = Description;
            
            // Set featured badge visibility
            FeaturedBadge.Visibility = IsFeatured ? Visibility.Visible : Visibility.Collapsed;
        }
        
        private static void OnIsFeaturedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProductCard3D card)
            {
                card.FeaturedBadge.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            // Apply hover effect
            AnimateHover(true);
        }
        
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            // Remove hover effect
            AnimateHover(false);
        }
        
        private void AnimateHover(bool isHovering)
        {
            // Scale animation
            var scaleAnimation = new DoubleAnimation
            {
                To = isHovering ? 1.05 : 1.0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            
            // Shadow animation
            var shadowAnimation = new DoubleAnimation
            {
                To = isHovering ? 15 : 8,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            
            // Get the DropShadowEffect from the Border
            var borderShadow = MainBorder.Effect as DropShadowEffect;
            
            // Apply animations
            if (borderShadow != null)
            {
                borderShadow.BeginAnimation(DropShadowEffect.BlurRadiusProperty, shadowAnimation);
            }
            
            // Apply scale animation to the parent Grid
            var scaleTransform = new ScaleTransform(1, 1);
            MainBorder.RenderTransform = scaleTransform;
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            
            // Change the border color when hovering
            var borderColorAnimation = new ColorAnimation
            {
                To = isHovering ? Color.FromRgb(255, 193, 7) : Color.FromRgb(51, 51, 51),
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            
            MainBorder.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, borderColorAnimation);
        }
        
        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            OnViewDetails?.Invoke(this, EventArgs.Empty);
        }
    }
}
