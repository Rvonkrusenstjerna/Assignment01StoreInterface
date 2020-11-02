using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Assignment01StoreInterface
{

    class XmlHandler
    {
        //This class handles the stores inventory, most of the functions returns a list of products created-
        //by either reading from a local file, scraping websites or dynamically creating from user input.
            
        
            Random random = new Random();
            
            public List<Product> ScrapeMopvies()
            {

                //Scrapes a top list from IMBD and returns it as a list.
            
                List<Product> movies = new List<Product>();
                
                //Creates a HTML document object, first the webclient downloads the html from-
                //the url and stores it as a string, then we create a Htmldocument object using that string.
            
                String movieUrl = "https://www.imdb.com/list/ls024149810/";
                WebClient wc = new WebClient();
                String movieListHtml = wc.DownloadString(movieUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(movieListHtml);
                
                
                //Using the Htmldocument, we loop thorugh all nodes (tags in the html) mathching
                //the description we ask for, in this case every node containing movies and their information.
                foreach (var item in doc.DocumentNode.SelectNodes("//div[@class[contains(.,'lister-item-content')]]"))
                {

                    //Scrapes the movie's title from a node within the current node.
                    //if it doesn't find a string at the expected place (if it returns null, nothing)-
                    //the title value defaults to "unknown"
                    //(might wanna create a function that does this for all strings)
                    
                    string title = "";

                    if (item.SelectSingleNode(".//h3/a") != null)
                    {
                        title = item.SelectSingleNode(".//h3/a").InnerText;
                    }
                    else
                    {
                        title = "unknown";
                    }


                    //Scrapes the movies rating as a string and converts it to a double.
                    //imdb uses commas instead of dots,CultureInfo.InvariantCulture is used to correct for this.
                    string rateString  = item.SelectSingleNode(".//*[@class[contains(.,'ipl-rating-star__rating')]]").InnerText;
                    double rating = double.Parse(rateString, CultureInfo.InvariantCulture);

                    
                    //Fetches the link to the current movie's own profile page to get more in depth information
                    //about its Dirctor, release date etc.

                    string link = "https://www.imdb.com"+item.SelectSingleNode(".//h3/a").GetAttributeValue("href", "");
                    HtmlDocument subDoc = new HtmlDocument();
                    String movieHtml = wc.DownloadString(link);
                    subDoc.LoadHtml(movieHtml);

                    String release = subDoc.DocumentNode.SelectSingleNode(".//*[@title[contains(.,'See more release dates')]]").InnerText;
                    DateTime date = DateTime.Parse(release.Replace(" (Sweden)", ""));

                    string time = subDoc.DocumentNode.SelectSingleNode(".//*[@class[contains(.,'subtext')]]/time").GetAttributeValue("datetime", "");
                    TimeSpan ts = XmlConvert.ToTimeSpan(time);

                    String director = "unknown";

                    
                    //If node contains a <h4> tag with the text 'Director', store inner text of its inner <a> tag. 
                    if(subDoc.DocumentNode.SelectSingleNode(".//div[h4[text() = 'Director:']]/a")  != null)
                    {
                        director = subDoc.DocumentNode.SelectSingleNode(".//div[h4[text() = 'Director:']]/a").InnerText;
                    }
                    
                    //If not, look for <h4> containing Directors and store their names in String.
                    else if (subDoc.DocumentNode.SelectNodes(".//div[h4[text() = 'Directors:']]/a") !=  null)
                    {

                        foreach (var node in subDoc.DocumentNode.SelectNodes(".//div[h4[text() = 'Directors:']]/a"))
                        {
                            director = director + node.InnerText + ".";
                        }

                    }
                    

                    //Creates new movie object using scraped information and adds it to list.
                    movies.Add(new Movie(title, rating, date, ts, director));
                

                }

            //
            return movies;




        }

            
            public List<Product> ScrapeAlbum()
            {
                List<Product> album = new List<Product>();

                string title, art;
                double rating, price;
                    


                Track t;






                
            
            
                return album;

            }
            
            public List<Product> UserGenerateProductList()
            {
                //This Function returns a list of products created by the user;
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
        
            public List<Product> LoadXML(String url)
            {
                
                List<Product> products = new List<Product>();

                XElement XDoc = XElement.Load(url);


                foreach (XElement item in XDoc.Elements())
                {

                    string title = item.Element("title").Value;
                    

                    double rating = double.Parse(item.Element("rating").Value, CultureInfo.InvariantCulture);
                    double price = Convert.ToDouble(item.Element("price").Value);
                    string dirArt = "";
                    DateTime release = DateTime.ParseExact(item.Element("release-date").Value, "d-M-yyyy", CultureInfo.InvariantCulture);

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

                
            
            
            
                
            public List<Product> martinAlbum()
            {
                List<Product> album = new List<Product>();

                XElement aXml = XElement.Load("C:/Users/Renox/Source/Repos/Assignment01StoreInterface/Assignment01StoreInterface/AlbumData.xml");



                foreach (var item in aXml.Elements())
                {

                    String title = item.Attribute("Title").Value;
                    String artist = item.Attribute("Artist").Value;
                    DateTime release = DateTime.Parse(item.Attribute("ReleaseDate").Value);
                    Double rating = Double.Parse(item.Attribute("AverageUserRating").Value, CultureInfo.InvariantCulture);
                    Double price = Double.Parse(item.Attribute("Price").Value);

                    List<Track> tracks = new List<Track>();


                foreach (var track in item.Elements("Track"))
                {
                    string tTitle = track.Attribute("Title").Value;
                    TimeSpan tTs = TimeSpan.Parse("0:"+track.Attribute("Runtime").Value);
                    string tArt = track.Attribute("FeatArtist").Value;

                    tracks.Add(new Track(tTitle,tTs,tArt));

                }
                   

                Product newAlbum = new Album(title, rating, release, artist, tracks);
                album.Add(new Album(title, rating, release, artist, tracks));

            }




            return album;
                
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

