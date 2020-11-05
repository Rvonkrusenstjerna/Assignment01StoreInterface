using System;

namespace Assignment01StoreInterface
{
    class Product
    {
        public String Title { set; get; }
        public double Rating { set; get; }
        public DateTime Releasedate { set; get; }
        public TimeSpan Runtime { set; get; }
        public double Price { set; get; }

        public Product(string title, double rating, DateTime date)
        {

            this.Title = title;
            this.Rating = rating;
            this.Releasedate = date;
            this.Price = 0;

        }

        public Product(string title, double rating, DateTime date, double price)
        {

            this.Title = title;
            this.Rating = rating;
            this.Releasedate = date;
            this.Price = price;

        }




        public virtual void PrintInfo()
        {

        }


    }
}
