using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class Instructor:User
    {
        int reservationLimit;
        Instructor()
        {

        }

        public Instructor(int ID, string Name, string Email):base(ID,Name,Email)
        {

        }

        public Instructor(string Name, string Email):base(Name,Email)
        {

        }

        public override string GetType()
        {
            return "Instructor";
        }

        public bool isMaxedReservations()
        {
            return reservationCount != reservationLimit;
        }

    }
}
