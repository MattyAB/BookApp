using BookLib;
using ModernChrome;
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
    /// Interaction logic for EvaluateWindow.xaml
    /// </summary>
    public partial class EvaluateWindow : ModernWindow
    {
        BookLib.BookLib lib;

        int[] possiblePrices;
        int[] optimalPrices;

        public EvaluateWindow(BookLib.BookLib lib)
        {
            possiblePrices = new int[5];
            optimalPrices = new int[5];
            this.lib = lib;
            InitializeComponent();
            DisplayEvaluation(lib.Evaluate());
        }

        public void DisplayEvaluation(List<Book> books)
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
    }
}
