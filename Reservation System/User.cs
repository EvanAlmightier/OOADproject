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

        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        protected int reservationCount;

        public User()
        {

        }

        public User(int ID, string Name, string Email)
        {
            id = ID;
            name = Name;
            email = Email;
            nextID = id + 1;
        }

        public User(string Name, string Email)
        {
            id = nextID++;
            name = Name;
            email = Email;
        }

        public virtual string GetType()
        {
            return "User";
        }

        public void CreateReservation()
        {
            ++reservationCount;
        }

        public string WriteUser()
        {
            string user = id.ToString();
            user = user + "|" + name;
            user = user + "|" + email;
            user = user + "|" + GetType();

            return user;
        }

    }
}
