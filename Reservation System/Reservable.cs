﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
    class Reservable
    {
        protected static int nextID = 1;

        public int id { get; set; }
        protected bool reserved;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id number for reservable</param>
        /// <param name="r">Wheather it is reserved on creation</param>
        public Reservable(int ID, bool r = false)
        {
            id = ID;
            reserved = r;
            nextID = ++id;
        }

        public Reservable(bool r = false)
        {
            id = nextID++;
            reserved = r;
        }

        public virtual string GetType()
        {
            return "";
        }

        public virtual void ResetStatus()
        {
            reserved = false;
        }

        public virtual void SetToReserved()
        {
            reserved = true;
        }

        public virtual string WriteRooms()
        {
            return "";
        }

        public string WriteReservable()
        {
            string rooms = null;
            string res = id.ToString();
            res = res + "|" + reserved.ToString();
            res = res + "|" + GetType();

            if (GetType() == "Computer")
            {
                rooms = WriteRooms();
                res = res + "|" + rooms;
            }

            return res;
        }

   }
}
