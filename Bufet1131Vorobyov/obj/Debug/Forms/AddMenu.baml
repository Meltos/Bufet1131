   M S B A M L   `   `   `  ����L  HBufet1131Vorobyov, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bufet1131VorobyovBufet1131Vorobyov  5   	       Bufet1131Vorobyov.AddMenu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Bufet1131Vorobyov     5   	     � Title$  Меню��5   	   $	��450��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      $��0.05* �6   �� 5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��0.5* �6   �� 5      .+�5   
   o� $��3��6   .@�5      m� $"��Редактирование��6   .@�5      m� -   5        $��Добавить меню��5      62   m� 5      -   5        $��Удалить меню��5      61   m� 5      -   5        $'��!Опубликовать меню��5      66   m� 5      -   5        $+��%Распубликовать меню��5      68   5      5      &   System.Windows.Controls.DataGrid  5   
   $��1��6   $��2��6       AutoGenerateColumns .  ��6M   �� *Menus+ �� SelectedItem 6k   �� *SelectedMenu+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 67   �� *Name+6P   5       �� 5!   
   $��1��6   $��2��6"   .+�5"      �� $��Название��6   �� 5#      ��6   �� *
NameMenu+�� 5$      $��
Блюда��6   �� 5%      
 �� DisplayMemberPath$

 Name��6_   ��6   �� *	MenuFoods+ 69   �� *SelectedFood+6_   �� 5&      -   5        $!��Добавить блюдо��5&      6/   5'        5(   
   $��2��6   $��2��6#   $��2��60    .  ��6]   �� *Foods+ 6{   �� *SelectedFoodMenu+ � ContextMenu 5)      �� 5*      .@�5+      m� -   5        $��Удалить блюдо��5+      66   5,       5.        5/      $ Название��6%    . 	 67   �� *Name+6P   51      52      54      