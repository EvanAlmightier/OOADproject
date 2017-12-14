using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Reservable
   {
      protected static int nextID = 1;

      public int id { get; set; }

      /// <summary>
      /// constructor
      /// </summary>
      /// <param name="id">id number for reservable</param>
      /// <param name="r">Wheather it is reserved on creation</param>
      public Reservable(int ID)
      {
         id = ID;
         if (id + 1 > nextID)
            nextID = id + 1;
      }

      public Reservable()
      {
         id = nextID++;
      }

      public virtual string GetType()
      {
         return "";
      }

      public virtual string WriteRooms()
      {
         return "";
      }

      public string WriteReservable()
      {
         string rooms = null;
         string res = id.ToString();
         res = res + "|" + GetType();

         if (GetType() == "Computer")
         {
            rooms = WriteRooms();
            res = res + "|" + rooms;
         }

         return res;
      }

   }
}
