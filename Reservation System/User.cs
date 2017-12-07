using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class User
    {
        protected string id;
        protected string name;
        protected string email;
        protected int reservationCount;

        public User()
        {

        }

        public User(string ID, string Name, string Email)
        {
            id = ID;
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
