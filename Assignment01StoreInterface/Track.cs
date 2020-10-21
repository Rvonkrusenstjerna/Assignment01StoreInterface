using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Track
    {
        String Title;
        public int RunTime; //sekunder
        List<String> featuring; 

        public Track(String title, int runTime)
        {
            this.Title = title;
            this.RunTime = runTime;

            
        }




    }
}
