   M S B A M L   `   `   `  ����  
VHD_HELPER    VHD_HELPER.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
VHD HELPER��5   	   $	��350��6   $	��825��6)    � 	Resources 5      ��%    5      .��5      � ��5   
   �� 5	       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5
      �  � Color$ 	#88556B2F��6    � Offset$ 0.004Q�61   � 5      $ 	#AA8AC12B��6   � 5      $ DarkOliveGreen��6   $ 1Q�66   5      .+�5   
   ]� ��. ���	#FFF0F0F0.+�5      �� $��bottom=�6   .+�5      �� -   5        $��	statusbar��5      ���#2E8DEF
�������.@�5      �� .��5      �� -   5        $��status_text��5   $   5      5      5      �� 5      $��Right=�6   $��	0,10,30,0q�60   .+�5      �� $��Center=�6   $
��Bold"�65   .��6G   Attached VHDS5      &   System.Windows.Controls.DataGrid  5      -   5        $��VHDDataGrid��5   0   $	��top=�6   $��	10,10,0,0q�6C   $	��400��6V   
�������   HeadersVisibility$ Column=�6u   $��Bottom=�6�   $��14(�6�   $	��180��6�   ���	#FFF3F3F3   RowBackground �#2E8DEF	   AlternatingRowBackground
	 ��ح�$��Center=�6  
   CanUserAddRows
 .  -  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Continue=�6P     AutoGenerateColumns .    GridLinesVisibility$
 None=�6�   %� VirtualizationMode$ Standard=�6�     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�  $   HorizontalScrollBarVisibility$
 Auto=�6�     ColumnHeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5      ��  �� Property � �� �� Value$
 Bold"�6;   �� 5       � ��
 �   �6;   5          Columns 5"      4  .System.Windows.Controls.DataGridCheckBoxColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Select��61      Width$
 Auto 6B      HeaderTemplate 5$      �� 5%   "   InstallableChkBx��.R�5&   &   ��  ��Name$ InstalledChkBxAll��6/   $��0,0,0,0q�6K   $
��Left=�6\   $
��auto��6w   $
��auto��6�   5'   #   5)      5+      5,      �� 5-      $
��Left=�6   $��	40,60,0,0q�60   .+�5.      �� -   5        $	��100��5.      $��50��6&   $
��Left=�62   $	��Top=�6i   $��Attach��6�   �� 5/      -   5        $	��100��5/      $��50��6&   $��0,10,0,0q�62   $
��Left=�6D   .��6{   Detach6�   50      51      52      53      