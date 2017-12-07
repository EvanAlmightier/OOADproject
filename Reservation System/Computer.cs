using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Computer:Reservable
   {

      public Computer(int ID, bool r = false):base(ID,r)
      {

      }

      public Computer(bool r = false) : base(r)
      {

      }

        public override string GetType()
      {
         return "Computer";
      }
   }
}
