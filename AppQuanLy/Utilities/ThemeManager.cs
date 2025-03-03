using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SubscriptionManagementWPF.Utilities
{
    public static class ThemeManager
    {
        public static void ApplyEntryAnimation(FrameworkElement element)
        {
            if (element == null) return;

            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(300))
            };

            Storyboard.SetTarget(fadeIn, element);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(UIElement.OpacityProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeIn);
            storyboard.Begin();
        }

        public static void ApplyButtonHoverAnimation(FrameworkElement button, bool isEntering)
        {
            if (button == null) return;

            // Ensure the button has a ScaleTransform
            if (button.RenderTransform == null || !(button.RenderTransform is ScaleTransform))
            {
                button.RenderTransform = new ScaleTransform(1.0, 1.0);
                button.RenderTransformOrigin = new Point(0.5, 0.5);
            }

            double targetScale = isEntering ? 1.05 : 1.0;
            
            DoubleAnimation scaleXAnim = new DoubleAnimation
            {
                To = targetScale,
                Duration = new Duration(TimeSpan.FromMilliseconds(100))
            };
            
            DoubleAnimation scaleYAnim = new DoubleAnimation
            {
                To = targetScale,
                Duration = new Duration(TimeSpan.FromMilliseconds(100))
            };

            Storyboard.SetTarget(scaleXAnim, button);
            Storyboard.SetTargetProperty(scaleXAnim, 
                new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            
            Storyboard.SetTarget(scaleYAnim, button);
            Storyboard.SetTargetProperty(scaleYAnim, 
                new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(scaleXAnim);
            storyboard.Children.Add(scaleYAnim);
            storyboard.Begin();
        }
    }
}
