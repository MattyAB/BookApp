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
        SqlConnection Connection = new SqlConnection("Data Source=HORCRUX;Initial Catalog=Books;Integrated Security=True");

        public BookLib()
        {
            Connection.Open();
        }

        public void AddBook(string ISBN)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Books (ISBN, RecievedDate) VALUES ('" + ISBN + "', @date)";
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            cmd.ExecuteNonQuery();
        }

        public void GetScanJob(int depth)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Books WHERE SellDate = null";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                Debug.Write(reader.GetString(1));
            }
        }
    }
}
