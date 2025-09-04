using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public abstract class Room
    {
        public class SeatAlreadyAddedException : Exception { }
        public string name { get; private set; }
        private List<Seat> seats;
        public IReadOnlyList<Seat> getSeats() => seats.AsReadOnly();
        protected Room(string n)
        {
            name = n;
            seats = new();
        }
        public void addSeat(Seat s)
        {
            foreach (var seat in seats)
            {
                if (seat.row == s.row && seat.num == s.num) throw new SeatAlreadyAddedException();
            }
            seats.Add(s);
        }
        public int seatsCount()
        {
            return seats.Count;
        }
        public abstract int discountPrice(Show s, Viewer v);
    }
    public class SmallRoom : Room
    {
        public SmallRoom(string n) : base(n) { }
        public override int discountPrice(Show s, Viewer v) => s.ticketPrice * (100 - v.discount(this)) / 100;
    }
    public class LargeRoom : Room
    {
        public LargeRoom(string n) : base(n) { }
        public override int discountPrice(Show s, Viewer v) => s.ticketPrice * (100 - v.discount(this)) / 100;
    }
    public class VIPRoom : Room
    {
        public VIPRoom(string n) : base(n) { }
        public override int discountPrice(Show s, Viewer v) => s.ticketPrice * (100 - v.discount(this)) / 100;
    }
}
