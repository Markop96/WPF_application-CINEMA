   M S B A M L   `   `   `  ����B  >Bioskop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:BioskopBioskop  5   	       Bioskop.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Bioskop     5   	     � Title$  Pocetna stranica��5   	   $	��500��6"   $	��800��6/   .��5	      � $��0,0,0,0q�6   ��5
   
   �� 5       �� ImageSource$ /Images/pozadina.jpg��6    j� Stretch$ UniformToFill=�6<   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridCentralni��5      $��Center=�6.   $	��Top=�6K   $
��auto��6c   $	��150��6q   $
��Auto��6�      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6�      MinColumnWidth$ 80Q�6�      RowBackground �	#FF3780DE   AlternatingRowBackground �#88d4fc	   CanUserAddRows	 .  
   SelectionMode$
 Single=�5   _   ���	#FFFF0046
���   � J� OpacityMask �	#FF090909$��	0,200,0,0q�6�    J� Opacity$	 0.9Q�6�   $��Franklin Gothic Heavy)�6�    � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell��%4  '        7 '  �     �� 5      8   "  6Q   .�5      ��  � �� �	#FFFD005E63   �� 5       � ��$ ARIAL BLACK)�63   �� 5       � ��
 �����63   �� 5       � ��$ Center=�6<   5      �� 5      "  6   .�5      ��  � ��$ Center=�6C   5      5      5      �� 5   
   $��Center=�6   $��35��62   $��	0,135,0,0q�6>   $	��Top=�6Q   $	��580��6i    �� Orientation$ 
Horizontal=�6u   .+�5       �� -   5         ��Name$ btnFilm��5       $��FILMOVI��6<   $��80��6N   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   �� 5"      -   5        $ btnProjekcija��5"      $��
PROJEKCIJE��6H   $��80��6]   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   �� 5$      -   5        $ btnPlacanje��5$      $��PLACANJA��6D   $��80��6W   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   �� 5&      -   5        $ btnRezervacija��5&      $��REZERVACIJE��6J   $	��100��6`   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   �� 5(      -   5        $ btnKorisnik��5(      $��	KORISNICI��6D   $��80��6X   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   �� 5*      -   5        $ btnZaposleni��5*      $��	ZAPOSLENI��6F   $��80��6Z   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   �� 5,      -   5        $ btnRecenzija��5,      $��	RECENZIJE��6F   $��80��6Z   ���	#FF0152BC
�������$
��Bold"�6�   $��Segoe UI Black)�6�   $��10(�6�   ��6�   Z� 6�   5.      �� 50   
   $��Center=�6   $	��112��6-   $	��Top=�6:   $	��304��6R   $��/Images/logoEONIS.png��6^   $��0,40,0,0q�6}   �� 52   
   $��Center=�6   $��0,0,0,50q�62   $��Bottom=�6D   $	��320��6_   $ 
Horizontal=�6k   .+�53      �� -	   5        $ btnDodaj��53      $��DODAJ��6@   $	��100��6P   $��30��6\   $��0,0,10,0q�6h   ���	#FF34FF00
���   �$
��Bold"�6�   $��Bottom=�6�   
 �   �6�   �� 54      -
   5        $ 	btnIzmeni��54      $��IZMENI��6G   $	��100��6X   $��30��6d   $��0,0,10,0q�6p   $
��Bold"�6�   
�������
���   �$��Bottom=�6�   
 �   �6�   �� 55      -   5        $ 	btnObrisi��55      $��OBRIŠI��6G   $	��100��6X   $��30��6d   $
��Bold"�6p   
���  ��
���  ��$��Bottom=�6�   
 �   �$��Center=�6�   56      5=      5>      