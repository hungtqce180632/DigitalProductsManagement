   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    	AppQuanLy5   /SubscriptionManagementWPF.AddSubscriptionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  Add Subscription��5   	   $	��300��6"   $	��400��6/   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Name:��6-   $��Center=�6=   �� 5   
   -   5        $��NameTextBox��5   /   $��0��6   $��1��6   $��5q�6B   �� 5   
   $��1��6   $��0��6   $��Package:��6-   $��Center=�6@   �� 5   
   -   5        $��PackageTextBox��5   /   $��1��6   $��1��6   $��5q�6E   �� 5   
   $��2��6   $��0��6   $��Expiry Date:��6-   $��Center=�6D   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��ExpiryDatePicker��5   2   $��2��6   $��1��6"   $��5q�6J   
 �� Now   SelectedDate# Z 6U   �� 5   
   $��3��6   $��0��6   $��Last Activity:��6-   $��Center=�6F     5   
   -   5        $��LastActivityDatePicker��5   2   $��3��6   $��1��6"   $��5q�6P   # Z 6[   �� 5   
   $��4��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��5q�6|   .+�5       �� -   5        $	��Add��5       $��80��6;   $��5q�6F   �� 5!      -   5        $��Cancel��5!      $��80��6A   $��5q�6L   5"      5#      5$      