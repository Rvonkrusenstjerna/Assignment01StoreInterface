using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Movie : Product
    {

        public string Director { set; get; }




        public Movie(string title, double rating, DateTime date, TimeSpan runTime, string director) : base(title, rating, date)
        {
            this.Director = director;
            this.Runtime = runTime;

        }


        public Movie(string title, double rating, DateTime date, TimeSpan runTime, double price, string director) : base(title, rating, date, price)
        {
            this.Director = director;
            this.Runtime = runTime;

        }

        public override void PrintInfo()
        {
            Console.WriteLine($"{Title} Rating: {Rating} DateTime: {Releasedate.ToString("yyyy-MM-dd")} Runtime: {Runtime} Director: {Director} Price: {Price}kr");
        }

    }
}
