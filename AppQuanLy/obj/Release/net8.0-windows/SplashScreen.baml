   M S B A M L   `   `   `  ����   	AppQuanLy,    &SubscriptionManagementWPF.SplashScreen   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  SplashScreen��$	��300��$	��500�� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � 	Resources    LoadingAnimation g� RepeatBehavior �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration W� EasingFunction/  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  FadeInAnimation.  (System.Windows.Media.Animation.CubicEase��%  &	        &	 �     �� $ Forever��.��W� $ LoadingRing��$9 3(UIElement.RenderTransform).(RotateTransform.Angle)�	 0
 360$ 0:0:1.5D�   $ 	EaseInOut=��� .��W� $ 
MainBorder��$ Opacity�	 0
 1$ 0:0:0.7D�   $ EaseOut=�.��� .+��� -    ��Name$ 
MainBorder�����#1A1A1A �� CornerRadius$ 15�� J� Opacity$ 0Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 20Q�   ShadowDepth$ 5Q�   Opacity$	 0.5Q�   Color$ Black��._��  � RowDefinitions �� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��1��$��SUBSCRIPTION MANAGER��$��Center=�$��30(�$
��Bold"�$��	0,20,0,10q�����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #FFFFFF�� � Offset$ 0Q�� $ #FFD700��$ 1Q�   $ 15Q�$ 3Q�$	 0.8Q��� $��2��$��v1.0.0��$��Center=�$��14(����#CCCCCC$��0,0,0,30q��� $��3��$��Center=�$	��Top=�$��0,0,0,30q�.+�� .+�A� -    A�Name$ LoadingRing��$��40��$��40��$��4�� J� RenderTransformOrigin$ 0.5,0.5&�z���  �� Angle$ 0Q����� $	 0,0&�$	 1,1&�.��� $ #FFD700��$ 0Q�� $ #1A1A1A��$	 0.5Q�� $ #FFD700��$ 1Q��� -    ��Name$ 
StatusText��$��
Loading...��$��Center=����#CCCCCC$��0,15,0,0q�