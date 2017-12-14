using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: Sort functions
// TODO: Time/Date check for Reservations

namespace Reservation_System
{
   class ReservationManager
   {
      public Dictionary<int, User> Users { get; set; }
      public Dictionary<int, Reservable> Reservables { get; set; }
      public Dictionary<int, Reservation> Reservations { get; set; }

      public int activeUser { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public ReservationManager()
      {
         // read in from text files to each map
         // initialize static variables to next id
         Users = new Dictionary<int, User>();
         Reservables = new Dictionary<int, Reservable>();
         Reservations = new Dictionary<int, Reservation>();
         ReadUsers();
         ReadReservables();
         ReadReservations();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public bool Save()
      {
         bool users = SaveUsers();
         bool Reservables = SaveReservables();
         bool Reservations = SaveReservations();
         return users && Reservables && Reservations;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="ActiveUser"></param>
      public void SetActiveUser(int ActiveUser)
      {
         activeUser = ActiveUser;
      }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="type"></param>
      /// <param name="name"></param>
      /// <param name="email"></param>
      /// <returns></returns>
      public bool AddUser(string type, string name, string email)
      {
         User toAdd = null;
         if (type == "Student")
         {
            toAdd = new Student(name, email);
         }
         else if (type == "Instructor")
         {
            toAdd = new Instructor(name, email);
         }
         else if (type == "Administrator")
         {
            toAdd = new Administrator(name, email);
         }
         else
            return false;

         Users.Add(toAdd.id, toAdd);
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="type"></param>
      /// <param name="name"></param>
      /// <param name="email"></param>
      /// <returns></returns>
      public bool AddUser(int id, string type, string name,
          string email)
      {
         User toAdd = null;
         if (type == "Student")
         {
            toAdd = new Student(id, name, email);
         }
         else if (type == "Instructor")
         {
            toAdd = new Instructor(id, name, email);
         }
         else if (type == "Administrator")
         {
            toAdd = new Administrator(id, name, email);
         }
         else
            return false;

         Users.Add(id, toAdd);
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public bool RemoveUser(int id)
      {
         if (Users.ContainsKey(id))
         {
            Users.Remove(id);
            return true;
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public User GetUser(int id)
      {
         User toGet = null;
         bool found = Users.TryGetValue(id, out toGet);
         if (found)
            return toGet;
         else
            return null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="type"></param>
      /// <param name="name"></param>
      /// <param name="email"></param>
      /// <returns></returns>
      public bool EditUser(int id, string type, string name, string email)
      {
         if (Users.ContainsKey(id))
         {
            RemoveUser(id);
            AddUser(id, type, name, email);
            return true;
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public SortedList<string, User> SortByName()
      {
         SortedList<string, User> names = new SortedList<string, User>();
         foreach (KeyValuePair<int, User> element in Users)
         {
            names.Add(element.Value.name, element.Value);
         }
         return names;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="type"></param>
      /// <param name="reserved"></param>
      /// <returns></returns>
      public bool AddReservable(string type, int RoomID)
      {
         Reservable toAdd = null;
         if (type == "Computer")
         {
            toAdd = new Computer();
            ((Room)GetReservable(RoomID)).AddComputer((Computer)toAdd);
         }
         else if (type == "Room")
         {
            toAdd = new Room();
         }
         else
            return false;

         Reservables.Add(toAdd.id, toAdd);
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="type"></param>
      /// <param name="reserved"></param>
      /// <returns></returns>
      public bool AddReservable(int id, string type, int RoomID)
      {
         Reservable toAdd = null;
         if (type == "Computer")
         {
            toAdd = new Computer(id);
            ((Room)GetReservable(RoomID)).AddComputer((Computer)toAdd);
         }
         else if (type == "Room")
         {
            toAdd = new Room(id);
         }
         else
            return false;

         Reservables.Add(id, toAdd);
         return true;
      }

      public void MoveReservable(int computerID, int endRoomID)
      {
         Reservable tempComputer = GetReservable(computerID);
         RemoveReservable(computerID);
         AddReservable(tempComputer.id, "Computer", endRoomID);

      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public bool RemoveReservable(int id)
      {
         if (Reservables.ContainsKey(id))
         {
            if (GetReservable(id).GetType() == "Room")
            {
               int[] tempIDs = ((Room)GetReservable(id)).GetComputerIDs();
               foreach (int i in tempIDs)
               {
                  Reservables.Remove(i);
               }
            }
            else
            {
               int[] tempRIDs = GetRoomIDs();
               foreach (int i in tempRIDs)
               {
                  ((Room)GetReservable(i)).removeComputer((Computer)GetReservable(id));
               }
            }
            Reservables.Remove(id);
            return true;
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public Reservable GetReservable(int id)
      {
         Reservable toGet = null;
         bool found = Reservables.TryGetValue(id, out toGet);
         if (found)
            return toGet;
         else
            return null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="type"></param>
      /// <param name="reserved"></param>
      /// <returns></returns>
      public bool EditReservable(int id, string type, bool reserved)
      {
         if (Reservables.ContainsKey(id))
         {
            RemoveReservable(id);
            AddReservable(id, type, 0);
            return true;
         }
         return false;
      }


      public SortedList<string, Reservable> SortByType()
      {
         SortedList<string, Reservable> types = new SortedList<string, Reservable>();
         foreach (KeyValuePair<int, Reservable> element in Reservables)
         {
            types.Add(element.Value.GetType(), element.Value);
         }
         return types;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="user"></param>
      /// <param name="reservable"></param>
      /// <param name="date"></param>
      /// <param name="duration"></param>
      /// <returns></returns>
      public bool AddReservation(int user, int reservable,
          DateTime date, double duration)
      {
         if (!Available(reservable, date, duration))
            return false;
         Reservation toAdd = new Reservation(user, reservable,
             date, duration);
         Reservations.Add(toAdd.id, toAdd);
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="user"></param>
      /// <param name="reservable"></param>
      /// <param name="date"></param>
      /// <param name="duration"></param>
      /// <returns></returns>
      public bool AddReservation(int id, int user, int reservable,
          DateTime date, double duration)
      {
         if (!Available(reservable, date, duration))
            return false;
         Reservation toAdd = new Reservation(id, user, reservable,
             date, duration);
         Reservations.Add(id, toAdd);
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public bool RemoveReservation(int id)
      {
         if (Reservations.ContainsKey(id))
         {
            Reservations.Remove(id);
            return true;
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public Reservation GetReservation(int id)
      {
         Reservation toGet = null;
         bool found = Reservations.TryGetValue(id, out toGet);
         if (found)
            return toGet;
         else
            return null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="user"></param>
      /// <param name="reservable"></param>
      /// <param name="date"></param>
      /// <param name="duration"></param>
      /// <returns></returns>
      public bool EditReservation(int id, int user, int reservable,
          DateTime date, double duration)
      {
         if (Reservations.ContainsKey(id))
         {
            RemoveReservation(id);
            AddReservation(id, user, reservable, date, duration);
            return true;
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public SortedList<int, Reservation> SortByUser()
      {
         SortedList<int, Reservation> sort = new SortedList<int, Reservation>();
         foreach (KeyValuePair<int, Reservation> element in Reservations)
         {
            sort.Add(element.Value.reservedBy, element.Value);
         }
         return sort;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public SortedList<int, Reservation> SortByReservable()
      {
         SortedList<int, Reservation> sort = new SortedList<int, Reservation>();
         foreach (KeyValuePair<int, Reservation> element in Reservations)
         {
            sort.Add(element.Value.reservable, element.Value);
         }
         return sort;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public SortedList<DateTime, Reservation> SortByStart()
      {
         SortedList<DateTime, Reservation> sort = new SortedList<DateTime, Reservation>();
         foreach (KeyValuePair<int, Reservation> element in Reservations)
         {
            sort.Add(element.Value.resStart, element.Value);
         }
         return sort;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="start"></param>
      /// <param name="duration"></param>
      /// <returns></returns>

      // TODO: use filter to get just Reservationsfor specified reservable
      // TODO: check each reservation time against the specified times

      public bool Available(int id, DateTime start, double duration)
      {
         DateTime end = start.AddHours(duration);

         SortedList<int, Reservation> sorted = SortByReservable();

         foreach (KeyValuePair<int, Reservation> element in sorted)
         {
            if (element.Key != id)
               sorted.Remove(element.Key);
         }

         foreach (KeyValuePair<int, Reservation> element in sorted)
         {
            if (element.Value.resStart >= start && element.Value.resStart <= end)
               return false;
            if (element.Value.resEnd >= start && element.Value.resEnd <= end)
               return false;
         }
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="start"></param>
      /// <param name="duration"></param>
      /// <returns></returns>
      public SortedList<int, Reservable> GetAvailable(string type, DateTime start, int duration)
      {
         SortedList<int, Reservable> available = new SortedList<int, Reservable>();
         if (type != "None")
         {
            SortedList<string, Reservable> sort = SortByType();

            if (type == "Computer")
            {
               foreach (KeyValuePair<string, Reservable> element in sort)
               {
                  if (element.Key == "Computer")
                     if (Available(element.Value.id, start, duration))
                        available.Add(element.Value.id, element.Value);
               }
            }
            else if (type == "Room")
            {
               foreach (KeyValuePair<string, Reservable> element in sort)
               {
                  if (element.Key == "Room")
                     available.Add(element.Value.id, element.Value);
               }
            }
            else
               available = null;
         }
         else
         {
            foreach (KeyValuePair<int, Reservable> element in Reservables)
            {
               available.Add(element.Value.id, element.Value);
            }
         }

         return available;
      }

      public int[] GetUserIDs()
      {
         List<int> IDs = new List<int>();
         foreach (KeyValuePair<int, User> kvp in Users)
         {
            IDs.Add(kvp.Key);
         }
         return IDs.ToArray();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public int[] GetRoomIDs()
      {
         List<int> IDs = new List<int>();
         foreach (KeyValuePair<int, Reservable> kvp in Reservables)
         {
            if (kvp.Value.GetType() == "Room")
            {
               IDs.Add(kvp.Key);
            }
         }
         return IDs.ToArray();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public int[] GetComputerIDs()
      {
         List<int> IDs = new List<int>();
         foreach (KeyValuePair<int, Reservable> kvp in Reservables)
         {
            if (kvp.Value.GetType() == "Computer")
            {
               IDs.Add(kvp.Key);
            }
         }
         return IDs.ToArray();
      }


      /// <summary>
      /// 
      /// </summary>
      public void ReadUsers()
      {
         List<List<string>> userList = ReadFile("users.txt");
         foreach (List<string> ls in userList)
         {
            AddUser(int.Parse(ls[0]), ls[3], ls[1], ls[2]);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      public void ReadReservables()
      {
         List<List<string>> reservablesList = ReadFile("reservables.txt");
         foreach (List<string> ls in reservablesList)
         {
            AddReservable(int.Parse(ls[0]), ls[1], int.Parse(ls[2]));
         }
      }

      /// <summary>
      /// 
      /// </summary>
      public void ReadReservations()
      {
         List<List<string>> reservationsList = ReadFile("reservations.txt");
         foreach (List<string> ls in reservationsList)
         {
            string[] date = ls[3].Split(' ')[0].Split('/');
            string[] time = ls[3].Split(' ')[1].Split(':');
            DateTime dt = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]),
               int.Parse(time[0]), int.Parse(time[1]), 0);
            AddReservation(int.Parse(ls[0]), int.Parse(ls[1]), int.Parse(ls[2]), dt, double.Parse(ls[4]));
         }
      }

      private List<List<string>> ReadFile(string path)
      {
         string basePath = "./";
         List<List<string>> file = new List<List<string>>();
         using (StreamReader sr = new StreamReader(basePath + path))
         {
            while (!sr.EndOfStream)
            {
               string line = sr.ReadLine();
               List<string> items = new List<string>(line.Split('|'));
               file.Add(items);
            }
         }
         return file;
      }

      private void WriteFile(string path, List<string> file)
      {
         using (StreamWriter sw = new StreamWriter(path))
         {
            foreach (string line in file)
            {
               sw.WriteLine(line);
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public bool SaveUsers()
      {
         try
         {
            List<string> users = new List<string>();
            foreach (KeyValuePair<int, User> pair in Users)
            {
               User user = pair.Value;
               users.Add(user.WriteUser());
            }
            WriteFile("users.txt", users);
            return true;
         }
         catch (Exception e) { return false; }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public bool SaveReservables()
      {
         try
         {
            List<string> reservables = new List<string>();
            foreach (KeyValuePair<int, Reservable> pair in Reservables)
            {
               Reservable reservable = pair.Value;
               reservables.Add(reservable.WriteReservable());
            }
            WriteFile("reservables.txt", reservables);
            return true;
         }
         catch (Exception e) { return false; }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public bool SaveReservations()
      {
         try
         {
            List<string> reservations = new List<string>();
            foreach (KeyValuePair<int, Reservation> pair in Reservations)
            {
               Reservation reservation = pair.Value;
               reservations.Add(reservation.WriteRes());
            }
            WriteFile("reservations.txt", reservations);
            return true;
         }
         catch (Exception e) { return false; }
      }

   }
}
