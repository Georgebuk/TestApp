using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Customer
    {
        public int CustId { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public List<Booking> Bookings { get; set; }

        public string Full_name
        {
            get { return First_name + " " + Last_name; }
        }

        public Customer()
        {
            //Replace with proper instansiation when logging in is added 
            CustId = 1;
            First_name = "George";
            Last_name = "Boulton";
            Email = "george.boulton@hotmail.co.uk";
            Phone_number = "07411762329";
            Bookings = new List<Booking>();
        }
    }
}
