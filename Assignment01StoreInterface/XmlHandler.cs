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
                //the description we ask for, in this case every node containing movies and their information,
                //placed inside a div tag, with a class containing 'lister-item-content'.

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

                    
                    //Fetches the link to the current movie's own profile page to get more indepth information
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
                    if (subDoc.DocumentNode.SelectSingleNode(".//div[h4[text() = 'Director:']]/a")  != null)
                    {
                        director = subDoc.DocumentNode.SelectSingleNode(".//div[h4[text() = 'Director:']]/a").InnerText;
                    }
                    
                    //If not, look for <h4> containing 'Directors' and store their names in String.
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
                        XElement xmlTracks = item.Element("tracks");
                    
                        dirArt = item.Element("artist").Value;
                        List<Track> tracklist = new List<Track>();

                        foreach (var track in xmlTracks.Elements("track"))
                        {
                            string tTitle = track.Element("track-title").Value;
                            TimeSpan tSpan = TimeSpan.Parse(track.Element("track-runtime").Value);
                            string tFeat = track.Element("featuring").Value;
 

                            tracklist.Add(new Track(tTitle, tSpan, tFeat));
                        }


                        products.Add(new Album(title, rating, release,dirArt,tracklist));


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

                    Inventory.Save("../../../XML/inventory.xml");
                    



                }
                Console.WriteLine("inventory saved");



        }







        }
    }

