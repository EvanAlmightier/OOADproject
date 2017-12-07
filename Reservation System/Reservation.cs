using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class Reservation
    {
        protected int reservedBy { get; set; }
        protected int reservable { get; set; }
        protected DateTime resStart { get; set; }
        protected DateTime resEnd { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public Reservation()
        {
            reservedBy = 0;
            reservable = 0;
            resStart = new DateTime();
            resEnd = new DateTime();
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userID">user ID for user making reservation</param>
        /// <param name="res">ID of reservable being reserved</param>
        /// <param name="date">DateTime of reservation</param>
        /// <param name="duration">duration of the reservation</param>
        public Reservation(int userID, int res, DateTime date, double duration)
        {
            reservedBy = userID;
            reservable = res;
            resStart = date;
            resEnd = date.AddHours(duration);
        }

    }
}
