using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Track
    {
        public String Title { set; get; }
        public TimeSpan RunTime { set; get; } 
        public  String Featuring { set; get; }

        public Track(String title, TimeSpan runTime)
        {
            this.Title = title;
            this.RunTime = runTime;
            this.Featuring = "Various Artists";
            
        }

        public Track(String title, TimeSpan runTime, String featuring)
        {
            this.Title = title;
            this.RunTime = runTime;
            this.Featuring = featuring;
        
        }




    }
}
