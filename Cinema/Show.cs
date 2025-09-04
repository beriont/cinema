using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Show
    {
        public class ViewerAlreadyHasTicketException : Exception { }
        public class SeatIsTakenException : Exception { }
        public int id { get; private set; }
        public Movie movie { get; private set; }
        public DateTime start { get; private set; }
        public Room room { get; private set; }
        public int ticketPrice { get; private set; }
        private List<Ticket> tickets;
        public Show(int i, Movie m, DateTime s, Room r, int p)
        {
            id = i;
            movie = m;
            start = s;
            tickets = new();
            room = r;
            ticketPrice = p;
        }
        public int boughtTickets()
        {
            int c = 0;
            foreach (var e in tickets)
            {
                if (e.isPaid) c++;
            }
            return c;
        }
        public int reservedSeat()
        {
            int c = 0;
            foreach (var e in tickets)
            {
                if (!e.isPaid) c++;
            }
            return c;
        }
        public int freeSeat()
        {
            return room.seatsCount() - tickets.Count;
        }
        public void ticketPurchase(Ticket t)
        {
            tickets.Add(t);
        }
        public void ticketCheck(Viewer v, Seat s)
        {
            foreach (var t in tickets)
            {
                if (t.viewer == v) throw new ViewerAlreadyHasTicketException();
                if (t.seat == s) throw new SeatIsTakenException();
            }
        }
    }
}
