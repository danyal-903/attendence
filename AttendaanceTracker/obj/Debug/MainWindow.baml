   M S B A M L   `   `   `  ����M  IAttendaanceTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AttendaanceTrackerAttendaanceTracker  5   	   #    AttendaanceTracker.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AttendaanceTracker     5   	     � Title$  Attendaance Tracker��5   	   $	��450��6%   $	��800��62   .��5	      � .+�5
   
   �� -   5         ��Name$ username��5
      {�. $��15(�65   $	��1pxq�6C   
���   � �� TextAlignment$
 Left=�6m   $��Center=�6�   $��30��6�    �� CharacterCasing$ Lower=�6�   $��264,82,103.309,298.655q�6�    �� TextWrapping$
 Wrap=�6�   $��Center=�6�   $	��400��6  6  G� 5   
   -   5         G�Name$ password��5      {�. $��15(�69   $	��1pxq�6G   
���   �$
��Left=�6q    G� PasswordChar$ *��6�   $��Center=�6�   $��30��6�   $��264,156,103.309,224.655q�6�   $��Center=�6�   $	��400��6  6  �� 5   
   -   5         ��Name$ login��5       �� 	IsDefault . {�.  
����  �
�������$��LOGIN��6h   $��15(�6x   $��Center=�6�   $��326,271,325.309,106.655q�6�   $��Center=�6�   $	��142��6�   $��43��6�   �� 5   
   -   5        	 ��Name$	 label_username��5      $��	Username:��6(   $��15(�6<   $
��Left=�6K   $��
143,82,0,0q�6f   $	��Top=�6z   $��40��6�   $��91��6�   �� 5   
   -   5        $	 label_password��5      $��	Password:��6(   $��15(�6<   $
��Left=�6K   $��143,156,0,0q�6f   $	��Top=�6{   $��40��6�   $��91��6�   �� 5   
   -   5        $	 label_error��5      $�� ��6%   
���  ��$��Center=�6A   $��Center=�6g   $��143,220,116.309,175.655q�6�   $��Center=�6�   $	��534��6�   $��30��6�   5      5      