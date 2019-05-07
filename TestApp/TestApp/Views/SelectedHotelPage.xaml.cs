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

            bookButton.Text = "Pick your dates!";
            bookButton.IsEnabled = false;

			//Populate UI with hotel information
			this.selectedHotel = selectedHotel;
			hotelName.Text = selectedHotel.HotelName;
			hotelDesc.Text = selectedHotel.HotelDesc;
			//Format datepicker
			bookingStartDatePicker.Date = DateTime.Now;
			bookingStartDatePicker.MinimumDate = DateTime.Now;
			bookingStartDatePicker.MaximumDate = DateTime.Now.AddYears(1);
			for (int i = 1; i < 13; i++)
			{
				daysPicker.Items.Add(i.ToString());
			}
		}

		private async void BookButton_Clicked(object sender, EventArgs e)
		{
            ShowLoading();
			string responseString = "";
			var selectedBookingDate = bookingStartDatePicker.Date;
			if (selectedBookingDate != null)
			{
				Booking newBooking = CreateNewBooking(selectedBookingDate);
				try
				{
					var response = await BookingRestService.Instance.SaveBookingAsync(newBooking, true);
                    if (response == ErrorEnum.BOOKING_FAILED)
                    {
                        responseLabel.IsVisible = true;
                        bookButton.IsVisible = true;
                    }
                    else
                    {
                        SuccessLabel.IsVisible = true;
                        BookingViewModel.Instance.RefreshBookings();
                    }
				}
				catch (Exception ex)
				{
					await DisplayAlert("Error","Error occured when attempting to create your booking.\nPlease try again", "OK");
				}
			}
            HideLoading();
		}

		private void BookingStartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
		{
            AvailableLabel.Text = "";
            bookButton.Text = "Pick your dates!";
            bookButton.IsEnabled = false;
            AvailableIcon.IsVisible = false;
            if (daysPicker.SelectedIndex != -1)
			{
                
                ShowLoadingRooms();

                var selectedBookingDate = bookingStartDatePicker.Date;
				Booking testBooking = CreateNewBooking(selectedBookingDate);
				CheckForRooms(testBooking);

                HideLoadingRooms();
            }
		}

		private void DaysPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
            AvailableLabel.Text = "";
            bookButton.Text = "Pick your dates!";
            bookButton.IsEnabled = false;
            AvailableIcon.IsVisible = false;
            if (bookingStartDatePicker.Date != null)
			{
                
                ShowLoadingRooms();

				var selectedBookingDate = bookingStartDatePicker.Date;
				Booking testBooking = CreateNewBooking(selectedBookingDate);
				CheckForRooms(testBooking);

                HideLoadingRooms();
			}
		}

		async void CheckForRooms(Booking testBooking)
		{
			bool roomsAvailable = await HotelRestService.Instance.CheckRoomsForDates(testBooking);
			if (roomsAvailable)
			{
				AvailableLabel.Text = "Rooms are available!";
				AvailableLabel.TextColor = Color.Green;
                bookButton.Text = "Book Now!";
                bookButton.IsEnabled = true;
                AvailableIcon.IsVisible = true;
			}
			else
			{
				AvailableLabel.Text = "This hotel is fully booked on these dates";
				AvailableLabel.TextColor = Color.Red;
			}
		}

		private Booking CreateNewBooking(DateTime selectedBookingDate)
		{
			var newBooking = new Booking
			{
				BookingID = 0,
				Customer = Globals.loggedInCustomer,
				Hotel = selectedHotel,
				BookedRoom = new Room { RoomID = 1 }, 
				DateBookingMade = DateTime.Now,
				DateOfBooking = selectedBookingDate,
				BookingFinishDate = selectedBookingDate.AddDays(Convert.ToInt32(daysPicker.SelectedItem.ToString())),
				Activated = false,
				HideBooking = false
			};
			return newBooking;
		}

        private void ShowLoadingRooms()
        {
            loadingActivity.IsVisible = true;
            loadingActivity.IsRunning = true;
        }

        private void HideLoadingRooms()
        {
            loadingActivity.IsVisible = false;
            loadingActivity.IsRunning = false;
        }

        private void ShowLoading()
        {
            bookButton.IsVisible = false;
            loadingActivity.IsVisible = true;
            loadingActivity.IsRunning = true;
        }

        private void HideLoading()
        {
            loadingActivity.IsVisible = false;
            loadingActivity.IsRunning = false;
        }
    }
}