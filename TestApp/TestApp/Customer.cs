using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Customer: IObservable
    {
        public int CustId { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public List<IObserver> observers;
        private static readonly object padlock = new object();
        private static Customer instance;
        private List<Booking> bookings;
        public List<Booking> Bookings { get { return bookings; } set { bookings = value; Notify(); } }

        public string Full_name
        {
            get { return First_name + " " + Last_name; }
        }


        public static Customer ActiveCustomer
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Customer();
                    return instance;
                }
            }
        }

        private Customer()
        {
            //Replace with proper instansiation when logging in is added 
            CustId = 2;
            First_name = "George";
            Last_name = "Boulton";
            Email = "george.boulton@hotmail.co.uk";
            Phone_number = "07411762329";
            observers = new List<IObserver>();
            Bookings = new List<Booking>();
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            if (observers.Count > 0)
            {
                foreach (IObserver observer in this.observers)
                {
                    observer.Update();
                }
            }
        }
    }
}
