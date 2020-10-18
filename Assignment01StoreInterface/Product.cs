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

        private int[] averageRating()
        {

            Random random = new Random();
            int[] ratings = new int[10];


            for (int x = 0; x <= ratings.Length; x++)
            {
                ratings[x] = random.nex
            }



            return ratings;
        }



    }
}
