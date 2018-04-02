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
            DisplayEvaluation(lib.Evaluate());
            InitializeComponent();
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
        }
    }
}
