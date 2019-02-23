using HotelClassLibrary;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Web_Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

		private async void test()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
			Debug.WriteLine("Position Status: {0}", position.Timestamp);
			Debug.WriteLine("Position Latitude: {0}", position.Latitude);
			Debug.WriteLine("Position Longitude: {0}", position.Longitude);
		}

		private void Test_Clicked(object sender, EventArgs e)
		{
            test2();
		}

        private async void test2()
        {
            HotelRestService service = HotelRestService.Instance;
            List<Hotel> hotels = await service.RefreshDataAsync();
            string meme = "";
        }
	}
}