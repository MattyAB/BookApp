using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void GetBookDetailsTest()
        {
            Book book = new Book("9781338099133");
            book.GetBookDetails();

            Assert.AreEqual("Harry Potter and the Cursed Child - Parts One and Two", book.title);
            Assert.AreEqual("The Official Script Book of the Original West End Production Special Rehearsal Edition", book.subtitle);
            Assert.AreEqual("J. K. Rowling", book.author);
            Assert.AreEqual("Arthur A. Levine Books", book.publisher);
            Assert.AreEqual(DateTime.Parse("2016-07-31"), book.publishDate);
        }
    }
}