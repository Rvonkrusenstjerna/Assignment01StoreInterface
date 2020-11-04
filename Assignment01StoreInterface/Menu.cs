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

        public void welcomeScreen()
        {
            bool running = true;
            do
            {
                string choice = "";
                do
                {
                
                    Console.Clear();
                    Console.WriteLine("Welcome To Hans-Johnnys Media Store!");
                    Console.WriteLine("Please Select an option!");
                
                    Console.WriteLine($"1.Check Inventory");
                    Console.WriteLine("2.Contact Info");
                    Console.WriteLine("3.GTFO");
                
                    choice = Console.ReadLine();
                } while (choice != "1"  &&   choice !=  "2" && choice != "3");

                if (choice == "1")
                {
                    inventoryMenu();
                }
                else if (choice == "2")
                {
                    contactMenu();
                }
                else if (choice == "3")
                {
                    running = false;
                }
            } while (running);
        }


        private void contactMenu()
        {
           
            Console.Clear();

            Console.WriteLine("Visit us at:");
            Console.WriteLine("Hans-Johnny's Media Store.\t Pretendstreet 66.\tGöteholm\t\n");
            Console.WriteLine("Billing Address:");
            Console.WriteLine("Kungsbäcksvägen 45.\t 801 76.\tGävle\t");

            Console.WriteLine("Press The Any Key To Exit");
            Console.ReadKey();


        }

        private void inventoryMenu()
        {
            bool running = true;

            do
            {
                string choice = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("Inventory");

                    Console.WriteLine("1. Load from XML");
                    Console.WriteLine("2. Make your own inventory");
                    Console.WriteLine("3. Scrape Movies Internet");
                    Console.WriteLine("4. Exit");

                    choice = Console.ReadLine();
                } while (choice != "1" && choice != "2" && choice != "3" && choice != "4");


                if (choice == "1")
                {
                    xmlMenu();
                }
                else if (choice == "2")
                {
                    //contactMenu();
                }
                else if (choice == "3")
                {
                    scrapeMenu();
                }
                else if (choice == "4")
                {
                    running = false;
                }

            } while (running);




        }

        private void scrapeMenu()
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

            Console.ReadKey();

        }

        private void xmlMenu()
        {
            Console.Clear();
            Console.WriteLine("XML Inventory");

            List<Product> products = xh.LoadXML("XML/inventory.xml");

            foreach (var item in products)
            {
                item.PrintInfo();
            }






            Console.ReadKey();

        }





    }
}
