   M S B A M L   `   `   `  ����  HoffmanAlgorithm!    HoffmanAlgorithm.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Huffman Algorithm��5   	   $	��480��6#   $	��640��60    � 
ResizeMode$ NoResize=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��30 �6   6,   �� 5	      6!   �� 5
      6!   �� 5      $��60 �6   6,   �� 5      $��60 �6   6,   �� 5      $��30 �6   6,    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��30 �6   6+   �� 5      $��20 �6   6*   �� 5      6   �� 5      $��20 �6   6*   �� 5      6   �� 5      $��20 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   .+�5   
   �� -   5        $��TextBoxPath��5      $��1��6%   $��2��65   $��1��6I   $��16(�6V   6f   �� 5   
   -   5        $��TextOpen��5       �� TextWrapping$
 Wrap=�6"   $��1��67   $��4��6G   $��3��6[   6k   �� 5   
   -   5        $��TextEncoded��5      $
 Wrap=�6%   $��1��69   $��4��6I   $��5��6]   6m   �� 5   
   -   5        $��TextBoxSave��5      $��1��6%   $��3��65   $��7��6I   $��16(�6V   6f   �� 5!   
   -   5        $��
ButtonOpen��5!      $��3��6#   $��1��63   .��6Y   Encode6a   �� 5"   
   -   5        $��ButtonDecode��5"      $��4��6%   $��1��65   .��6]   Decode6e   �� 5#   
   -   5        $��
ButtonSave��5#      {�.  $��4��65   $��7��6E   .��6k   	Encode to6v   5%      5(      