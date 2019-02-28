using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomBookingControl : ContentView
	{
        public Booking ThisBooking;
		public static readonly BindableProperty RoomLabelProperty =
			BindableProperty.Create("RoomLabelText", typeof(string), typeof(RoomBookingControl), default(string));


		public string RoomLabelText
		{
			get { return (string)GetValue(RoomLabelProperty); }
			set { SetValue(RoomLabelProperty, value); }
		}

		public static readonly BindableProperty HotelLabelProperty =
			BindableProperty.Create("HotelLabelText", typeof(string), typeof(RoomBookingControl), default(string));
		public string HotelLabelText
		{
			get { return (string)GetValue(HotelLabelProperty); }
			set { SetValue(HotelLabelProperty, value); }
		}

		public static readonly BindableProperty DateLabelProperty =
			BindableProperty.Create("DateLabelText", typeof(string), typeof(RoomBookingControl), default(string));
		public string DateLabelText
		{
			get { return (string)GetValue(DateLabelProperty); }
			set { SetValue(DateLabelProperty, value); }
		}

		public RoomBookingControl ()
		{
			InitializeComponent ();

			roomName.SetBinding(Label.TextProperty, new Binding("RoomLabelText", source: this));
			hotelName.SetBinding(Label.TextProperty, new Binding("HotelLabelText", source: this));
			dateBooked.SetBinding(Label.TextProperty, new Binding("DateLabelText", source: this));

            var tapGestureRecognister = new TapGestureRecognizer();
            tapGestureRecognister.Tapped += async (s, e) =>
            {
                DependencyService.Register<IViewBarcodePage>();
                DependencyService.Get<IViewBarcodePage>().StartNativeIntentOrActivity();
                //var SelectedBookingPage = new NavigationPage(new SelectedBookingPage(ThisBooking));
                //await Navigation.PushAsync(SelectedBookingPage);
            };
            bookingFrame.GestureRecognizers.Add(tapGestureRecognister);
        }
	}
}