using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp
{
    class Book
    {
        public delegate float HtmlSweep(string html);

        private string v;
        DateTime now;
        int[] prices = new int[5];

        HtmlSweep[] sweepers = new HtmlSweep[5];
        string[] urls = new string[5];

        public Book(string v)
        {
            now = DateTime.Now;
            this.v = v;

            urls[0] = "https://www.webuybooks.co.uk/selling-basket/?isbn=" + v;

            GetPrices();
        }

        void GetPrices()
        {
            string html = GetURL(urls[0]);
            prices[0] = Convert.ToInt32(sweepers[0](html)*100); // From float of pounds to int of pence
        }
        
        string GetURL(string url)
        {
            var result = string.Empty;
            using (var webClient = new System.Net.WebClient())
                result = webClient.DownloadString(url);

            return result;
        }
    }
}
