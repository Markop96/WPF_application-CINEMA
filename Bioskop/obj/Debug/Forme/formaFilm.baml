   M S B A M L   `   `   `  ����B  >Bioskop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Bioskop.FormeBioskop.Forme  5   	       Bioskop.Forme.formaFilm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Bioskop.Forme     5   	     � Title$  FILMOVI��5   	   $	��500��6   $	��800��6&   .��5	      � ��5
   
   �� 5       �� ImageSource$ /Images/pozadinaFilm.png��6    j� Stretch$ UniformToFill=�6@   .+�5   
   �� $��NAZIV FILMA��6   $��Center=�6&   $��	0,0,0,360q�6C   $��Center=�6V    J� RenderTransformOrigin$ -0.662,0.51&�6q   �� 5   
   -   5         ��Name$ txtNazivFilma��5      $��Center=�6)   $��	0,0,0,320q�6F    �� TextWrapping$
 Wrap=�6Y   $�� ��6m   $��Center=�6u   $	��300��6�   
������� �� SelectionBrush �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   $��ŽANR��6   $��Center=�6   $��	0,0,0,280q�6<   $��Center=�6O   $ -0.662,0.51&�6j   �� 5   
   -   5        $ txtZanr��5      $��Center=�6#   $��	0,0,0,240q�6@   $
 Wrap=�6S   $�� ��6g   $��Center=�6o   $	��300��6�   
������� �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   $��REŽISER��6   $��Center=�6"   $��	0,0,0,200q�6?   $��Center=�6R   $ -0.662,0.51&�6m   �� 5   
   -   5        $ 
txtReziser��5      $��Center=�6&   $��	0,0,0,160q�6C   $
 Wrap=�6V   $�� ��6j   $��Center=�6r   $	��300��6�   
������� �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   $��TRAJANJE��6   $��Center=�6#   $��	0,0,0,120q�6@   $��Center=�6S   $ -0.662,0.51&�6n   �� 5   
   -   5        $ txtTrajanje��5      $��Center=�6'   $
 Wrap=�6D   $�� ��6X   $��Center=�6`   $	��300��6{   
������� �	#FFFF4B00$��0,0,0,80q�6�   $��23��6�   ��6�   Z� 6�   �� 5   
   $��GODINA IZLASKA��6   $��Center=�6)   $��0,0,0,40q�6F   $��Center=�6X   $ -0.662,0.51&�6s   �� 5   
   -   5        $ txtGodinaIzlaska��5      $��Center=�6,   $
 Wrap=�6I   $�� ��6]   $��Center=�6e   $	��300��6�   
�������$��0,0,0,0q�6�    �	#FFFF4B00$��23��6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sačuvaj��6%   $��Center=�67   $��100,0,0,150q�6T   $��Bottom=�6i   $��75��6�   ���	#FF6AFF00$
��Bold"�6�   
���   ���6�   Z� 6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkaži��6$   $��Center=�65   $��0,0,100,150q�6R   $��Bottom=�6g   $��75��6�   
���  ��$
��Bold"�6�   
���   ���6�   Z� 6�   5       5#      