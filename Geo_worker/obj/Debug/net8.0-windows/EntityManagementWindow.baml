   M S B A M L   `   `   `  ����  
Geo_worker'    !Geo_worker.EntityManagementWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$/  )Управление сущностями��5   	   $	��500��6'   $	��800��64   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,10q�6;   .+�5      �� $��Тип сущности:��6   $��Center=�6,   �� 5      -   5         ��Name$ EntityTypeComboBox��5      $	��200��63   .@�5      �� $��Project��6   �� 5      $��Customer��6   �� 5      $��CustomerType��6   �� 5      $��Employee��6   �� 5      $��	Equipment��6   �� 5      $��Report��6   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ EntitiesDataGrid��5       $��1��6      AutoGenerateColumns$
 True��6:      
IsReadOnly$
 True��6U      SelectionMode$ Single=�5      $��0,0,0,10q�6*   �� 5   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5      �� -   5        $��Добавить��5      $��80��6(   $��0,0,10,0q�63   6E   �� 5       -   5        $��Изменить��5       $��80��6(   $��0,0,10,0q�63   6E   �� 5!      -   5        $��Удалить��5!      $��80��6'   $��0,0,10,0q�62   6D   �� 5"      -   5        $��Закрыть��5"      $��80��6'   62   5#      5$      5%      