using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Album : Product
    {

        String Artist;
        public List<Track> Tracks; 



        public Album(string title, double betyg, DateTime date, int speltid, double pris, string Artist): base(title, betyg, date, speltid, pris)
        {
            this.Artist = Artist;
            this.Tracks = new List<Track>();
            
        }
        

        public int antalLåtar()
        {
            return Tracks.Count;
        }

        public int trackRuntime()
        {

            int spelTid = 0;
            foreach (var item in Tracks)
            {
                spelTid += item.spelTid;
            }

            return spelTid;

        }
        


    }
}
