   M S B A M L   `   `   `  ����  	AppQuanLy,    &SubscriptionManagementWPF.SplashScreen   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  SplashScreen��5   	   $	��300��5   	   $	��500��6    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	   
������  � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5         LoadingAnimation g� RepeatBehavior �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration W� EasingFunction/  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  FadeInAnimation.  (System.Windows.Media.Animation.CubicEase��%�  &	        &	     �� 5   
   $ Forever��6.   .��5      W� $ LoadingRing��5      $9 3(UIElement.RenderTransform).(RotateTransform.Angle)�5      	 05      
 3606   $ 0:0:1.5D�6#    5        5      $ 	EaseInOut=�6!   5      5      �� 5   
   .��5      W� $ 
MainBorder��5      $ Opacity�5      	 05      
 16   $ 0:0:0.7D�6!    5        5      $ EaseOut=�6    5       5!      5"      .��5$      � .+�5%   
   �� -   5         ��Name$ 
MainBorder��5%      ���#1A1A1A �� CornerRadius$ 15��5'       J� Opacity$ 0Q�5(       J� Effect 5)      3  -System.Windows.Media.Effects.DropShadowEffect  5*         
BlurRadius$ 20Q�6#      ShadowDepth$ 5Q�63      Opacity$	 0.5Q�6C      Color$ Black��6Q   ._�5-      �  � RowDefinitions 5.      �� 5/      $��* �6$   �� 50      $
��Auto �6$   �� 51      $
��Auto �6$   �� 52      $��* �6$   .+�56      �� $��1��6   $��SUBSCRIPTION MANAGER��6)   $��Center=�6E   $��30(�57      $
��Bold"�6*   $��	0,20,0,10q�6<   ��58      �� 59       �� 
StartPoint$	 0,0&�6.    �� EndPoint$	 1,1&�6?   .��5:      �  � Color$ #FFFFFF��6+    � Offset$ 0Q�6;   � 5;      $ #FFD700��6+   $ 1Q�6;   5<       5>        5?      $ 15Q�6+   $ 3Q�6;   $	 0.8Q�6K   5A      �� 5D      $��2��6   $��v1.0.0��6)   $��Center=�67   $��14(�5E      ���#CCCCCC$��0,0,0,30q�6?   �� 5H      $��3��6   $��Center=�6*   $	��Top=�6G   $��0,0,0,30q�6_   .+�5I      � .+�5J      A� -   5         A�Name$ LoadingRing��5J   "   $��40��67   $��40��6B   $��4��6N    J� RenderTransformOrigin$ 0.5,0.5&�6b   z�5K      �� 5L   "    �� Angle$ 0Q�62   ��5N      �� 5O   "   $	 0,0&�66   $	 1,1&�6G   .��5P   &   � $ #FFD700��63   $ 0Q�6C   � 5Q   &   $ #1A1A1A��63   $	 0.5Q�6C   � 5R   &   $ #FFD700��63   $ 1Q�6C   5S   #   5U      5V      �� 5W      -   5         ��Name$ 
StatusText��5W       $��
Loading...��64   $��Center=�6F   ���#CCCCCC$��0,15,0,0q�5X   5   5Y      5Z      5[      5\      5]      