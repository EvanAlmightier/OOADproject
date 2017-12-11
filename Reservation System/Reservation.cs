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
            reservedBy = userID;
            reservable = res;
            resStart = date;
            resEnd = date.AddHours(duration);
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
            resEnd = date.AddHours(duration);
        }


        public string WriteRes()
        {
            string res = id.ToString();
            res = res + "|" + reservedBy.ToString();
            res = res + "|" + reservable.ToString();
            res = res + "|" + resStart.ToString();
            res = res + "|" + resEnd.ToString();

            return res;
        }

    }
}
