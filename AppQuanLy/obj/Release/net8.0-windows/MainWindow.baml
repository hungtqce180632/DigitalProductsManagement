   M S B A M L   `   `   `  ���� D  @AppQuanLy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SubscriptionManagementWPFSubscriptionManagementWPF  *    $SubscriptionManagementWPF.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:SubscriptionManagementWPF       � Title$  Subscription Management��$	��650��$	��950��   ModernWindow#��[   � 	Resources   TabTransition �� 
TargetName �� TargetProperty W� From	 W� To g� Duration g� AutoReverse��%   &	       �� .��W� $ MainTabControl��$7 1(UIElement.Effect).(DropShadowEffect.ShadowDepth)� 0 10$ 0:0:0.3D�$
 True��W� $ MainTabControl��$V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)� 0 1$ 0:0:0.3D�$
 True��.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��  HeaderGradient#��[ 	 �� Padding$	 15,10q�._��� 
 �� Orientation$
 
Horizontal=�.+��� $��SUBSCRIPTION��$��22(�$
��Bold"�$��0,0,0,0q�����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,0&�.���  � Color$ White�� � Offset$ 0Q��   PrimaryAccentColor# [ $ 1Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 2Q�   
BlurRadius$ 5Q�   Opacity$	 0.7Q��� $��MANAGER��$��22(�$
��Bold"�  PrimaryAccentBrush#��[ $��5,0,0,0q�   $ 2Q�$ 5Q�$	 0.7Q��� -    ��Name$ MainTabControl��$��1��  ModernTabControl#��[ z�V� .~���  �� Angle$ 0Q���  �� ScaleX$ 1Q� �� ScaleY$ 1Q�   $ 15Q�$ 0Q�$	 0.6Q�.@��� $��Subscriptions��  ModernTabItem#��[ .��� $��10q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$
 
Horizontal=�$��0,0,0,10q�.+��� -    ��Name$ SearchTextBox��$	��250��$��0,0,10,0q�  ModernTextBox#��[ .��
 � Tag$% Search by name, package or date��.���� -    ��Name$ SearchButton��$��Search��  ModernButton#��[ �� -   $ RefreshCustomerListButton��$��Refresh��#��[ $��10,0,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ CustomerDataGrid��$��1�� 	 ModernDataGrid#��[	  
 ModernDataGridColumnHeader   ColumnHeaderStyle# [
   ModernDataGridCell   	CellStyle# [    AutoGenerateColumns$ False��   SelectionMode$ Single=�   
IsReadOnly$
 True��    Columns  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$! ID��"   Width5  /System.Windows.Controls.DataGridLengthConverter$" 50 1  +System.Windows.Controls.DataGridBoundColumn#   Binding# �� *Id+  $
! Name��$	" 150 # �� *Name+  $! Package��$	" 100 # �� *SubscriptionPackage+  $! Register Date��$	" 120 # �� *RegisterDay+$ �� StringFormat$$ 
dd/MM/yyyy��  $! Expiry Date��$	" 120 # �� *SubscriptionExpiry+$$ 
dd/MM/yyyy��  $! Last Activity��$	" 120 # �� *LastActivity+$$ 
dd/MM/yyyy��  $! Account��$" * # �� *Note+�� $��2��$
 
Horizontal=�$��0,10,0,0q�.+��� -   $ AddSubscriptionButton��$��Add Subscription��#��[ $��0,0,10,0q��� -   $ EditSubscriptionButton��$��Edit Subscription��#��[ $��0,0,10,0q��� -   $ DeleteSubscriptionButton��$��Delete Subscription��#��[ $��0,0,10,0q��� -	   $! CheckRenewalRemindersButton��$��Check Renewals��#��[ $��0,0,10,0q��� -
   $ UpdateActivityLogButton��$��Renew Subscription��#��[ �� $��Accounts��#��[ .��� $��10q� �� $��* ��� $
��Auto �.+�  -   $ AccountDataGrid��$��0��#��[	 # [
 # [ $ False��$ Single=�$
 True��    $! Email��$" * # �� *Email+  $! 	Customers��$	" 100 # �� *CustomerCount+�� $��1��$
 
Horizontal=�$��0,10,0,0q�.+��� -   $ AddAccountButton��$��Add Account��#��[ $��0,0,10,0q��� -   $ EditAccountButton��$��Edit Account��#��[ $��0,0,10,0q��� -   $ DeleteAccountButton��$��Delete Account��#��[ $��0,0,10,0q��� -   $ ExportAccountsButton��$��Export Accounts��#��[ $��0,0,10,0q��� -   $ ImportAccountsButton��$��Import Accounts��#��[ �� $��	Messenger��#��[ .��� $��10q� �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��Messenger Configuration��  ModernGroupBox#��[ $��0��.��� $��5q�% � ColumnDefinitions% �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��Page Access Token:��  ModernLabel#��[ �� -   $ PageAccessTokenTextBox��$��0��$��1��#��[ �� $��1��$��0��$��Default Recipient ID:��#��[ �� -   $ DefaultRecipientIdTextBox��$��1��$��1��#��[ �� $��2��$��1��$
 
Horizontal=�$��0,10,0,0q�.+��� -   $ SaveMessengerConfigButton��$��Save Configuration��#��[ $��0,0,10,0q��� -   $$ RefreshMessengerMessagesButton��$��Refresh Messages��#��[ �� $��Incoming Messages��#��[ $��1��.��� $��5q� �� $
��Auto ��� $��* �.+��� $��0��$
 
Horizontal=�$��0,0,0,10q�.+��� -   $ IncomingMessageTextBox��$	��300��$��0,0,10,0q�#��[ .��$ Simulate incoming message��.���� -   $# SimulateIncomingMessageButton��$��Simulate��#��[   -   $ MessageDataGrid��$��1��#��[	 # [
 # [ $ False��$ Single=�$
 True��    $! ID��$" 50 # �� *Id+  $! Content��$	" 300 # �� *	Content+  $! Received At��$	" 150 # �� *
ReceivedAt+$$ dd/MM/yyyy HH:mm��4  .System.Windows.Controls.DataGridCheckBoxColumn  $
! Read��$" 50 # �� *IsRead+�� $��2��$
 
Horizontal=�$��0,10,0,0q�.+��� -   $ SendAutoReplyButton��$��Send Auto-Reply��#��[ $��0,0,10,0q��� -   $ MarkAllMessagesReadButton��$��Mark All as Read��#��[ �� $��Data Handling��#��[ .��� $��10q�.+��� $
 Vertical=�$��Center=�$��Center=�.+��� -   $��Backup Data��$	��200��$��5q�#��[ �� -   $��Restore Data��$	��200��$��5q�#��[ �� -   $��Export as CSV��$	��200��$��5q�#��[ �� -   $��Export as JSON��$	��200��$��5q�#��[ �� -   $��Import Data from CSV��$	��200��$��5q�#��[ �� -   $��DataStatusTextBlock��$��10q�$
��Wrap=�& �� TextAlignment$& Center=��� $��Messenger Setup��#��[ .��� $��10q�.+��� $
 Vertical=�$��Center=�$��Center=�.+��� $��Page Access Token:��#��[ �� -    $! PageAccessTokenSetupTextBox��$	��400��$��5q�#��[ �� $��Default Recipient ID:��#��[ �� -!   $$ DefaultRecipientIdSetupTextBox��$	��400��$��5q�#��[ �� -"   $��Save Configuration��$	��150��$��5q�#��[ �� -#   $��Refresh Messages��$	��150��$��5q�#��[ �� -$   ' ��Name$' MessengerStatusTextBlock��$��5q�$
��Wrap=�$& Center=��� $��	Chat Form��#��[ .��� $��10q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $
 
Horizontal=�$��5q�.+��� $��Compose Message:��$��Center=�#��[ �� -%   $ ComposeMessageTextBox��$	��400��$��5q�#��[ �� -&   $��Save Template��$	��100��$��5q�#��[ �� -'   ( ��Name$( RecipientListBox��$��1��$��5q�) �� SelectionMode$) Extended=�  ModernListBox#��[ �� $
 
Horizontal=�$��2��$��Center=�$��5q�.+��� -(   $��Send Template to Selected��$	��150��$��5q�#��[ �� $��Settings��#��[ .��� $��10q�.+��� $
 Vertical=�$��Center=�$��Center=�.+��� -)   $��AutoStartCheckBox��$$��Enable Auto-Start with Windows��$��5q�  ModernCheckBox#��[ �� $'��!(Requires restart to take effect)��$��5q��� $��Account Inventory��#��[ .��� $��10q�.+��� .+�  -*   $ AccountInventoryDataGrid��$ False��*   CanUserAddRows$* False��$
 True��#��[	 # [
 # [     $! Email��$	" 250 # �� *Email+  $! Customer Count��$	" 100 # �� *CustomerCount+�� $
 
Horizontal=�$��Center=�$��10q�.+��� -+   $��Add Account��$��5q�#��[ �� -,   $��Edit Account��$��5q�#��[ �� --   $��Delete Account��$��5q�#��[ �� -.   $��Export Accounts��$��5q�#��[ �� -/   $��Import Accounts��$��5q�#��[ �� $��Money Statistics��#��[ .��� $��10q�.+��� $��Center=�$��Center=�.+��� -0   $"' SubscriptionsProfitTextBlock��$��16(�$��5q��� -1   $' AccountCostTextBlock��$��16(�$��5q��� -2   $' NetProfitTextBlock��$��16(�$��5q��� -3   $��Refresh Statistics��$	��150��$��10q�$��Center=�#��[ 