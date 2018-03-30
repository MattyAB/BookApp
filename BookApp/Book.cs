using System;
using HtmlAgilityPack;

namespace BookApp
{
    class Book
    {
        public delegate double HtmlSweep(string html);

        private string v;
        DateTime now;
        int[] prices = new int[5];

        HtmlSweep[] sweepers = new HtmlSweep[5];
        string[] urls = new string[5];

        public Book(string v)
        {
            sweepers[0] = WeBuyBookSweeper;

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
            {
                webClient.Headers.Add("Cookie", "visited_before=1; cb-enabled=enabled; __zlcmid=lbhDZhUYZZismC; fbm_557474571013295=base_domain=.webuybooks.co.uk; wbbpreviousvisitor=1; visited_before=1; PHPSESSID=takdpip6u30cmv8hj4sgogcun2; fbsr_557474571013295=iRgiZRj93HmFfceDWBZe1ehD6sCiJuBPXndAJPGLPVs.eyJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImNvZGUiOiJBUUFNMVBtaldVeUx5dGlPUTE3N2hjOW5ycUhLUVN5Y2c2UlZKYlUwTW9LcE1QZDZVaVU0eVhrTUxZQWt4b3NtTWlaTF91MTlBd1JDZDNGZWdWSTFYWUp3bUpiT1g5dVF1TkRETUxzalFHQVk2TkpYbDRjc3pMZ3d4OFZnSGtQa2lNV2ZGbnpXc3A3UlQ3YUNKQU03VnZTWGpoaXNYc1pNM1hlM0tnd3BYMnFjSUZxOXl0eTZ4MDFmZXZvX0FWdDhtc2llakxzNzRfZTY5TDQwZ091bDZ0bDBFbE5PX3llYUxHNVphcTZKUllYc3VEWDdGMkU4VVBYeWhMVUhyT25RYW9QUGxteG5VODZqQmF5eHU1eDk1ZGVncTlpc0FSQjQ5OUE0ZnFOenpoVm13YWprd2FNRlRSUE9mblgyTEVyd1NCVjFfaWczZ1Y2ZUdPWnAtVTgtcVRyNyIsImlzc3VlZF9hdCI6MTUyMjAwOTI5NiwidXNlcl9pZCI6IjE5MjIyNDUzOTQ3MDY1ODgifQ");
                result = webClient.DownloadString(url);
            }

            return result;
        }

        double WeBuyBookSweeper(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            
            string price = htmlDocument.DocumentNode
                .SelectSingleNode("//*[@id=\"sellingb\"]/tbody/tr[1]/td[4]")
                .InnerText;

            return Convert.ToDouble(price);
        }
    }
}
