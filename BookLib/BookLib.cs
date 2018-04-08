using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookLib
{
    public class BookLib
    {
        SqlConnection Connection = new SqlConnection("Data Source=HORCRUX;Initial Catalog=Books;Integrated Security=True;MultipleActiveResultSets=True");

        public BookLib()
        {
            Connection.Open();
        }

        public void AddBook(string RawISBN)
        {
            // Checks

            string ISBN = "";

            foreach (char c in RawISBN)
            {
                if (Char.IsNumber(c))
                {
                    ISBN += c;
                }
            }

            if (ISBN.Length != 13 && ISBN.Length != 10)
            {
                throw new Exception("The ISBN was not a known ISBN length. Needs to be 10 or 13 characters.");
            }

            if (ISBN.Length == 10)
            {
                ISBN = ISBNConvert(ISBN);
            }

            if(GetBook(ISBN).Count != 0)
                throw new Exception("This book is already owned.");

            // SQL Command

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Books (ISBN, RecievedDate) VALUES ('" + ISBN + "', @date)";
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            cmd.ExecuteNonQuery();
        }

        public void RemoveBook(string RawISBN)
        {
            // Checks

            string ISBN = "";

            foreach (char c in RawISBN)
            {
                if (Char.IsNumber(c))
                {
                    ISBN += c;
                }
            }

            if (ISBN.Length != 13 && ISBN.Length != 10)
            {
                throw new Exception("The ISBN was not a known ISBN length. Needs to be 10 or 13 characters.");
            }

            if (ISBN.Length == 10)
            {
                ISBN = ISBNConvert(ISBN);
            }

            if (GetBook(ISBN).Count == 0)
                throw new Exception("This book is not owned.");

            // SQL Command

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE Books SET SellDate = @date WHERE ISBN='" + ISBN + "' AND RecievedDate IS NOT NULL";
            cmd.Parameters.AddWithValue("@date", DateTime.Parse("02/04/2018"));
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            cmd.ExecuteNonQuery();
        }

        public List<Book> GetBook(string ISBN)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Books WHERE SellDate IS NULL";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            List<Book> books = new List<Book>();

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Book book = new Book(ISBN);
                    if (Convert.ToString(reader.GetValue(1)) == ISBN)
                        books.Add(book);
                }
            }

            return books;
        }

        // Uses code from https://www.codeproject.com/Tips/75999/Convert-ISBN10-To-ISBN-13.aspx
        public string ISBNConvert(string ISBN10)
        {
            string isbn10 = "978" + ISBN10.Substring(0, 9);
            int isbn10_1 = Convert.ToInt32(isbn10.Substring(0, 1));
            int isbn10_2 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(1, 1)) * 3);
            int isbn10_3 = Convert.ToInt32(isbn10.Substring(2, 1));
            int isbn10_4 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(3, 1)) * 3);
            int isbn10_5 = Convert.ToInt32(isbn10.Substring(4, 1));
            int isbn10_6 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(5, 1)) * 3);
            int isbn10_7 = Convert.ToInt32(isbn10.Substring(6, 1));
            int isbn10_8 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(7, 1)) * 3);
            int isbn10_9 = Convert.ToInt32(isbn10.Substring(8, 1));
            int isbn10_10 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(9, 1)) * 3);
            int isbn10_11 = Convert.ToInt32(isbn10.Substring(10, 1));
            int isbn10_12 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(11, 1)) * 3);
            int k = (isbn10_1 + isbn10_2 + isbn10_3 + isbn10_4 + isbn10_5 + isbn10_6 + isbn10_7 + isbn10_8 + isbn10_9 + isbn10_10 + isbn10_11 + isbn10_12);
            int checkdigit = 10 - ((isbn10_1 + isbn10_2 + isbn10_3 + isbn10_4 + isbn10_5 + isbn10_6 + isbn10_7 + isbn10_8 + isbn10_9 + isbn10_10 + isbn10_11 + isbn10_12) % 10);
            if (checkdigit == 10)
                checkdigit = 0;
            return isbn10 + checkdigit.ToString();
        }

        public List<ScanJob> GetScanJob(int depth)
        {
            List<ScanJob> jobs = new List<ScanJob>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Books WHERE SellDate IS NULL";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            reader = cmd.ExecuteReader();

            // For every book it gets
            while(reader.Read())
            {

                for (int i = 0; i < 5; i++)
                {
                    // New SQL command to get scans of book
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader reader1;

                    cmd1.CommandText = "SELECT * FROM BookPrice WHERE ID = " + reader.GetInt32(0) + " AND Site = " + i;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = Connection;

                    reader1 = cmd1.ExecuteReader();

                    List<ScanJob> pastScans = new List<ScanJob>();

                    // Add each past scan to the list of scans
                    while (reader1.Read())
                    {
                        ScanJob job = new ScanJob();
                        job.site = i;
                        job.ISBN = reader.GetString(1);
                        job.date = reader1.GetDateTime(3);
                        pastScans.Add(job);
                    }

                    List<ScanJob> orderedScans = pastScans.OrderBy(o => o.date).ToList();

                    if(DateTime.Now.Subtract(orderedScans[orderedScans.Count - 1].date).TotalDays > depth)
                    {
                        ScanJob job = new ScanJob();
                        job.ISBN = reader.GetString(1);
                        job.ID = reader.GetInt32(0);
                        job.site = i;
                        jobs.Add(job);
                    }
                }
            }

            List<ScanJob> orderedJobs = jobs.OrderBy(o => o.site).ToList();

            return orderedJobs;
        }

        public void SubmitJob(ScanJob job)
        {
            // SQL Command

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO BookPrice VALUES (@ID, @site, @price, @date)";
            cmd.Parameters.AddWithValue("@ID", job.ID);
            cmd.Parameters.AddWithValue("@site", job.site);
            cmd.Parameters.AddWithValue("@price", job.price);
            cmd.Parameters.AddWithValue("@date", job.date);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            cmd.ExecuteNonQuery();
        }
        
        public List<Book> Evaluate()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Books WHERE SellDate IS NULL";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            reader = cmd.ExecuteReader();

            List<Book> books = new List<Book>();

            // For every book it gets
            while (reader.Read())
            {
                Book book = new Book(reader.GetString(1));

                for (int i = 0; i < 5; i++)
                {
                    // New SQL command to get scans of book
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader reader1;

                    cmd1.CommandText = "SELECT * FROM BookPrice WHERE ID = " + reader.GetInt32(0) + " AND Site = " + i;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = Connection;

                    reader1 = cmd1.ExecuteReader();

                    List<ScanJob> pastScans = new List<ScanJob>();

                    // Add each past scan to the list of scans
                    while (reader1.Read())
                    {
                        ScanJob job = new ScanJob();
                        job.site = i;
                        job.ISBN = reader.GetString(1);
                        job.date = reader1.GetDateTime(3);
                        job.price = reader1.GetInt32(2);
                        pastScans.Add(job);
                    }

                    if (pastScans.Count != 0)
                    {
                        // NEEDS MORE CODE
                        List<ScanJob> orderedScans = pastScans.OrderBy(o => o.date).ToList();

                        book.prices[i] = orderedScans[orderedScans.Count - 1].price;
                    }
                }

                books.Add(book);
            }

            return books;
        }
    }
}
