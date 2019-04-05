using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			//Populate UI with hotel information
			this.selectedHotel = selectedHotel;
			hotelImage.Source = "@drawable/premierInn.jpg";
			hotelName.Text = selectedHotel.HotelName;
			hotelDesc.Text = selectedHotel.HotelDesc;
			hotelNumberRooms.Text = "Number of Rooms: " + selectedHotel.numberOfRooms();
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
			string responseString = "";
			var selectedBookingDate = bookingStartDatePicker.Date;
			if (selectedBookingDate != null)
			{
				Booking newBooking = createNewBooking(selectedBookingDate);
				try
				{
					var response = await BookingRestService.Instance.SaveBookingAsync(newBooking, true);
					if (response == ErrorEnum.BOOKING_FAILED)
					{
						responseString += "Error occured when creating booking. Please try again";
						responseLabel.Text = responseString;
						responseLabel.TextColor = Color.Red;
						responseLabel.IsVisible = true;
					}
					else
					{
						responseString += "Booking Successful";
						responseLabel.Text = responseString;
						responseLabel.TextColor = Color.Green;
						responseLabel.IsVisible = true;
					}
				}
				catch (Exception ex)
				{
					await DisplayAlert("Error","Error occured when attempting to create your booking.\nPlease try again", "OK");
				}
			}
		}

		private void BookingStartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
		{
			if (daysPicker.SelectedIndex != -1)
			{
				var selectedBookingDate = bookingStartDatePicker.Date;
				Booking testBooking = createNewBooking(selectedBookingDate);
				checkForRooms(testBooking);
			}
		}

		private void DaysPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bookingStartDatePicker.Date != null)
			{
				var selectedBookingDate = bookingStartDatePicker.Date;
				Booking testBooking = createNewBooking(selectedBookingDate);
				checkForRooms(testBooking);
			}
		}

		async void checkForRooms(Booking testBooking)
		{
			bool roomsAvailable = await HotelRestService.Instance.CheckRoomsForDates(testBooking);
			if (roomsAvailable)
			{
				AvailableLabel.Text = "Looks Okay";
				AvailableLabel.TextColor = Color.Green;
			}
			else
			{
				AvailableLabel.Text = "This hotel is fully booked on these dates";
				AvailableLabel.TextColor = Color.Red;
			}
		}

		private Booking createNewBooking(DateTime selectedBookingDate)
		{
			var newBooking = new Booking
			{
				BookingID = 0,
				Customer = Globals.loggedInCustomer,
				Hotel = selectedHotel,
				BookedRoom = new Room { RoomID = 1 }, //Change to available room when logic is present
				DateBookingMade = DateTime.Now,
				DateOfBooking = selectedBookingDate,
				BookingFinishDate = selectedBookingDate.AddDays(Convert.ToInt32(daysPicker.SelectedItem.ToString())),
				Activated = false,
				HideBooking = false
			};
			return newBooking;
		}
	}
}