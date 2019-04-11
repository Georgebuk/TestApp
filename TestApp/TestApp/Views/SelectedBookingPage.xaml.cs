using HotelClassLibrary;
using System.Drawing;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace TestApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedBookingPage : ContentPage
	{
        Booking ThisBooking;
		public SelectedBookingPage (Booking booking)
		{
			InitializeComponent();
            ThisBooking = booking;
            QRImageView.BarcodeValue = booking.QrcodeString;
        }
	}
}