﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Room:Reservable
   {
      protected List<Computer> computerList;

      public Room(int ID, bool r):base(ID,r)
      {
         computerList = new List<Computer>();
      }

      public Room(bool r = false) : base(r)
      {
         computerList = new List<Computer>();
      }

      public bool AddComputer(Computer c)
      {
         if (!ComputerInList(c.id))
         {
            computerList.Add(c);
            return true;
         }
         return false;
      }

      public override string GetType()
      {
         return "Room";
      }

      public override void SetToReserved()
      {
         base.SetToReserved();
      }

      public override void ResetStatus()
      {
         base.ResetStatus();
      }

      private bool ComputerInList(int id)
      {
         foreach(Computer c in computerList)
         {
            if (c.id == id)
               return false;
         }
         return true;
      }
   }
}
