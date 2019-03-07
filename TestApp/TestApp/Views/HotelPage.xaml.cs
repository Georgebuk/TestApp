using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Web_Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HotelPage : ContentPage
	{
		public HotelPage ()
		{
			InitializeComponent ();
            populateHotels();
		}

        async void populateHotels()
        {
            HotelRestService service = HotelRestService.Instance;
            List<Hotel> hotels = await service.RefreshDataAsync();
            foreach (Hotel h in hotels)
            {
                HotelCustomControl hcl = new HotelCustomControl
                {
                    CityLabelText = h.City,
                    HotelLabelText = h.HotelName,
                    HotelImage = "@drawable/premierInn.jpg",
                    ThisHotel = h
                    
                };
                hotelStack.Children.Add(hcl);
            }
        }
	}
}