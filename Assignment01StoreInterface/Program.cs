using System;
using System.Collections.Generic;

namespace Assignment01StoreInterface
{
    class Program
    {
        static void Main(string[] args)
        {



            Product P = new Product("Smurfarna", 7, new DateTime(2019, 5, 12), 60, 99);

            P.PrintInfo();

            Product M = Movie("Smurfarna", 7, new DateTime(2019, 5, 12), 60, 99, "Charles Dickens");

            //Generator
            //25 Movies, 25 Albums,  2 Adresser,



        }

        

    }
}
