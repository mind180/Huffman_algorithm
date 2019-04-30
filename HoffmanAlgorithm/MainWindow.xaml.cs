using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;

namespace HoffmanAlgorithm
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        HoffmanEncode hoffmanEncode;
              
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpen_Click( object sender, RoutedEventArgs e )
        {
            String path = TextBoxPath.Text;
            try
            {
                using (FileStream fs = File.Open( path, FileMode.Open) )
                {
                    byte[] data = new byte[102400];
                    

                    int len = fs.Read( data, 0, data.Length );
                 
                    hoffmanEncode = new HoffmanEncode( data, len );
                           
                    TextOpen.Text = "File size: " + len.ToString() + " bytes \n"; 
                    TextOpen.Text += hoffmanEncode.Data;

                    TextEncoded.Text = "Count of each letter:\n";
                    TextEncoded.Text += hoffmanEncode.getLetterCounterString();
                    TextEncoded.Text += "\nHuffmans code of each symbol:\n";
                    TextEncoded.Text += hoffmanEncode.getHuffmansCodeString();
                    TextEncoded.Text += "\nEncoded string: " + hoffmanEncode.getEncodedString();
                    //TextEncoded.Text += "\nEncoded size = " + hoffmanEncode.EncodedSize.ToString() + " bytes";


                    ButtonSave.IsEnabled = true;
                }

            }
            catch (FileNotFoundException)
            {
                TextBoxPath.Text = "File not found!";
            }
            catch (ArgumentException)
            {
                TextBoxPath.Text = "Path is empty!";
            }
            catch (DirectoryNotFoundException)
            {
                TextBoxPath.Text = "Directory not found!";
            }
            //using (FileStream fs = File.Open( @"D:\new.bin", FileMode.Open) )
            //{
            //        byte[] data = new byte[1024];

            //        int len = fs.Read( data, 0, data.Length );
                    
            //        hoffmanEncode.readBinary(data, len);
                                    
            //}
            
         }

       

        private void ButtonDecode_Click( object sender, RoutedEventArgs e )
        {
            int j = 0;
            String path = TextBoxPath.Text;
            try
            {
                using (FileStream fs = File.Open(path+".dic", FileMode.Open))
                {
                    byte[] data = new byte[102400];

                    int len = fs.Read(data, 0, data.Length);
                
                    char[] dataChar = new char[len];
                               

                    char ch; 
                    String val="";

                    Dictionary<char, int> dict = new Dictionary<char, int>();
                    for(int i = 0; i < len; i++)
                    {
                        ch = Convert.ToChar(data[i]);
                        i += 2;// =
                        for( j = i; j < len; i++, j++ )
                        {
                            if (data[j] != 32) val += Convert.ToChar(data[j]);
                            else
                            {
                                dict.Add(ch, Int32.Parse(val));
                                val = "";
                                break;
                            }
                        }
                    }
                                                            
                    hoffmanEncode = new HoffmanEncode(dict);
                }
            }
            catch (FileNotFoundException)
            {
                TextBoxPath.Text = "File not found!";
            }
            catch (ArgumentException)
            {
                TextBoxPath.Text = "Path is empty!";
            }
            catch (DirectoryNotFoundException)
            {
                TextBoxPath.Text = "Directory not found!";
            }
            //------------------------------------open binary---------------------------------

            try
            {
                using (FileStream fs = File.Open(path, FileMode.Open) )
                {
                    byte[] data = new byte[1024];

                    int len = fs.Read(data, 0, data.Length);

                    hoffmanEncode.decode(data, len);
                    TextOpen.Text = hoffmanEncode.Data;
                                      
                }

            }
            catch (FileNotFoundException)
            {
                TextBoxPath.Text = "File not found!";
            }
            catch (ArgumentException)
            {
                TextBoxPath.Text = "Path is empty!";
            }
            catch (DirectoryNotFoundException)
            {
                TextBoxPath.Text = "Directory not found!";
            }


            

        }//private void ButtonDecode_Click

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            String path = TextBoxSave.Text;
            hoffmanEncode.writeBinary(path);
            hoffmanEncode.writeDictionary(path+".dic");
            MessageBox.Show("Write success", "Huffman binary code");
        }
                
    }
}