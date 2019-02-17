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
		public SelectedHotelPage()
		{
			InitializeComponent();
		}
		public SelectedHotelPage (Hotel selectedHotel)
		{
			InitializeComponent ();
            hotelImage.Source = "@drawable/premierInn.jpg";
            hotelName.Text = selectedHotel.HotelName;
            hotelDesc.Text = selectedHotel.HotelDesc;
            hotelNumberRooms.Text = "Number of Rooms: " + selectedHotel.numberOfRooms();
		}
	}
}