   M S B A M L   `   `   `  ����E  AGeo_worker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Geo_worker
Geo_worker  5   	   $    Geo_worker.ImportProjectWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Geo_worker     5   	     � Title$5  /Выбор проекта для импорта��5   	   $	��450��5	   	   $	��800��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�6   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ ProjectsDataGrid��5      $��0��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         SelectionMode$ Single=�5      $��0,0,0,20q�5      ��5      �� *
Projects+	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID проекта��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 5   #   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   #   �� *Id+5   #     5      $%
 Название проекта��6%   $ * 5!   #    5    #   �� *Name+5!   #     5"      $
 Заказчик��6%   $	 200 5$   #    5#   #   �� *CustomerName+5$   #     5%      $
 Руководитель��6%   $	 150 5'   #    5&   #   �� *EmployeeLogin+5'   #   5)      �� 5,   
   $��1��6    �� Orientation$ 
Horizontal=�5-      $��Right=�5.      .+�5/      �� -   5        $��Выбрать��5/      $	��100��50      $��30��51      $��0,0,10,0q�52      ���	#FF4CAF50
�������$��0q�55      56      �� 57      -   5        $��Отмена��57      $	��100��58      $��30��59      ���	#FFF44336
�������$��0q�5<      5=      5>      5?      5@      