using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModel;
using TestApp.Web_Service;
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

            if (!ThisBooking.Activated)
                CheckoutButton.IsVisible = false;
		}

        private async void CheckoutButton_Clicked(object sender, EventArgs e)
        {
            ShowLoading();
            if (await BookingRestService.Instance.Checkout(ThisBooking.BookingID)
                == ErrorEnum.SUCCESS)
            {
                SuccessLabel.IsVisible = true;
                CheckoutButton.IsEnabled = false;
                BookingViewModel.Instance.RefreshBookings();
                
            }
            HideLoading();
        }

        private void ShowLoading()
        {
            CheckoutButton.IsVisible = false;
            loadingActivity.IsVisible = true;
            loadingActivity.IsRunning = true;
        }

        private void HideLoading()
        {
            CheckoutButton.IsVisible = true;
            loadingActivity.IsVisible = false;
            loadingActivity.IsRunning = false;
        }
    }
}