using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class Student:User
    {
        int reservationLimit;
        Student()
        {

        }

        public Student(int ID, string Name, string Email):base(ID,Name,Email)
        {

        }

        public Student(string Name, string Email):base(Name,Email)
        {

        }

        public override string GetType()
        {
            return "Student";
        }

        public bool isMaxedReservations()
        {
            return reservationCount != reservationLimit;
        }

    }
}
