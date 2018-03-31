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

        private void ISBNBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    lib.AddBook(ISBNBox.Text);
                }
                catch(Exception ex)
                {
                    if (ex.Message == "The ISBN was not a known ISBN length. Needs to be 10 or 13 characters.")
                    {
                        AddBookStatusBox.Text = ex.Message;
                    }
                    else if (ex.Message == "Specified ISBN is not valid.")
                    {
                        AddBookStatusBox.Text = ex.Message;
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
