   M S B A M L   `   `   `  ���� D  @AppQuanLy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SubscriptionManagementWPFSubscriptionManagementWPF  4    .SubscriptionManagementWPF.AccountDetailsWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:SubscriptionManagementWPF       � Title$  Account Details��$	��300��$	��450��   ModernWindow#��[   � WindowStartupLocation$ CenterOwner=� � Triggers :�  :� RoutedEvent$ Window.Loaded��.[��� .���� .��W�  �� TargetProperty$ Opacity� W� From 0.0	 W� To 1.0 g� Duration$ 0:0:0.3D�W�  �� 
TargetName$ 
MainBorder��$9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)� 0.95 1.0$ 0:0:0.3D�	 W� EasingFunction	 -  'System.Windows.Media.Animation.BackEase  7  1System.Windows.Media.Animation.EasingFunctionBase
   
EasingMode$
 EaseOut=�   	Amplitude$	 0.3Q�W� $ 
MainBorder��$9 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)� 0.95 1.0$ 0:0:0.3D�	   $
 EaseOut=�$	 0.3Q�.��� $��20q�.+��� -    ��Name$ 
MainBorder��  SecondaryBackgroundBrush#��[   PrimaryAccentBrush#��[ $��1q� �� CornerRadius$ 5�� �� Padding$ 20q� J� RenderTransformOrigin$ 0.5,0.5&�z���  �� ScaleX$ 1Q� �� ScaleY$ 1Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   ShadowDepth$ 3Q�   Opacity$	 0.5Q�._��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Account Information��$��20(�$
��Bold"�$��0,0,0,20q�#��[    $ 1Q�$ 3Q�$	 0.7Q��� $��1��.+��� $��Email Address:��  ModernLabel#��[ �� -    ��Name$ EmailTextBox��  ModernTextBox#��[ $��0,0,0,15q��� $q��kThis email address will be used to identify the account and will be associated with customer subscriptions.��  TextSecondaryBrush#��[ $
��Wrap=�$��0,0,0,10q��� $��2�� �� Orientation$ 
Horizontal=�$��Right=�.+��� -    ��Name$ 
SaveButton��$
��Save��  ModernButton#��[ $	��100��$��0,0,10,0q��� -   $ CancelButton��$��Cancel��#��[ $	��100��