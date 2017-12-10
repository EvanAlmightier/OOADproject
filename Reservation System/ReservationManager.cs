using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: Sort functions
// TODO: Time/Date check for reservations

namespace Reservation_System
{
    class ReservationManager
    {
        public Dictionary<int, User> users { get; set; }
        public Dictionary<int, Reservable> reservables { get; set; }
        public Dictionary<int, Reservation> reservations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ReservationManager()
        {
            // read in from text files to each map
            // initialize static variables to next id
            readUsers();
            readReservables();
            readReservations();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool save()
        {
            bool users = saveUsers();
            bool reservables = saveReservables();
            bool reservations = saveReservations();
            return users && reservables && reservations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool addUser(string type, string name, string email)
        {
            User toAdd = null;
            if (type == "student")
            {
                toAdd = new Student(name, email);
            }
            else if (type == "Instructor")
            {
                toAdd = new Instructor(name, email);
            }
            else if (type == "administrator")
            {
                toAdd = new Administrator(name, email);
            }
            else
                return false;

            users.Add(toAdd.id, toAdd);
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
        public bool addUser(int id, string type, string name,
            string email)
        {
            User toAdd = null;
            if (type == "student")
            {
                toAdd = new Student(id, name, email);
            }
            else if (type == "Instructor")
            {
                toAdd = new Instructor(id, name, email);
            }
            else if (type == "administrator")
            {
                toAdd = new Administrator(id, name, email);
            }
            else
                return false;

            users.Add(id, toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeUser(int id)
        {
            if (users.ContainsKey(id))
            {
                users.Remove(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User getUser(int id)
        {
            User toGet = null;
            bool found = users.TryGetValue(id, out toGet);
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
        public bool editUser(int id, string type, string name, string email)
        {
            if (users.ContainsKey(id))
            {
                removeUser(id);
                addUser(id, type, name, email);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SortedList<string, User> sortByName()
        {
            SortedList<string, User> names = new SortedList<string, User>();
            foreach(KeyValuePair<int, User> element in users)
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
        public bool addReservable(string type, bool reserved)
        {
            Reservable toAdd = null;
            if (type == "Computer")
            {
                toAdd = new Computer(reserved);
            }
            else if (type == "Room")
            {
                toAdd = new Room(reserved);
            }
            else
                return false;

            reservables.Add(toAdd.id, toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        public bool addReservable(int id, string type, bool reserved)
        {
            Reservable toAdd = null;
            if (type == "Computer")
            {
                toAdd = new Computer(id, reserved);
            }
            else if (type == "Room")
            {
                toAdd = new Room(id, reserved);
            }
            else
                return false;

            reservables.Add(id, toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeReservable(int id)
        {
            if (reservables.ContainsKey(id))
            {
                reservables.Remove(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reservable getReservable(int id)
        {
            Reservable toGet = null;
            bool found = reservables.TryGetValue(id, out toGet);
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
        public bool editReservable(int id, string type, bool reserved)
        {
            if (reservables.ContainsKey(id))
            {
                removeReservable(id);
                addReservable(id, type, reserved);
                return true;
            }
            return false;
        }


        public SortedList<string, Reservable> sortByType()
        {
            SortedList<string, Reservable> types = new SortedList<string, Reservable>();
            foreach (KeyValuePair<int, Reservable> element in reservables)
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
        public bool addReservation(int user, int reservable,
            DateTime date, double duration)
        {
            if (!available(reservable, date, duration))
                return false;
            Reservation toAdd = new Reservation(user, reservable,
                date, duration);
            reservations.Add(toAdd.id, toAdd);
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
        public bool addReservation(int id, int user, int reservable,
            DateTime date, double duration)
        {
            if (!available(reservable, date, duration))
                return false;
            Reservation toAdd = new Reservation(id, user, reservable,
                date, duration);
            reservations.Add(id, toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeReservation(int id)
        {
            if (reservations.ContainsKey(id))
            {
                reservations.Remove(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reservation getReservation(int id)
        {
            Reservation toGet = null;
            bool found = reservations.TryGetValue(id, out toGet);
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
        public bool editReservation(int id, int user, int reservable,
            DateTime date, double duration)
        {
            if (reservations.ContainsKey(id))
            {
                removeReservation(id);
                addReservation(id, user, reservable, date, duration);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SortedList<int, Reservation> sortByUser()
        {
            SortedList<int, Reservation> sort = new SortedList<int, Reservation>();
            foreach (KeyValuePair<int, Reservation> element in reservations)
            {
                sort.Add(element.Value.reservedBy, element.Value);
            }
            return sort;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SortedList<int, Reservation> sortByReservable()
        {
            SortedList<int, Reservation> sort = new SortedList<int, Reservation>();
            foreach (KeyValuePair<int, Reservation> element in reservations)
            {
                sort.Add(element.Value.reservable, element.Value);
            }
            return sort;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SortedList<DateTime, Reservation> sortByStart()
        {
            SortedList<DateTime, Reservation> sort = new SortedList<DateTime, Reservation>();
            foreach (KeyValuePair<int, Reservation> element in reservations)
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

        // TODO: use filter to get just reservations for specified reservable
        // TODO: check each reservation time against the specified times

        public bool available(int id, DateTime start, double duration)
        {
            DateTime end = start.AddHours(duration);

            SortedList<int, Reservation > sorted = sortByReservable();

            foreach(KeyValuePair<int, Reservation> element in sorted)
            {
                if (element.Key != id)
                    sorted.Remove(element.Key);
            }

            foreach(KeyValuePair<int, Reservation> element in sorted)
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
        public SortedList<int, Reservable> getAvailable(string type, DateTime start, int duration)
        {
            SortedList<int, Reservable> available = new SortedList<int, Reservable>();
            if (type != "None")
            {
                SortedList<string, Reservable> sort = sortByType();

                if (type == "Computer")
                {
                    foreach (KeyValuePair<string, Reservable> element in sort)
                    {
                        if (element.Key == "Computer")
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

            return available;
        }

        /// <summary>
        /// 
        /// </summary>
        public void readUsers()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void readReservables()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void readReservations()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool saveUsers()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool saveReservables()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool saveReservations()
        {
            return true;
        }

    }
}
