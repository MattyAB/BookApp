using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLib;

namespace BookConsole
{
    class Program
    {
        static BookLib.BookLib lib;

        static void Main(string[] args)
        {
            lib = new BookLib.BookLib();

            while (true)
            {
                string command = Console.ReadLine();
                if(command == "importcsv")
                {
                    string[] lines = File.ReadAllLines(@"C:\Users\matth\OneDrive\Documents\BookBusiness\BookApp\import.csv");
                    foreach(string line in lines)
                    {
                        lib.AddBook(line);
                    }
                }
            }
        }
    }
}
