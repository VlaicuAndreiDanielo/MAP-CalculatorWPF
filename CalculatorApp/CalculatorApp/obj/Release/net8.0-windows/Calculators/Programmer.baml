   M S B A M L   `   `   `  ����   CalculatorApp    CalculatorApp.Programmer   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Programmer Calculator��$	��700��$	��600�� � 
ResizeMode$ NoResize=�   WindowBackground#���     WindowForeground#���   � 	Resources   CalcButtonStyle �� 
TargetType �� Property �� Value  ButtonBackground  ButtonForeground  ButtonBorderBrush �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value  DisabledButtonBackground  DisabledButtonForeground  DisabledButtonBorderBrush 	 DisplayTextBlockStyle 
 TextBlockBackground  TextBlockForeground  DarkMenuItemStyle��%O  &	       &		 �    &	 d    �� " ��.���  � ��$ 18(���  � ��$
 Bold"���  � ��$ 4q���  � ��$ 10q���  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��$ 1q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ 6��#��z��#��z��#��z��._��� $��Center=�$��Center=� P� 	 � {�$
 False��. ���  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@J�Opacity$	 0.7Q��� " ��.���  � ��$ 48(���  � ��$ Right=���  � ��$ Center=���  � ��$ 5q���  �@��Padding$ 10q���  � ��# � 
 ��  � ��# �  �� " m�.���  � ��
 �������  � ��
 ���� ��  � ��$ 16(���  � ��$	 8,4q� P� 	 �@m�IsHighlighted$

 True��. ���  � ��
 �   ���  � �� �	#FFDDDDDD.��]� .+�o� $	��Top=����	#FF2D2D30
�������#��[ .@�m� $��_File��.@�m� -   $	��Cut��m� -   $
��Copy��m� -   $��Paste���� m� -    m�Name$! menuDigitGroupingProgrammer��$��Digit Grouping�� m� IsCheckable$
 True��m� $��_Mode��.@�m� -   $��Standard��m� -   $��
Programmer��m� -   $��
Scientific��m� -	   $��Ecuation��m� $��_Help��.@�m� -
   $��Istoric��m� -   $��About��m� $��_Themes��.@�m� -   $��Heaven Light��m� -   $��Total Darkness��m� -   $��Forest Green��m� -   $��Quantum Red��m� -   $��	Deep Blue��m� -   $��Pale Lavender��m� -   $��Sunny Yellow��m� -   $��Deep Orange��m� -   $��Intense Violet��m� -   $��	Baby Pink���� -   #���  $��50��$
��Exit��� $��10q� � RowDefinitions �� $
��Auto ��� $��* ��� $��* � � ColumnDefinitions �� $��2* ��� $��* �.+��� -    ��Name$ txtDisplayProgrammer��  CalculatorTextBlockStyle#���  $��0��$��2��$��0��$��40(�$��Right=�$��Center=�$��5q� �� Padding$ 10q��� $��1��$��0�� �� Orientation$ Vertical=�$��5q�.+��� $	��HEX��$
��Bold"�$��18(��� -   $ txtHexValue��$��0��$
��Wrap=�$��0,0,0,10q��� $	��DEC��$
��Bold"�$��18(��� -   $ txtDecValue��$��0��$
��Wrap=�$��0,0,0,10q��� $	��OCT��$
��Bold"�$��18(��� -   $ txtOctValue��$��0��$
��Wrap=�$��0,0,0,10q��� $	��BIN��$
��Bold"�$��18(��� -   $ txtBinValue��$��0��$
��Wrap=�$��0,0,0,10q��� $��1��$��1��$ Vertical=�$��5q�.+�A�  A� Rows$ 4�� A� Columns$ 2��$��5q�.+��� -   #���  $	��AND���� -   #���  $��OR���� -   #���  $	��XOR���� -   #���  $	��NOT���� -    #���  $
��NAND���� -!   #���  $	��NOR���� -"   #���  $��<<���� -#   #���  $��>>��A� $ 2��$ 2��$��5q�.+��� -$   #���  $	��BIN���� -%   #���  $	��OCT���� -&   #���  $	��DEC���� -'   #���  $	��HEX��A� $��2��$��2��$ 7��$ 4��$��5q�.+��� -(    ��Name$
 BtnA��#���  $��A���� -)   $
 Btn7��#���  $��7���� -*   $
 Btn8��#���  $��8���� -+   $
 Btn9��#���  $��9���� -,   $
 BtnB��#���  $��B���� --   $
 Btn4��#���  $��4���� -.   $
 Btn5��#���  $��5���� -/   $
 Btn6��#���  $��6���� -0   $
 BtnC��#���  $��C���� -1   $
 Btn1��#���  $��1���� -2   $
 Btn2��#���  $��2���� -3   $
 Btn3��#���  $��3���� -4   $
 BtnD��#���  $��D���� -5   #���  $��Clear���� -6   $
 Btn0��#���  $��0���� -7   #���  $	��⌫���� -8   $
 BtnE��#���  $��E���� -9   #���  $��+���� -:   #���  $��-���� -;   #���  $��=���� -<   $
 BtnF��#���  $��F���� -=   #���  $��*���� ->   #���  $��/���� -?   #���  $��%���� -@   #���  $��(���� -A   #���  $��)���� -B   #���  $��±���� -C   #���  $��.��