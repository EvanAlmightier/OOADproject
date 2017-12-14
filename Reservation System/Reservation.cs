using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Reservation
   {
      protected static int nextID = 1;

      public int id { get; set; }
      public int reservedBy { get; set; }
      public int reservable { get; set; }
      public DateTime resStart { get; set; }
      public DateTime resEnd { get; set; }

      /// <summary>
      /// default constructor
      /// </summary>
      public Reservation()
      {
         id = 0;
         reservedBy = 0;
         reservable = 0;
         resStart = new DateTime();
         resEnd = new DateTime();
      }

      /// <summary>
      /// constructor
      /// </summary>
      /// <param name="ID">reservation ID</param>
      /// <param name="userID">user ID for user making reservation</param>
      /// <param name="res">ID of reservable being reserved</param>
      /// <param name="date">DateTime of reservation</param>
      /// <param name="duration">duration of the reservation</param>
      public Reservation(int ID, int userID, int res, DateTime date,
          double duration)
      {
         id = ID;
         nextID = ID + 1;
         reservedBy = userID;
         reservable = res;
         resStart = date;
         resEnd = date.AddMinutes(duration);
      }

      /// <summary>
      /// constructor
      /// </summary>
      /// <param name="userID">user ID for user making reservation</param>
      /// <param name="res">ID of reservable being reserved</param>
      /// <param name="date">DateTime of reservation</param>
      /// <param name="duration">duration of the reservation</param>
      public Reservation(int userID, int res, DateTime date,
          double duration)
      {
         id = nextID++;
         reservedBy = userID;
         reservable = res;
         resStart = date;
         resEnd = date.AddMinutes(duration);
      }


      public string WriteRes()
      {
         string dt = (resEnd - resStart).ToString();
         string[] DA = dt.Split(':');
         int duration = int.Parse(DA[0]) * 60 + int.Parse(DA[1]);

         string res = id.ToString();
         res = res + "|" + reservedBy.ToString();
         res = res + "|" + reservable.ToString();
         res = res + "|" + resStart.ToString();
         res = res + "|" + duration.ToString();

         return res;
      }

   }
}
