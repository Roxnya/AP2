   M S B A M L   `   `   `  ����D  @WPFClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:WPFClient.ValidationRulesWPFClient.ValidationRules  5      $    WPFClient.UserControls.NewMaze   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >
validation'clr-namespace:WPFClient.ValidationRules     5      .��5	      �   +� FocusedElement  6   ��  �� ElementName$ txtName�� � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � 	Resources 5   
    �� 
TargetType �� BasedOn �� Property �� Value	 ��Name��%�  '��       7��'�� L     7���� 5      " ��6   8   .�5      ��  � ��$ 0��60   5      �� 5      " ��6   8   .�5      ��  � ��$ 1��60   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      �� .+�5   "   �� $	 textBox��6<   �� 5    "   
���  ����6,   �� *[0].ErrorContent+6N   5!      5"      5$      5%      5&      .+�5'   
   �� $��
Game Name:��6   $��0��6%   �� 5(   
   -   5        $��txtName��5(      $��0��6!   �� 5)   
   $��
Maze Rows:��6   $��1��6%   �� 5*   
   -   5        $��txtRows��5*      $��1��6!   .��6.   ��5+      �� 5,      
 �� Path$

 Rows�6    �� Mode$ TwoWay=�6&    �� UpdateSourceTrigger$ PropertyChanged=�64    �� ValidationRules 5-      9   3WPFClient.ValidationRules.StringToIntValidationRule  5.      ,  &System.Windows.Controls.ValidationRule   ValidationStep$ RawProposedValue=�6?   50      .��51      52      �� 53   
   $��Maze Columns:��6   $��2��6(   �� 54   
   -   5        $��
txtColumns��54      $��2��6$   .��61   ��55      �� 56      $
 Columns�6   $ TwoWay=�6)   $ PropertyChanged=�67    57        58      $ RawProposedValue=�6?   5:      .��5;      5<      �� 5=   
   -   5        $��btnStartGame��5=      $��Start New Game��6%   $��3��6>   $��2��6K   $��Center=�6_   $��5q�6|   $��10,10q�6�   ��5>      �� 5?      " ��6   # [B��66   .�5@      ��  � {� .  62    �� Triggers 5A      ^� 5B       ^� 
Conditions 5C      �� 5D   "    �� Binding 6,   �� $ 
txtColumns��$
 (Validation.HasError)� �� Value	 false6s   �� 5E   "    6,   �� $ txtRows��$
 (Validation.HasError)�	 false6p   .0�5G      ��  � {� . 6:   5H      5J      5L      5M      5N      