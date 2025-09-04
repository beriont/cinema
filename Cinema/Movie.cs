using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Movie
    {
        public class ShowAlreadyAddedException : Exception { }
        public string name { get; private set; }
        public int length { get; private set; }
        private List<Show> shows;
        public Movie(string n, int l)
        {
            name = n;
            length = l;
            shows = new();
        }
        public void addShow(Show s)
        {
            foreach (var show in shows)
            {
                if (show.id == s.id) throw new ShowAlreadyAddedException();
            }
            shows.Add(s);
        }
        public int totalViews()
        {
            int c = 0;
            foreach (var e in shows)
            {
                c += e.boughtTickets();
            }
            return c;
        }
    }
}
