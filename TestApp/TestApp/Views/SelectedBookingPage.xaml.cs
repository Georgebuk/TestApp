using HotelClassLibrary;
using System;
using System.Drawing;
using System.IO;
using TestApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedBookingPage : TabbedPage
	{
        Booking ThisBooking;
		public SelectedBookingPage (Booking booking)
		{
            try
            {
                InitializeComponent();
                ThisBooking = booking;

                var infoPage = new NavigationPage(new BookingInfoPage(booking));
                infoPage.Title = "Info";
                infoPage.Icon = "info.png";

                var navigationPage = new NavigationPage(new BarcodePage(booking));
                navigationPage.Title = "Key";
                navigationPage.Icon = "key.png";

                Children.Add(infoPage);
                Children.Add(navigationPage);
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
	}
}