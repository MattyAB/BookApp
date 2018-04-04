using BookLib;
using ModernChrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for EvaluateWindow.xaml
    /// </summary>
    public partial class EvaluateWindow : ModernWindow
    {
        BookLib.BookLib lib;

        int[] possiblePrices;
        int[] optimalPrices;

        List<Book> books;

        public EvaluateWindow(BookLib.BookLib lib)
        {
            possiblePrices = new int[5];
            optimalPrices = new int[5];
            this.lib = lib;
            InitializeComponent();
            books = lib.Evaluate();
            DisplayEvaluation();
        }

        public void DisplayEvaluation()
        {
            // Optimal
            foreach(Book b in books)
            {
                b.SetBestSite();
                optimalPrices[b.bestSite] += b.prices[b.bestSite];
            }

            // Possible
            foreach (Book b in books)
            {
                for (int i = 0; i < 5; i++)
                    possiblePrices[i] += b.prices[i];
            }

            WO.Content = optimalPrices[0];
            ZiO.Content = optimalPrices[1];
            MuO.Content = optimalPrices[2];
            MoO.Content = optimalPrices[3];
            ZaO.Content = optimalPrices[4];

            WP.Content = possiblePrices[0];
            ZiP.Content = possiblePrices[1];
            MuP.Content = possiblePrices[2];
            MoP.Content = possiblePrices[3];
            ZaP.Content = possiblePrices[4];
        }

        private void EvaluateClick(object sender, RoutedEventArgs e)
        {
            string ButtonName = ((Button)sender).Name;

            bool Possible;

            if (ButtonName.Substring(ButtonName.Length - 1) == "P")
                Possible = true;
            else
                Possible = false;

            ButtonName = ButtonName.Remove(ButtonName.Length - 1);

            int site = 0;

            switch(ButtonName)
            {
                case "W":
                    site = 0;
                    break;
                case "Zi":
                    site = 1;
                    break;
                case "Mu":
                    site = 2;
                    break;
                case "Mo":
                    site = 3;
                    break;
                case "Za":
                    site = 4;
                    break;
            }

            if(Possible)
            {
                foreach (Book book in books)
                {
                    if (book.prices[site] > 0)
                    {
                        Debug.Write(book.ISBN + ": " + book.prices[site] + "\n");
                    }
                }
            }
            else
            {
                foreach (Book book in books)
                {
                    if (book.bestSite == site && book.prices[site] > 0)
                    {
                        Debug.Write(book.ISBN + ": " + book.prices[site] + "\n");
                    }
                }
            }
        }
    }
}
