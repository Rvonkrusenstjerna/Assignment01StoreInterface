using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Assignment01StoreInterface
{
    class Menu_System
    {
        
        private XmlHandler xh = new XmlHandler();

        public void WelcomeScreen()
        {
            bool running = true;
            do
            {
                string choice;
                do
                {
                
                    Console.Clear();
                    Console.WriteLine("Welcome To Hans-Johnnys Media Store!");
                    Console.WriteLine("Please Select an option!\n");
                
                    Console.WriteLine($"1.Check Inventory");
                    Console.WriteLine("2.Contact Info");
                    Console.WriteLine("3.GTFO");
                
                    choice = Console.ReadLine();

                } while (choice != "1"  &&   choice !=  "2" && choice != "3");

                if (choice == "1")
                {
                    InventoryMenu();
                }
                else if (choice == "2")
                {
                    ContactMenu();
                }
                else if (choice == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Thank you come again.");
                    running = false;
                }
            } while (running);
        }


        private void ContactMenu()
        {
           
            Console.Clear();

            Console.WriteLine("Visit us at:");
            Console.WriteLine("Hans-Johnny's Media Store. Pretendstreet 66. Göteholm\n");
            Console.WriteLine("Billing Address:");
            Console.WriteLine("Kungsbäcksvägen 45. 80176. Gävle.");

            Console.WriteLine("Press The Any Key To Exit");
            Console.ReadKey();


        }

        private void InventoryMenu()
        {
            bool running = true;

            do
            {
                string choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Inventory\t");

                    Console.WriteLine("1. Load from XML");
                    Console.WriteLine("2. Scrape Movies Internet");
                    Console.WriteLine("3. Exit");

                    choice = Console.ReadLine();
                } while (choice != "1" && choice != "2" && choice != "3" && choice != "4");


                if (choice == "1")
                {
                    XmlMenu();
                }
               
                else if (choice == "2")
                {
                    ScrapeMenu();
                }
                else if (choice == "3")
                {
                    
                    running = false;
                }

            } while (running);




        }

        private void ScrapeMenu()
        {

            Console.Clear();
            Console.WriteLine("Loading Database...");

            List<Product> movies = xh.ScrapeMopvies();

            var sortedMovies = movies.OrderByDescending(x => x.Releasedate);
            Console.Clear();
            Console.WriteLine("Movies");
            foreach (var item in sortedMovies)
            {
                item.PrintInfo();
            }
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();

        }

        private void XmlMenu()
        {
            Console.Clear();
            Console.WriteLine("XML Inventory");

            List<Product> products = xh.LoadXML("XML/inventory.xml");

            List<Product> movies = new List<Product>();
            List<Product> album = new List<Product>();

            foreach (var item in products)
            {
                if (item is Album)
                {
                    album.Add(item);
                }
                else if (item is Movie)
                {
                    movies.Add(item);
                }
            }


            if (movies.Count > 0)
            {
                Console.WriteLine("Movies\n");
                
                var sortedMovies = movies.OrderByDescending(x => x.Releasedate);
                foreach (var movie in sortedMovies)
                {
                    movie.PrintInfo();
                }
                Console.WriteLine();
            }



            if (album.Count > 0)
            {
                Console.WriteLine("Album\n");
                var sortedAlbums = album.OrderByDescending(x => x.Rating);

                foreach (var albumItem in sortedAlbums)
                {
                    albumItem.PrintInfo();
                    Console.WriteLine();
                }
                
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();



        }

        





    }
}
