   M S B A M L   `   `   `  ����  
Geo_worker'    !Geo_worker.ObjectPropertiesWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$'  !Настройки объекта��5   	   $	��200��6#   $	��300��60   .��5      � $��	0,10,0,10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Имя:��6-   �� 5   
   -   5         ��Name$ txtName��5   /   $��0��6   $��1��6   $��0,0,0,10q�6@   �� 5   
   $��1��6   $��0��6   $,��&Родительский объект:��6-   �� 5   
   -   5         ��Name$ cbParent��5   0   $��1��6   $��1��6    $��0,0,0,10q�6B    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Id��5      �� 5   
   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5      �� -   5         ��Name$ btnSave��5      $��Сохранить��6&   $��80��6:   $��0,0,10,0q�6E   6W   �� 5      -   5        $ 	btnCancel��5      $��Отменить��6(   $��80��6;   6F   5      5      5      