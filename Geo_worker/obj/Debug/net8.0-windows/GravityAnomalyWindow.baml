   M S B A M L   `   `   `  ����  
Geo_worker%    Geo_worker.GravityAnomalyWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5   	     � Title$8  2График аномалии гравитации��5   	   $	��450��5   	   $	��800��5   	   .��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $
��Bold"�5      $��0,0,0,10q�5      $��Center=�5      ��5      �� *
PicketName+5        OxyPlot.Wpf.PlotView  5   
   $��1��6   $��0,0,0,10q�5        OxyPlot.Wpf.PlotViewBase   Model 5      �� *	PlotModel+5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�5      $��Right=�5      .+�5      �� -   5        $��Аномалия��5      $��80��5       $��0,0,10,0q�5!      5"      �� 5#      -   5        $��Высота��5#      $��80��5$      $��0,0,10,0q�5%      5&      �� 5'      -   5        $��Закрыть��5'      $��80��5(      5)      5*      5+      5,      