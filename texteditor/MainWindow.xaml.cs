using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace texteditor
{

    public partial class MainWindow : Window
    {
        string selectalldata;
        string cutd=""; 
        string copyd=""; 
        bool alld = false;
        bool copydata = false;
        bool cutdata = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(textbox.Document.ContentStart, textbox.Document.ContentEnd);
            string text = textRange.Text;
            List<string> datas = new List<string> { text };
            var data = JsonSerializer.Serialize(datas);
            if (cehckbox.IsChecked == true)
            {
                File.WriteAllText($"../../../{filebox.Text}.json", data);
            }
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string dos = openFileDialog.FileName;

                string dd = File.ReadAllText(dos);
                TextRange textRange = new TextRange(textbox.Document.ContentStart, textbox.Document.ContentEnd);
                textRange.Text = "";
                textRange.Text = dd;

                try
                {
                    var data = JsonSerializer.Deserialize<object>(dd);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("JSON deserialize error : " + ex.Message);
                }

            }
        }
        
   

        private void cut_Click(object sender, RoutedEventArgs e)
        {
            var data =new  TextRange(textbox.Selection.Start, textbox.Selection.End);
            cutdata = true;
            cutd = data.Text;
            data.Text = "";
        }

        private void paste_Click(object sender, RoutedEventArgs e)
        {
            if (alld)
            {
                alld = false;
                TextRange textRange = new TextRange(textbox.Selection.Start, textbox.Selection.End);
                textRange.Text = selectalldata;
            }
            else if(copydata)
            {
                TextRange textRange = new TextRange(textbox.Selection.Start, textbox.Selection.End);
                textRange.Text = copyd;

                copydata = false;
            }
            else if (cutdata)
            {
                cutdata=false;
                TextRange textRange = new TextRange(textbox.Selection.Start, textbox.Selection.End);
                textRange.Text = cutd;


            }
        }

        private void selectall_Click(object sender, RoutedEventArgs e)
        {
            alld = true;
       selectalldata = new TextRange(textbox.Document.ContentStart, textbox.Document.ContentEnd).Text;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(textbox.Document.ContentStart, textbox.Document.ContentEnd);
            string text = textRange.Text;

            List<string> datas = new List<string> { text}; 
            var data = JsonSerializer.Serialize(datas);
       
                File.WriteAllText($"../../../{filebox.Text}.json", data);
            
        }

        private void cehckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void copy_Click(object sender, RoutedEventArgs e)
        {
            copydata = true;
        }
    }
}