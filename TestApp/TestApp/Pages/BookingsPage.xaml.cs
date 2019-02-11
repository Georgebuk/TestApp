using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class BookingsPage : ContentPage
    {
        public BookingsPage()
        {
            InitializeComponent();
            populateBookings();
        }

        async void populateBookings()
        {
            try
            {
                RestService service = new RestService();
                List<Booking> bookings = await service.RefreshDataAsync();
                foreach (Booking book in bookings)
                {
                    RoomBookingControl rbc = new RoomBookingControl
                    {
                        DateLabelText = book.DateOfBooking.Date.ToString(),
                        HotelLabelText = book.Hotel.HotelName,
                        RoomLabelText = "Room1"
                    };
                    hotelStack.Children.Add(rbc);
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }
    }
}
