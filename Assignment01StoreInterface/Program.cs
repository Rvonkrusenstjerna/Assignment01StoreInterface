using System;
using System.Collections.Generic;

namespace Assignment01StoreInterface
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Product> Products = new List<Product>();

            Product P = new Product("Smurfarna", 7, new DateTime(2019, 5, 12), 60, 99);
            Product M = new Movie("Sagan Om Ringen", 7, new DateTime(2019, 5, 12), 60, 99, "Charles Dickens");

            Products.Add(P);
            Products.Add(M);

            foreach (var item in Products)
            {
                item.PrintInfo();
            }


            //Generator
            //25 Movies, 25 Albums,  2 Adresser,



        }

        
    }
}
