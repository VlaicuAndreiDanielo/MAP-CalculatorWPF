   M S B A M L   `   `   `  ����   CalculatorApp    CalculatorApp.Ecuation   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Equation Calculator��$	��450��$	��700�� � 
ResizeMode$ NoResize=�   WindowBackground#���     WindowForeground#���   � 	Resources   EqButtonStyle �� 
TargetType �� Property �� Value  ButtonBackground  ButtonForeground  ButtonBorderBrush �� 
TargetType �� CornerRadius  DisplayTextBlockStyle  TextBlockBackground  TextBlockForeground 	 DarkMenuItemStyle �� Triggers	 P� Property
 P� Value��%�  &	       &	 8    &		 �    �� " ��.���  � ��$ 16(���  � ��$
 Bold"���  � ��$ 4q���  � ��$ 10q���  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��$ 1q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ 6��#��z��#��z��#��z��._��� $��Center=�$��Center=��� " ��.���  � ��$ 48(���  � ��$ Right=���  � ��$ Center=���  � ��$ 5q���  �@��Padding$ 10q���  � ��# �  ��  � ��# �  �� " m�.���  � ��
 �������  � ��
 ���� ��  � ��$ 16(���  � ��$	 8,4q� P� 	 �@m�IsHighlighted$

 True��. ���  � ��
 �   ���  � �� �	#FFDDDDDD.��]� .+�o� $	��Top=����	#FF2D2D30
�������#��[	 .@�m� $��_File��.@�m� -   $	��Cut��m� -   $
��Copy��m� -   $��Paste��m� $
��Mode��.@�m� -   $��Standard��m� -   $��
Programmer��m� -   $��
Scientific��m� -   $��Ecuation��m� $��_Help��.@�m� -	   $��Istoric��m� -
   $��About��m� $��_Themes��.@�m� -   $��Heaven Light��m� -   $��Total Darkness��m� -   $��Forest Green��m� -   $��Quantum Red��m� -   $��	Deep Blue��m� -   $��Pale Lavender��m� -   $��Sunny Yellow��m� -   $��Deep Orange��m� -   $��Intense Violet��m� -   $��	Baby Pink���� -   #���  $��50��$
��Exit��� $��10q� � RowDefinitions �� $
��Auto ��� $��* �.+��� -    ��Name$ txtEquation��$��0��$��24(� 
 CalculatorTextBoxStyle#��� 
 $��1q� �� 
IsReadOnly$
 True��$��5q�$��8q�$��Stretch=�A� $��1�� A� Rows$ 5�� A� Columns$ 4��$��5q�.+��� -   #���  $��7���� -   #���  $��8���� -   #���  $��9���� -   #���  $��/���� -   #���  $��4���� -   #���  $��5���� -   #���  $��6���� -   #���  $��*���� -   #���  $��1���� -    #���  $��2���� -!   #���  $��3���� -"   #���  $��-���� -#   #���  $��0���� -$   #���  $��(���� -%   #���  $��)���� -&   #���  $��+���� -'   #���  $��C���� -(   #���  $��.���� -)   #���  $��=���� -*   #���  $	��⌫��