using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Assignment01StoreInterface
{
    class Program
    {
        static void Main(string[] args)
        {


            //Added Changes

            XmlHandler xh = new XmlHandler();
            //List<Product> inventory = xh.UserGenerateProductList();
            //xh.SaveToXML(inventory);
            //xh.LoadXML();


            //C:\Users\Renox\Source\Repos\Assignment01StoreInterface\Assignment01StoreInterface\movies.xml
            List<Product> prod = xh.LoadXML("C:/Users/Renox/Source/Repos/Assignment01StoreInterface/Assignment01StoreInterface/movies.xml");

            var sortedProducts = prod.OrderBy(x => x.Releasedate);

            
            //Linq, Icomparer

            foreach (var item in sortedProducts)
            {
                item.PrintInfo();
            }

            //xh.SaveToXML(xh.ScrapeMopvies());








            //List<Track> tracks = new List<Track>();
            //tracks.Add(new Track("A", new TimeSpan(0, 1, 0)));
            //tracks.Add(new Track("B", new TimeSpan(0, 1, 0)));
            //tracks.Add(new Track("C", new TimeSpan(0, 1, 0)));

            //DateTime release = DateTime.ParseExact("1/12/2008", "d/M/yyyy", CultureInfo.InvariantCulture);
            //Album Ass = new Album("name", 7, release, 99, "singers", tracks);
            //Console.WriteLine(Ass.RunTime);


        }


    }
}
