   M S B A M L   `   `   `  ����M  IAttendaanceTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AttendaanceTrackerAttendaanceTracker  5   	        AttendaanceTracker.Tracker   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AttendaanceTracker     5   	     � Title$  Tracker��5   	   $	��450��6   $	��800��6&   .��5	      � .+�5
   
   �� -   5         ��Name$ userDetails��5
      $
��Left=�6)    �� TextAlignment$ Center=�6D   $��
515,10,0,0q�6[   $
��Wrap=�6o   $
��Bold"�6�   $�� ��6�   $	��Top=�6�   $	��184��6�   $��31��6�   �� 5   
   -   5        $ loggedInTimer��5      $
��Left=�6+   $ Center=�6F   $��
222,10,0,0q�6]   $
��Wrap=�6q   $
��Bold"�6�   $�� ��6�   $	��Top=�6�   $	��184��6�   $��31��6�   �� 5   
   -   5        $ LabelLastInteraction��5      $
��Left=�62   $
 Left=�6M   $��	10,41,0,0q�6b   $
��Wrap=�6u   $
��Bold"�6�   $��Latest Interaction at:��6�   $	��Top=�6�   $	��184��6�   $��32��6�   �� 5   
   -   5        $ LastInteraction��5      $
��Left=�6-   $ Center=�6H   $��
222,41,0,0q�6_   $
��Wrap=�6s   $
��Bold"�6�   $�� ��6�   $	��Top=�6�   $	��184��6�   $��32��6�   �� 5   
   -   5        $ LabelLoggedIn��5      $
��Left=�6+   $
 Left=�6F   $��	10,10,0,0q�6[   $
��Wrap=�6n   $
��Bold"�6�   $��Logged in timer:��6�   $	��Top=�6�   $	��184��6�   $��32��6�   �� 5   
   -   5        $  LabelInteractionDifference��5      $
��Left=�68   $
 Left=�6S   $��	10,73,0,0q�6h   $
��Wrap=�6{   $
��Bold"�6�   $��Screen Idle Time:��6�   $	��Top=�6�   $	��184��6�   $��32��6�   �� 5   
   -   5        $ InteractionDifference��5      $
��Left=�63   $ Center=�6N   $��
222,73,0,0q�6e   $
��Wrap=�6y   $
��Bold"�6�   $�� ��6�   $	��Top=�6�   $	��184��6�   $��32��6�   �� 5   
   -   5         ��Name$ logout��5      $��LOGOUT��6!   $
��Left=�62   $��
706,10,0,0q�6M   $	��Top=�6a   $��76��6y   $��31��6�   
���  ��
�������$��15(�6�   6�   5      5      