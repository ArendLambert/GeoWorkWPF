   M S B A M L   `   `   `  ����E  AGeo_worker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Geo_worker
Geo_worker  5   	   $    Geo_worker.CreateProjectWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Geo_worker     5   	     � Title$  CreateProjectWindow��5   	   $	��450��6%   $	��800��62   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Название:��6-   �� 5   
   -   5         ��Name$ txtName��5   /   $��0��6   $��1��6   $��0,0,0,5q�6@   �� 5   
   $��1��6   $��0��6   $��Заказчик:��6-   �� 5   
   -   5         ��Name$ CustomerProjectCombobox��5   0   $��1��6   $��1��6     �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Id��6   $��0,0,0,5q�61   �� 5   
   $��2��6   $��0��6   $��Руководитель:��6-   �� 5   
   -   5        $ HeadProjectCombobox��5   0   $��2��6   $��1��6    $ Passport��5       $ Id��6$   $��0,0,0,5q�6;   �� 5#   
   $��5��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5$      �� -   5        $��Сохранить��5$      $��80��6)   $��0,0,10,0q�64   6F   �� 5%      -   5        $��Отменить��5%      $��80��6(   63   5&      5'      5(      