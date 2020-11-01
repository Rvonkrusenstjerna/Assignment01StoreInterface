using System;
using System.Collections.Generic;
using System.Globalization;

namespace Assignment01StoreInterface
{
    class Program
    {
        static void Main(string[] args)
        {




            XmlHandler xh = new XmlHandler();
            //List<Product> inventory = xh.UserGenerateProductList();
            //xh.SaveToXML(inventory);
            //xh.LoadXML();

            //List<Product> prod = xh.LoadXML();

            //foreach (var item in prod)
            //{
            //    item.PrintInfo();
            //}

            xh.SaveToXML(xh.ScrapeMopvies());








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
