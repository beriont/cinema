using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Seat
    {
        public char row { get; private set; }
        public int num { get; private set; }
        public Seat(char row, int num)
        {
            this.row = row;
            this.num = num;
        }
    }
}
