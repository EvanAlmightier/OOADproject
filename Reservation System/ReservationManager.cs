using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class ReservationManager
    {
        protected Dictionary<int, User> users;
        protected Dictionary<int, Reservable> reservables;
        protected Dictionary<int, Reservation> reservations;

        ReservationManager()
        {
            // read in from text files to each map
            // initialize static variables to next id
        }

        public bool addUser(string type)
        {
            if (type == "student")
                Student toAdd = new Student()
            return true;
        }
    }
}
