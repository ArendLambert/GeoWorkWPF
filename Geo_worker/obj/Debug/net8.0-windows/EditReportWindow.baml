   M S B A M L   `   `   `  ����  
Geo_worker!    Geo_worker.EditReportWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #Редактирование Report��5   	   $	��250��6'   $	��400��64   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Сотрудник:��6-   �� 5   
   -   5         ��Name$ 
cbEmployee��5   0   $��0��6   $��1��6     �� DisplayMemberPath$ Login��5       �� SelectedValuePath$ Id��6-   $��0,0,0,5q�6D   �� 5   
   $��1��6   $��0��6   $��Проект:��6-   �� 5   
   -   5        $ 	cbProject��5   0   $��1��6   $��1��6    $
 Name��5      $ Id��6,   $��0,0,0,5q�6C   �� 5   
   $��2��6   $��0��6   $��Файл отчета:��6-   �� 5   
   -   5         ��Name$ txtReportFile��5   /   $��2��6   $��1��6   $��0,0,0,5q�6F   �� 5   
   $��3��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5      �� -   5        $��Сохранить��5      $��80��6)   $��0,0,10,0q�64   6F   �� 5      -   5        $��Отменить��5      $��80��6(   63   5      5       5!      