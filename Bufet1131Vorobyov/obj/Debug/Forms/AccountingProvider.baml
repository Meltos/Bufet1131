   M S B A M L   `   `   `  ����L  HBufet1131Vorobyov, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bufet1131VorobyovBufet1131Vorobyov  5   	   *    $Bufet1131Vorobyov.AccountingProvider   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Bufet1131Vorobyov     5   	     � Title$(  "Учёт поставок блюд��5   	   $	��450��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�6=   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      $
��0.7* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   o� $��2��6   .@�5      m� $��Редактор��6   .@�5      m� -   5        $'��!Добавить поставку��5      66   m� 5      -   5        $%��Удалить поставку��5      65   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dataGrid1��5      $��1��6&      AutoGenerateColumns .  ��63   �� *Accountings+ �� SelectedItem 6W   �� *SelectedAccounting+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата��6%   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 5   %   �� *
DateTime+ �� StringFormat$ d/M/yyyy��  5      $ 
Блюдо��6%   	 . 
 5   %   �� *	Food.Name+  5      $ Поставщик��6%   	 . 
 5    %   �� *Provider.Name+  5!      $ Количество��6%   	 . 
 5"   %   �� *Count+5$      �� 5%   
   -   5         ��Name$ stack��5%      $��1��6$   $��1��61   .+�5&      �� $��Дата��6   (  "System.Windows.Controls.DatePicker  5'         SelectedDate 6   �� *DateTimeAcc+�� 5(      $��Поставщик��6   �� 5)       �� DisplayMemberPath$
 Name��6c   ��6   �� *	Providers+ 69   �� *SelectedProvider+6c   �� 5*      $��Количество��6   �� 5+      ��6   �� *
CountAcc+5,      5-      5.      