﻿using Newtonsoft.Json.Linq;
using System.Net;
using System;

namespace BookLib
{
    public class Book
    {
        public string ISBN;
        public string title;
        public string subtitle;
        public string author;
        public string publisher;
        public DateTime publishDate;

        public Book(string ISBN)
        {
            this.ISBN = ISBN;
        }

        public void GetBookDetails()
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=isbn:" + ISBN;
            string s;

            using (WebClient client = new WebClient())
            {
                s = client.DownloadString(url);
            }

            dynamic d = JObject.Parse(s);

            if(d.totalItems == 0)
            {
                throw new Exception("Specified ISBN is not valid.");
            }

            title = d.items[0].volumeInfo.title;
            subtitle = d.items[0].volumeInfo.subtitle;
            author = d.items[0].volumeInfo.authors[0];
            publisher = d.items[0].volumeInfo.publisher;
            publishDate = DateTime.Parse(d.items[0].volumeInfo.publishedDate.ToString());
        }
    }
}