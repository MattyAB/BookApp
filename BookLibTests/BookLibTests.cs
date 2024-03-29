﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Tests
{
    [TestClass()]
    public class BookLibTests
    {
        BookLib lib;

        public BookLibTests()
        {
            lib = new BookLib();
        }

        [TestMethod()]
        public void ISBNConvertTest()
        {
            Assert.AreEqual("9781338099133", lib.ISBNConvert("1338099132"));
        }

        [TestMethod()]
        public void AddBookAlreadyOwnedTest()
        {
            try
            {
                lib.AddBook("9781444729016");
                Assert.Fail();
            }
            catch (Exception e)
            {
                if (e.Message != "This book is already owned.")
                    throw e;
            }
        }
    }
}