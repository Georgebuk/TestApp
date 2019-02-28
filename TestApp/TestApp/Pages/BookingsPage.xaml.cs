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
                BookingRestService service = BookingRestService.Instance;
                List<Booking> bookings = await service.RefreshDataAsync();
                foreach (Booking book in bookings)
                {
                    RoomBookingControl rbc = new RoomBookingControl
                    {
                        DateLabelText = book.DateOfBooking.Date.ToString(),
                        HotelLabelText = book.Hotel.HotelName,
                        RoomLabelText = book.BookedRoom.RoomNumber.ToString(),
                        ThisBooking = book
                        
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
