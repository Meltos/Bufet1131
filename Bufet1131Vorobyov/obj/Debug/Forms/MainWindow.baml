   M S B A M L   `   `   `  ����L  HBufet1131Vorobyov, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bufet1131VorobyovBufet1131Vorobyov  5   	   "    Bufet1131Vorobyov.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Bufet1131Vorobyov     5   	     � Title$  
Буфет��5   	   $	��450��6   $	��800��6$    � WindowStartupLocation$ CenterScreen=�60   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   o� $��3��6   .@�5      m� $��Редактор��6   .@�5      m� -   5        $!��Добавить блюдо��5      63   m� 5      -   5        $!��Составить меню��5      63   m� 5      -   5        $��Поставщики��5      6/   5      m� 5      -   5        $��Учёт заказов��5      6-   5      �� 5   
   $��1��6   .+�5      �� $��Выбрать меню��6   �� 5       �� DisplayMemberPath$
 Name��6   ��60   �� *Menus+ �� SelectedItem 6N   �� *SelectedMenu+5      &   System.Windows.Controls.DataGrid  5    
   $��2��6   $��2��6       AutoGenerateColumns .  ��64   �� *Foods+   Columns 5"      4  .System.Windows.Controls.DataGridTemplateColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Картинка��6)   	   CellTemplate	 5$      �� 5%      .R�5&      �� $��IsReadImage��6A   $��50��6$   $��50��63   ��6T   �� *	PathIMG+5'      5)      0  *System.Windows.Controls.DataGridTextColumn  5*      $ Название��6%   
   
IsReadOnly
 . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5+   %   �� *Name+  5,      $ Цена��6%   
 .  5-   %   �� *Price+  5.      $ Количество��6%   
 .  5/   %   �� *Count+  50      $ Описание��6%   
 .  51   %   �� *Description+54      55      56      