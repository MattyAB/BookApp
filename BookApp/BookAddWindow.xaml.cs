using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace BookApp
{
    /// <summary>
    /// Interaction logic for BookAddWindow.xaml
    /// </summary>
    public partial class BookAddWindow : Window
    {
        BookLib.BookLib lib;

        public BookAddWindow(BookLib.BookLib lib)
        {
            this.lib = lib;
            InitializeComponent();
        }

        private void SubmitBox_Click(object sender, RoutedEventArgs e)
        {
            AddBook();
        }

        private void ISBNBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddBook();
            }
        }

        private void AddBook()
        {
            try
            {
                lib.AddBook(ISBNBox.Text);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The ISBN was not a known ISBN length. Needs to be 10 or 13 characters.")
                {
                    AddBookStatusBox.Text = ex.Message;
                }
                else if (ex.Message == "Specified ISBN is not valid.")
                {
                    AddBookStatusBox.Text = ex.Message;
                }
                else if (ex.Message == "This book is already owned.")
                {
                    AddBookStatusBox.Text = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }
        }

        private void FileBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FilePathBox.Text = filename;
            }
        }

        private void BulkSubmit_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = File.ReadAllLines(FilePathBox.Text);
            foreach (string line in lines)
            {
                lib.AddBook(line);
            }
        }
    }
}
