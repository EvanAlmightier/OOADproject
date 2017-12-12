using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Computer:Reservable
   {

      public Computer(int ID):base(ID)
      {

      }

      public Computer() : base()
      {

      }

      public override string GetType()
      {
         return "Computer";
      }

      public override string WriteRooms()
      {
         return base.WriteRooms();
      }
    }
}
