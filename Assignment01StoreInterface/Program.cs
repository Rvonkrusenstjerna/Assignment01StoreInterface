using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Assignment01StoreInterface
{
    class Program
    {
        static void Main(string[] args)
        {



            Menu_System ms = new Menu_System();

            ms.welcomeScreen();

            
            
            //XmlHandler xh = new XmlHandler();
            //List<Product> movies = xh.LoadXML("movies.xml");
            //List<Product> album = xh.martinAlbum();

            //foreach (var item in movies)
            //{
            //    album.Add(item);
            //}

            //xh.SaveToXML(album);

            //Console.WriteLine(Directory.GetCurrentDirectory());

            //C:\Users\Robert\source\repos\Assignment01StoreInterface\Assignment01StoreInterface\Program.cs
            //C:\Users\Robert\source\repos\Assignment01StoreInterface\Assignment01StoreInterface\bin\Debug\netcoreapp3.1

            
            
            
            




            //List<Product> album = xh.martinAlbum();
            //var sortedAlbums = album.OrderBy(x => x.Rating);
            //var sortedAlbums = album.OrderByDescending(x => x.Rating);

            //Console.WriteLine("Album");
            //foreach (var x in sortedAlbums)
            //{
            //    x.PrintInfo();

            //}









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
