   M S B A M L   `   `   `  ����G  COkosTukorWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:OkosTukorWPFOkosTukorWPF  5   	       OkosTukorWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:OkosTukorWPF     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5      � .+�5   
   �� -   5         ��Name$ 	btn_Recog��5      $��Start Facial Recognition��6$   $
��Left=�6G   $��602,367,0,0q�6b   $	��Top=�6w   $	��180��6�   $��42��6�   ���	#FFA800006�   �� 5   
   -   5        $ btn_TrainModel��5      $��Train Model��6)   $
��Left=�6?   $��602,320,0,0q�6Z   $	��Top=�6o   $	��180��6�   $��42��6�   ���	#FFFFDC006�   �� 5   
   -   5        $ btn_Extract��5      $��Extract Embeddings��6&   $
��Left=�6C   $��602,273,0,0q�6^   $	��Top=�6s   $	��180��6�   $��42��6�   ���	#FFFFDC006�   �� 5   
   -   5         ��Name$ DetectedUser��5      $�� ��6&   $
��Left=�61   $��
576,61,0,0q�6L   $	��Top=�6`   $	��152��6x   $��51��6�   �� 5   
   -   5        $ btn_RegisterU��5      $��Register User��6(   $
��Left=�6@   $��
10,367,0,0q�6[   $	��Top=�6o   $	��180��6�   $��42��6�   ���	#FF159B006�   �� 5   
   -   5         ��Name$ txtbox_name��5      $
��Left=�6'   $��23��6B   $��
10,240,0,0q�6N    �� TextWrapping$
 Wrap=�6b   $
��Name��6v   $	��Top=�6�   $	��180��6�   #  Xceed.Wpf.Toolkit.ColorPicker  5   
   -   5        $��cp_color��5      $��10,297,602,69q�6+      DisplayColorAndName . !   AvailableColorsSortingMode$ HueSaturationBrightness=�6]   �� 5   
   -	   5        $ btn_OpenMirror��5      $��Open Mirror Window��6)   $
��Left=�6F   $��309,273,0,0q�6a   $	��Top=�6v   $	��180��6�   $	��136��6�   ���	#FFFF00E8$��18(�6�   6�   �� 5   
   -
   5        $ btn_Save��5      $
��Save��6#   $
��Left=�62   $��204,367,0,0q�6M   $	��Top=�6b   $��43��6z   $��42��6�   ���	#FFFF97006�   �� 5   
   -   5        $ btn_Read��5      $
��Read��6#   $
��Left=�62   $��252,367,0,0q�6M   $	��Top=�6b   $��43��6z   $��42��6�   ���	#FFFF97006�   �� 5   
   -   5         ��Name$ list_u��5      $
��Left=�6"   $	��225��6=   $��	10,10,0,0q�6J   $	��Top=�6]   $	��180��6u   6�   �� 5   
   -   5        $ textbox_loc��5      $
��Left=�6'   $��23��6B   $��
282,10,0,0q�6N   $
 Wrap=�6b   $��Location��6v   $	��Top=�6�   $	��120��6�   �� 5   
   $��	Location:��6   $
��Left=�6$   $��	209,9,0,0q�6?   $	��Top=�6R   $��61��6j   �� 5   
   -   5        $ textbox_rssfeed��5      $
��Left=�6+   $��23��6F   $��
282,41,0,0q�6R   $
 Wrap=�6f   $	��Rss��6z   $	��Top=�6�   $	��489��6�   �� 5   
   $��Rssfeed:��6   $
��Left=�6#   $��
209,38,0,0q�6>   $	��Top=�6R   $��61��6j   �� 5   
   -   5        $��Change Data��5      $
��Left=�6'   $��
209,78,0,0q�6B   $	��Top=�6V   $	��135��6n   6z   5       5!      