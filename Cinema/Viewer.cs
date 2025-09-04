using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public abstract class Viewer
    {
        public class NoReservationException : Exception { }
        private string name;
        private List<Ticket> tickets;
        public Viewer(string n)
        {
            name = n;
            tickets = new();
        }
        public void reserve(Show sh, Seat se)
        {
            sh.ticketCheck(this, se);
            Ticket t = new Ticket(sh, se, this);
            tickets.Add(t);
            sh.ticketPurchase(t);
        }
        public void purchaseReserved(Show sh, Seat se)
        {
            Ticket? t = null;
            int ind = 0;
            while (t == null && ind < tickets.Count)
            {
                if (tickets[ind].show == sh && tickets[ind].seat == se) t = tickets[ind];
                else ind++;
            }
            if (t == null) throw new NoReservationException();
            t.purchase();
        }
        public void purchase(Show sh, Seat se)
        {
            sh.ticketCheck(this, se);
            Ticket t = new Ticket(sh, se, this);
            t.purchase();
            tickets.Add(t);
            sh.ticketPurchase(t);
        }
        public virtual int discount(SmallRoom r) => 0;
        public virtual int discount(LargeRoom r) => 0;
        public virtual int discount(VIPRoom r) => 0;
    }
    public class Child : Viewer
    {
        public Child(string n) : base(n) { }

        public override int discount(SmallRoom r) => 40;
        public override int discount(LargeRoom r) => 40;
        public override int discount(VIPRoom r) => 40;
    }
    public class Student : Viewer
    {
        public Student(string n) : base(n) { }

        public override int discount(SmallRoom r) => 30;
        public override int discount(LargeRoom r) => 20;
    }
    public class Adult : Viewer
    {
        public Adult(string n) : base(n) { }

        public override int discount(SmallRoom r) => 10;
    }
    public class Pensioner : Viewer
    {
        public Pensioner(string n) : base(n) { }

        public override int discount(SmallRoom r) => 30;
        public override int discount(LargeRoom r) => 20;
    }
    public class Member : Viewer
    {
        public Member(string n) : base(n) { }

        public override int discount(SmallRoom r) => 30;
        public override int discount(LargeRoom r) => 30;
    }
}
