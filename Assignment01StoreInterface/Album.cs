using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01StoreInterface
{
    class Album : Product
    {

        public String Artist { set; get; }
        public List<Track> Tracks;


        public Album(string title, double rating, DateTime release, string artist, List<Track> tracks) : base(title, rating, release)
        {
            this.Artist = artist;
            this.Tracks = tracks;
            this.Runtime = getRunTime();
        }

        public Album(string title, double rating, DateTime release, double pris, string artist, List<Track> tracks): base(title, rating, release, pris)
        {
            this.Artist = artist;
            this.Tracks = tracks;
            this.Runtime = getRunTime();
        }
        

        public void AddTrack(Track nTrack)
        {
            Tracks.Add(nTrack);
            Runtime = getRunTime();

        }
        
        
        public int antalLåtar()
        {
            return Tracks.Count;
        }

        public TimeSpan getRunTime()
        {

            TimeSpan total =new TimeSpan(0,0,0);

            foreach (var item in Tracks)
            {
                total = total.Add(item.Runtime);

            }

            return total;
        
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Title: {Title} Rating: {Rating} DateTime: {Releasedate.ToString("yyyy-MM-dd")} Runtime: {Runtime} Artist: {Artist} Price: {Price}");

            foreach (var item in Tracks)
            {
                Console.WriteLine($"\tTitle: {item.Title} Runtime: {item.Runtime} Feat: {item.Featuring}");
            }
        }



    }
}
