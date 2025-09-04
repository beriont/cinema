using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Ticket
    {
        public Show show { get; private set; }
        public Seat seat { get; private set; }
        public Viewer viewer { get; private set; }
        public bool isPaid { get; private set; }
        public Ticket(Show sh, Seat se, Viewer v)
        {
            show = sh;
            seat = se;
            viewer = v;
            isPaid = false;
        }
        public int discountPrice()
        {
            return show.room.discountPrice(show, viewer);
        }
        public void purchase()
        {
            isPaid = true;
        }
    }
}
