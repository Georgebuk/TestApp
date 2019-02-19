using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedHotelPage : ContentPage
	{
        Hotel selectedHotel;
		public SelectedHotelPage()
		{
			InitializeComponent();
		}
		public SelectedHotelPage (Hotel selectedHotel)
		{
			InitializeComponent ();
            this.selectedHotel = selectedHotel;
            hotelImage.Source = "@drawable/premierInn.jpg";
            hotelName.Text = selectedHotel.HotelName;
            hotelDesc.Text = selectedHotel.HotelDesc;
            hotelNumberRooms.Text = "Number of Rooms: " + selectedHotel.numberOfRooms();
            //Format datepicker
            var tomorrowsDate = DateTime.Now.AddDays(1);
            bookingDatePicker.Date = tomorrowsDate;
            bookingDatePicker.MinimumDate = tomorrowsDate;
            bookingDatePicker.MaximumDate = DateTime.Now.AddYears(1);
		}

        private void BookButton_Clicked(object sender, EventArgs e)
        {
            var selectedBookingDate = bookingDatePicker.Date;
            if (selectedBookingDate != null)
            {
                var newBooking = new Booking
                {
                    BookingID = 0,
                    Customer = Globals.loggedInCustomer,
                    Hotel = selectedHotel,
                    DateBookingMade = DateTime.Now,
                    DateOfBooking = selectedBookingDate,
                    Activated = false,
                    HideBooking = false
                };
                try
                {
                    BookingRestService.Instance.SaveBookingAsync(newBooking, true);
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error","Error occured when attempting to create your booking.\nPlease try again", "OK");
                }
            }
        }
    }
}