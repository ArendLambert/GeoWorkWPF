   M S B A M L   `   `   `  ����  
Geo_worker!    Geo_worker.EditSquareWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$)  #Редактирование Square��5   	   $	��250��6'   $	��400��64   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6!   $��Имя:��61   �� 5      -   5         ��Name$ txtName��5   3   $��0��6   $��1��6#   $��0,0,0,10q�6D   �� 5      $��1��6   $��0��6!   $��Высота:��61   �� 5      -   5        $ 
txtAlitude��5   3   $��1��6   $��1��6#   $��0,0,0,10q�6G   �� 5      $��2��6   $��0��6!   $��Проект:��61   �� 5      -   5         ��Name$ 	cbProject��5   4   $��2��6   $��1��6$    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Id��60   $��0,0,0,10q�6G   5      �� 5!   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5"      �� -   5         ��Name$ btnSave��5"      $��Сохранить��6&   $��80��6:   $��0,0,10,0q�6E   6W   �� 5#      -   5        $ 	btnCancel��5#      $��Отменить��6(   $��80��6;   6F   5$      5%      5&      