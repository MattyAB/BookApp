using BookLib;
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
using ModernChrome;

namespace BookApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        BookLib.BookLib lib;

        public MainWindow()
        {
            lib = new BookLib.BookLib();
            InitializeComponent();
        }

        private void AddBookNav_Click(object sender, RoutedEventArgs e)
        {
            BookAddWindow bookAddWindow = new BookAddWindow(lib);
            bookAddWindow.Show();
        }

        private void ScanJobNav_Click(object sender, RoutedEventArgs e)
        {
            ScanJobWindow scanJobWindow = new ScanJobWindow(lib, 0);
            try
            {
                scanJobWindow.Show();
            }
            catch(Exception ex)
            {
                if (ex.Message == "No more scans to do.")
                {

                }
                else
                    throw ex;
            }
        }
    }
}
