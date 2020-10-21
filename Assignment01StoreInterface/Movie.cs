using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Movie : Product
    {

        string Director;

        public Movie(string title, double betyg, DateTime date, int speltid, double pris, string director) : base(title, betyg, date, speltid, pris)
        {
            this.Director = director; 
        }



    }
}
