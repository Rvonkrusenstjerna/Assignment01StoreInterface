using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Product
    {
        String Title;
        double Rating;
        DateTime Releasedate;
        int Runtime { set; get; }
        double Price;


        public Product(string title, double rating, DateTime date, int runtime, double price)
        {

            this.Title = title;
            this.Rating = rating;
            this.Releasedate = date;
            this.Runtime = runtime;
            this.Price = price;

        }

        
        public void PrintInfo()
        {
            Console.WriteLine($"Title: {Title} Rating: {Rating} DateTime: {Releasedate.ToString("yyyy-MM-dd")} Runtime: {Runtime} Price: {Price}");
        }


    }
}
