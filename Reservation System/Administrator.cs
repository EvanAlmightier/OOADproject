using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class Administrator : User
    {

        public Administrator(string ID, string Name, string Email):base(ID,Name,Email)
        {

        }

        public override string GetType()
        {
            return "Instructor";
        }     
    }
}
