using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_System
{
   class Room:Reservable
   {
      protected List<Computer> computerList;
      
      public Room(int ID):base(ID)
      {
         computerList = new List<Computer>();
      }

      public Room() : base()
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

      private bool ComputerInList(int id)
      {
         foreach(Computer c in computerList)
         {
            if (c.id == id)
               return true;
         }
         return false;
      }

      public override string WriteRooms()
      {
         string rooms = null;
         foreach (Computer element in computerList)
         {
            rooms = rooms + "|" + element.id;
         }
            return rooms;
      }
   }
}
