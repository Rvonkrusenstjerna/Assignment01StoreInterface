using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Assignment01StoreInterface
{




    class XmlHandler
    {

            Random random = new Random();
            
            public List<Product> ScrapeMopvies()
            {

                List<Product> movies = new List<Product>();
            
            
                String movieUrl = "https://www.imdb.com/list/ls024149810/";
                WebClient wc = new WebClient();
                String movieListHtml = wc.DownloadString(movieUrl);
                HtmlDocument doc = new HtmlDocument();
                //doc.OptionEmptyCollection = true;

                doc.LoadHtml(movieListHtml);
                int x = 1;


            //foreach (var item in doc.DocumentNode.SelectNodes("//div[@class[contains(.,'lister-item mode-detail')]]"))
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class[contains(.,'lister-item-content')]]"))
            {


                //(title, rating, release, runTime, price, dirArt)
                //link

                string title = "";

                if (item.SelectSingleNode(".//h3/a") != null)
                {
                    title = item.SelectSingleNode(".//h3/a").InnerText;
                }
                else
                {
                    title = "Rostbiff";
                }
                
                
                string rateString  = item.SelectSingleNode(".//*[@class[contains(.,'ipl-rating-star__rating')]]").InnerText;
                double rating = double.Parse(rateString, CultureInfo.InvariantCulture);

                //Take release and runtime from link
                string link = "https://www.imdb.com"+item.SelectSingleNode(".//h3/a").GetAttributeValue("href", "");
                HtmlDocument subDoc = new HtmlDocument();
                String movieHtml = wc.DownloadString(link);
                subDoc.LoadHtml(movieHtml);

                String release = subDoc.DocumentNode.SelectSingleNode(".//*[@title[contains(.,'See more release dates')]]").InnerText;
                DateTime date = DateTime.Parse(release.Replace(" (Sweden)", ""));

                string time = subDoc.DocumentNode.SelectSingleNode(".//*[@class[contains(.,'subtext')]]/time").GetAttributeValue("datetime", "");
                TimeSpan ts = XmlConvert.ToTimeSpan(time);

                String director = "";

                if(subDoc.DocumentNode.SelectSingleNode(".//div[h4[text() = 'Director:']]/a")  != null)
                {
                    director = subDoc.DocumentNode.SelectSingleNode(".//div[h4[text() = 'Director:']]/a").InnerText;
                }
                
                else if (subDoc.DocumentNode.SelectNodes(".//div[h4[text() = 'Directors:']]/a") !=  null)
                {

                    foreach (var node in subDoc.DocumentNode.SelectNodes(".//div[h4[text() = 'Directors:']]/a"))
                    {
                        director = director + node.InnerText + ".";
                    }

                }

                else
                {
                    director = "unknown";
                }

                
                movies.Add(new Movie(title, rating, date, ts, director));
                Console.WriteLine($"{title} {release} {rating} | {ts} ");

                






            }








            //
            return movies;




        }


            public List<Product> UserGenerateProductList()
            {

                List<Product> products = new List<Product>();
                
                do
                {
                    
                    string choice = question("Is this a movie or album? m/a");
                    string title = question("Enter Product Name");
                    string dirArt = question("Enter Director or Artists name");
                    //int rating = Convert.ToInt32(question("Enter Rating (0-10)"));
                    int rating = random.Next(0,10);
                    DateTime release = DateTime.ParseExact(question("Enter Release Date (dd-mm-yyyy)"), "d-M-yyyy", CultureInfo.InvariantCulture);

                    //Console.WriteLine("Enter Price (kr)");
                    //double price = Convert.ToDouble(Enter Rating (0-10));
                    double price = random.Next(10,300);
                    
                    if (choice == "m")
                    {
                        //make Movie

                        Console.WriteLine("Enter Runtime (h:m:s)");
                        TimeSpan runTime = TimeSpan.Parse(Console.ReadLine());

                        products.Add(new Movie(title, rating, release, runTime, price, dirArt));
                        Console.WriteLine("---");
                        Console.WriteLine(runTime);
                    }

                    else
                    {
                        //make Album
                        List<Track> tracks = new List<Track>();

                        do
                        {
                            Console.WriteLine("Track Title");
                            String trackTitle = Console.ReadLine();

                            Console.WriteLine("Track Length (h:m:s)");
                            TimeSpan trackTime = TimeSpan.Parse(Console.ReadLine());

                        //Console.WriteLine("Featuring");
                        //String features = Console.ReadLine();
                        String features = "Various Artists";

                            tracks.Add(new Track(trackTitle, trackTime, features));


                            Console.WriteLine("Add other track? (y/n)");

                        } while (Console.ReadLine() != "");


                        products.Add(new Album(title, rating, release, price, dirArt, tracks));

                    }

                    Console.WriteLine("Add more Products?");

                } while (Console.ReadLine() != "");


                return products;

            }


            private String question(String quest)
            {
                Console.WriteLine(quest);
                String Answer = Console.ReadLine();
                Console.Clear();
                return Answer;
                
            }
        
            public List<Product> LoadXML()
            {
                
                List<Product> products = new List<Product>();

                XElement XDoc = XElement.Load("D:/inventory.xml");


                foreach (XElement item in XDoc.Elements())
                {

                    string title = item.Element("title").Value;
                    string dirArt = "";

                    double rating = double.Parse(item.Element("rating").Value, CultureInfo.InvariantCulture);
                    double price = Convert.ToDouble(item.Element("price").Value);

                    DateTime release = DateTime.Parse(item.Element("release-date>").Value);
                    TimeSpan runtime = TimeSpan.Parse(item.Element("runtime").Value);

                

                    //This is a movie
                    if (item.FirstAttribute.Value == "movie")
                    {
                        dirArt = item.Element("director").Value;




                        products.Add(new Movie(title, rating, release,runtime,dirArt));

                    }


                    //this is an album
                    else if (item.FirstAttribute.Value == "album")
                    {

                        Console.WriteLine("This is an album");

                    }

                }

                return products;
            }

                
            
            
            
                

            
            
        
            public void SaveToXML(List<Product> products)
            {
                Console.WriteLine("Make XML ACTIVATED");
                XElement Inventory = new XElement("Inventory");

                foreach (Product item in products)
                {

                    XElement product = new XElement("Product");
                    product.Add(new XElement("title", item.Title),
                                new XElement("rating", item.Rating),
                                new XElement("release-date", item.Releasedate.ToString("dd-M-yyyy")),
                                new XElement("price", item.Price));



                    if (item is Album album)
                    {
                        product.SetAttributeValue("id", "album");
                        product.Add(new XElement("run-time", album.Runtime.ToString()));
                        product.Add(new XElement("artist", album.Artist));
                        XElement tracks = new XElement("tracks");
                        product.Add(tracks);

                        foreach (Track track in album.Tracks)
                        {

                            XElement tr = new XElement("track");

                            tr.Add(new XElement("track-title", track.Title),
                                   new XElement("track-runtime", track.Runtime.ToString()),
                                   new XElement("featuring", track.Featuring));

                            tracks.Add(tr);
                        }


                    }


                    else if (item is Movie movie)
                    {
                        product.SetAttributeValue("id", "movie");
                        product.Add(new XElement("director", movie.Director));
                        product.Add(new XElement("runtime", movie.Runtime.ToString()));
                    }

                    Inventory.Add(product);

                    Inventory.Save("D:/inventory.xml");
                    



                }
                Console.WriteLine("inventory saved");



        }







        }
    }

