using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BookLib
{
    public class BookLib
    {
        SqlConnection Connection = new SqlConnection("Data Source=HORCRUX;Initial Catalog=Books;Integrated Security=True");

        public BookLib()
        {

        }

        public List<Book> GetScanJob(int depth)
        {

        }
    }
}
