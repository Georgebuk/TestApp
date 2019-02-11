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
        BookingsObserver observer;
        public BookingsPage()
        {
            InitializeComponent();
            try
            {
                observer = new BookingsObserver(Customer.ActiveCustomer) { page = this };
                Customer.ActiveCustomer.AddObserver(observer);

                ServerConnection connection = ServerConnection.Instance;
                connection.sendRequest("Bookings");
            }
            catch (Exception e)
            {
                string error = e.ToString();
            }
            
        }

        async void TestButton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Page1());
        }

        public void Update(List<Booking> bookings)
        {
            Device.BeginInvokeOnMainThread(() => {
                hotelStack.Children.Clear();
                foreach (Booking b in bookings)
                {
                    RoomBookingControl control = new RoomBookingControl
                    {
                        DateLabelText = b.DateBookingMade.Date.ToString(),
                        HotelLabelText = b.Hotel.HotelName,
                        RoomLabelText = "Room 1"
                    };
                    hotelStack.Children.Add(control);
                }
            });
           
        }
    }
}
