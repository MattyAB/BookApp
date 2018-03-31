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
                lib.AddBook(ISBNBox.Text);
            }
        }
    }
}
