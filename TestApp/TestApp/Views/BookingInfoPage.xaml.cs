using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookingInfoPage : ContentPage
	{
        Booking ThisBooking;
		public BookingInfoPage (Booking booking)
		{
			InitializeComponent ();

            ThisBooking = booking;
            BindingContext = ThisBooking;

            string bookingRage = "Your booking will be commence on " 
                + ThisBooking.DateOfBooking + " and will be valid until "
                + ThisBooking.BookingFinishDate;
		}
	}
}