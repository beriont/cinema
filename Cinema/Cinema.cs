using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace Cinema
{
    public class Cinema
    {
        public class RoomAlreadyAddedException : Exception { }
        public class MovieAlreadyAddedException : Exception { }
        public class RoomNotFoundException : Exception { }
        private string name;
        private List<Room> rooms;
        private List<Movie> movies;
        public Cinema(string n)
        {
            name = n;
            rooms = new();
            movies = new();
        }
        public void addRoom(Room r)
        {
            foreach (var room in rooms)
            {
                if (room.name == r.name) throw new RoomAlreadyAddedException();
            }
            rooms.Add(r);
        }
        public void addMovie(Movie m)
        {
            foreach (var movie in movies)
            {
                if (movie.name == m.name && movie.length == m.length) throw new MovieAlreadyAddedException();
            }
            movies.Add(m);
        }
        public Movie mostViewers()
        {
            int max = -1;
            Movie? elem = null;
            foreach (var e in movies)
            {
                int t = e.totalViews();
                if (t > max)
                {
                    max = t;
                    elem = e;
                }
            }
            return elem!;
        }
    }
}
