   M S B A M L   `   `   `  ����L  HBufet1131Vorobyov, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bufet1131VorobyovBufet1131Vorobyov  5   	        Bufet1131Vorobyov.AddOrder   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Bufet1131Vorobyov     5   	     � Title$  Учёт заказов��5   	   $	��450��6   $	��850��6+    � WindowStartupLocation$ CenterScreen=�67   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      $
��0.4* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   o� $��2��6   .@�5      m� $��Редактор��6   .@�5      m� -   5        $!��Добавить заказ��5      63   m� 5      -   5        $��Удалить заказ��5      62   5      5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns .  ��6    �� *Orders+ �� SelectedItem 6?   �� *SelectedOrder+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата��6%      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 5   %   �� *
DateTime+
 �� StringFormat$
 d/M/yyyy��  5      $ Меню��6%    . 	 5   %   �� *	Menu.Name+  5      $ 
Блюдо��6%    . 	 5    %   �� *	Food.Name+  5!      $ Поставщик��6%    . 	 5"   %   �� *Provider.Name+  5#      $ Количество��6%    . 	 5$   %   �� *Count+  5%      $) #Итоговая стоимость��6%    . 	 5&   %   �� *Cost+5(      �� 5)   
   $��1��6   $��1��6"   .+�5*      �� $��Дата��6   (  "System.Windows.Controls.DatePicker  5+         SelectedDate 6   �� *DateTimeOrder+�� 5,      $��Меню��6   �� 5-       �� DisplayMemberPath$
 Name��6[   ��6   �� *Menus+ 65   �� *SelectedMenu+6[   �� 5.      $��Поставщик��6   �� 5/      $
 Name��6c   ��6   �� *	Providers+ 69   �� *SelectedProvider+6c   �� 50      $��Количество��6   �� 51      ��6   �� *
CountOrder+52      53      54      