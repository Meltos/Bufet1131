   M S B A M L   `   `   `  ����L  HBufet1131Vorobyov, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bufet1131VorobyovBufet1131Vorobyov  5   	       Bufet1131Vorobyov.AddFood   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Bufet1131Vorobyov     5   	     � Title$  
Блюда��5   	   $	��450��6   $	��800��6$    � WindowStartupLocation$ CenterScreen=�60   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      $
��0.3* �6   �� 5      $
��0.3* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      $
��0.1* �6   �� 5      $
��0.4* �6   .+�5   
   o� $��2��6   .@�5      m� $��Редактор��6   .@�5      m� -   5        $!��Добавить блюдо��5      63   m� 5      -   5        $��Удалить блюдо��5      62   5      5      &   System.Windows.Controls.DataGrid  5   
   $��3��6   $��1��6$      AutoGenerateColumns .  ��61   �� *Foods+ �� SelectedItem 6O   �� *SelectedFood+   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Картинка��6)      CellTemplate 5      �� 5       .R�5!      �� $��IsReadImage��6A   $��50��6$   $��50��63   ��6T   �� *	PathIMG+5"      5$      0  *System.Windows.Controls.DataGridTextColumn  5%      $ Название��6%   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 5&   %   �� *Name+  5'      $ Цена��6%   	 . 
 5(   %   �� *Price+  5)      $ Количество��6%   	 . 
 5*   %   �� *Count+  5+      $ Описание��6%   	 . 
 5,   %   �� *Description+5.      �� 5/   
   $��1��6   $��1��6"   $��2��62   .+�50      �� $!��Название блюда��6   �� 51      ��6   �� *
NameFood+�� 52      $��Цена��6   �� 53      ��6   �� *	PriceFood+�� 54      $��Количество��6   �� 55      ��6   �� *	CountFood+�� 56      $��Описание��6   �� 57      ��6   �� *DescriptionFood+�� 58      $��Картинка��6   �� 59      ��6   �� *PathIMGFood+5;      �� 5<   
   $��Меню��6   $��2��6   $��1��6,   �� 5=   
   $��3��6   $��1��6    �� DisplayMemberPath$
 Name��6M   ��6/   �� *Menus+6M   �� 5>   
   $��2��6   $��2��6   $��Поставщики��6-   �� 5?   
   $��3��6   $��3��6   $
 Name��6Q   ��6/   �� *	Providers+6Q   5@      5A      