using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class User
    {
        protected static int nextID = 1;

        protected int id;
        protected string name;
        protected string email;
        protected int reservationCount;

        public User()
        {

        }

        public User(int ID, string Name, string Email)
        {
            id = ID;
            name = Name;
            email = Email;
            nextID = ++id;
        }

        public User(string Name, string Email)
        {
            id = nextID++;
            name = Name;
            email = Email;
        }

        public virtual string GetType()
        {
            return "user";
        }

        public void CreateReservation()
        {
            ++reservationCount;
        }

    }
}
