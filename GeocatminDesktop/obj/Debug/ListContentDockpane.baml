   M S B A M L   `   `   `  ����a  ]ArcGIS.Desktop.Extensions, Version=12.4.0.0, Culture=neutral, PublicKeyToken=8fc3cc631e44ad86hJclr-namespace:ArcGIS.Desktop.Extensions;assembly=ArcGIS.Desktop.ExtensionsArcGIS.Desktop.Extensions  5   	    GeocatminDesktop.   (GeocatminDesktop.ListContentDockpaneView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   a
extensionsJclr-namespace:ArcGIS.Desktop.Extensions;assembly=ArcGIS.Desktop.Extensions     5   	     � 	Resources  5
       �� MergedDictionaries<   6ArcGIS.Desktop.Extensions.DesignOnlyResourceDictionary   Source�� 5   
    5        5      $S Mpack://application:,,,/ArcGIS.Desktop.Framework;component\Themes\Default.xaml?�6:   %    5      .��5      � $��Stretch=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   ��. -  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Local=�66   $��30��6_   .+�5      �� $��1��6   $��Center=�6A   $��Center=�6\       Esri_TextBlockDockPaneHeader#���   6y   ��6(   �� *	Heading+ � ToolTip 5      � 5       � Orientation$ Vertical=�6    $	��300��67   .+�5      �� $
��Wrap=�6=   ��6$   �� *	Heading+6=   5      5      5       � 5!   
   $��1��6   $��Stretch=�6   $��5q�6:    � ColumnDefinitions 5"      �� 5#      $
��Auto �6#   62   �� 5$      $��* �6#   6/   �� 5%      $
��Auto �6#   62    5'      �� 5(      6"   .+�5*      �� $��0��6   $��0��6(   $��0,0,5,0q�65   $��Center=�6F   $��Module��6a     Esri_TextBlockRegular#���  6o   �� 5+      $��1��6   $��0��6&   $��Stretch=�6~   $��Center=�6�   ��63   �� *	GdbPath+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� Mode$	 TwoWay=�6�   �� 5,      $��2��6   $��0��6%     Esri_Button#���  6Q   $��Center=�6w   ��62   �� *
OpenGdbCmd+.��6�   
Add to Map6�   5-      &   System.Windows.Controls.DataGrid  5.   
   $��2��6   $��5q�6    ��. 
   AutoGenerateColumns
 . $��Stretch=�51         HeadersVisibility$ Column=�52         RowHeaderWidth$ 0Q�53        Esri_DataGrid#���  55      ��54      ��  �� Path$ GdbDefinitions�$	 OneWay=�56      57      58      