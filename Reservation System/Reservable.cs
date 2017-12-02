using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Reservable
   {
      public int id { get; set; }
      protected bool reserved;

      /// <summary>
      /// constructor
      /// </summary>
      /// <param name="id">id number for reservable</param>
      /// <param name="r">Wheather it is reserved on creation</param>
      public Reservable(int ID, bool r = false)
      {
         id = ID;
         reserved = r;
      }

      public virtual string GetType()
      {
         return "";
      }

      public virtual void ResetStatus()
      {
         reserved = false;
      }

      public virtual void SetToReserved()
      {
         reserved = true;
      }

   }
}
