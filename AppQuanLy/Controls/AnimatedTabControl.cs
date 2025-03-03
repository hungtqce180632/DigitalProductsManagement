using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SubscriptionManagementWPF.Controls
{
    public class AnimatedTabControl : TabControl
    {
        public AnimatedTabControl()
        {
            SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl && tabControl.SelectedContent != null)
            {
                if (tabControl.SelectedContent is FrameworkElement content)
                {
                    // Create animation for the new tab content
                    DoubleAnimation fadeIn = new DoubleAnimation
                    {
                        From = 0.3,
                        To = 1.0,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };

                    fadeIn.EasingFunction = new BackEase 
                    { 
                        EasingMode = EasingMode.EaseOut,
                        Amplitude = 0.3
                    };

                    // Apply the animation
                    content.BeginAnimation(OpacityProperty, fadeIn);

                    // Optional: Create transformation animation
                    if (content.RenderTransform == null)
                    {
                        content.RenderTransform = new System.Windows.Media.TranslateTransform();
                        content.RenderTransformOrigin = new Point(0.5, 0.5);
                    }

                    if (content.RenderTransform is System.Windows.Media.TranslateTransform transform)
                    {
                        DoubleAnimation slideIn = new DoubleAnimation
                        {
                            From = 20,
                            To = 0,
                            Duration = new Duration(TimeSpan.FromMilliseconds(300))
                        };

                        slideIn.EasingFunction = new BackEase
                        {
                            EasingMode = EasingMode.EaseOut,
                            Amplitude = 0.3
                        };

                        transform.BeginAnimation(System.Windows.Media.TranslateTransform.YProperty, slideIn);
                    }
                }
            }
        }
    }
}
