using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp
{
    public class BookingsObserver : IObserver
    {
        Customer User;
        public BookingsPage page { set; get; }
        public BookingsObserver(Customer User)
        {
            this.User = User;
        }

        public void Update()
        {
            List<Booking> bookings = User.Bookings;
            page.Update(bookings);
        }
    }
}
