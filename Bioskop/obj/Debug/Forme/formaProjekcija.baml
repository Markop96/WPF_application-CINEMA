   M S B A M L   `   `   `  ����B  >Bioskop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Bioskop.FormeBioskop.Forme  5   	   #    Bioskop.Forme.formaProjekcija   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Bioskop.Forme     5   	     � Title$  
PROJEKCIJE��5   	   $	��500��6   $	��800��6)   .��5	      � ��5
   
   �� 5       �� ImageSource$$ /Images/pozadinaProjekcija.png��6    j� Stretch$ UniformToFill=�6F   .+�5   
   �� $
��FILM��6   $��Center=�6   $��	0,0,0,360q�6<   $��Center=�6O    J� RenderTransformOrigin$ -0.662,0.51&�6j   �� 5   
   -   5         ��Name$ cbFilm��5      $��Center=�6#   $��	0,0,0,320q�6@   $��Center=�6S   $	��300��6n   $��23��6z   ���	#FFFF9B00 J� OpacityMask �	#FFFF9605��6�   Z� 6�   �� 5   
   $
��SALA��6   $��Center=�6   $��	0,0,0,270q�6<   $��Center=�6O   $ -0.662,0.51&�6j   �� 5   
   -   5         ��Name$ txtSala��5      $��Center=�6#   $��	0,0,0,230q�6@    �� TextWrapping$
 Wrap=�6S   $�� ��6g   $��Center=�6o   $	��300��6�   
������� �� SelectionBrush �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   $
��CENA��6   $��Center=�6   $��	0,0,0,190q�6<   $��Center=�6O   $ -0.662,0.51&�6j   �� 5   
   -   5        $ txtCena��5      $��Center=�6#   $��	0,0,0,150q�6@   $
 Wrap=�6S   $�� ��6g   $��Center=�6o   $	��300��6�   
������� �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   $��DATUM PROJEKTOVANJA��6   $��Center=�6.   $��	0,0,0,110q�6K   $��Center=�6^   $ -0.662,0.51&�6y   (  "System.Windows.Controls.DatePicker  5   
   -   5        	  Name$	 pcDatum��5      $��Center=�6&   $��0,0,0,60q�6C   $��Center=�6U   
���������6p   Z� 6�   �� 5   
   $��VREME PROJEKCIJE��6   $��Center=�6+   $��Center=�6H   $ -0.662,0.51&�6c   $��0,0,0,10q�6�   �� 5   
   -   5        $ txtVreme��5      $��Center=�6$   $
 Wrap=�6A   $�� ��6U   $��Center=�6]   $	��300��6x   
�������$��0,40,0,0q�6�    �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   -   5        
 ��Name$
 
btnSacuvaj��5      $��Sačuvaj��6>   $��Center=�6P   $��100,0,0,150q�6m   $��Bottom=�6�   $��75��6�   ���	#FF6AFF00$
��Bold"�6�   
���   ���6�   Z� 6�   �� 5   
   -   5        $
 	btnOtkazi��5      $��Otkaži��6<   $��Center=�6M   $��0,0,100,150q�6j   $��Bottom=�6   $��75��6�   
���  ��$
��Bold"�6�   
���   ���6�   Z� 6�   5       5!      