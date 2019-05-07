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
	public partial class HotelPage : ContentPage
	{
        HotelViewModel hm;
		public HotelPage ()
		{
			InitializeComponent ();
            hm = HotelViewModel.Instance;
            BindingContext = hm;
		}

        private void HotelListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            LoadHotelPage((Hotel)e.Item);
        }

        private async void LoadHotelPage(Hotel h)
        {
            var SelectedHotelPage = new NavigationPage(new SelectedHotelPage(h));
            await Navigation.PushAsync(SelectedHotelPage);
        }

        private void HotelsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                string filter = hotelsSearch.Text;
                hm.FilterHotels(filter);
            });
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            RefreshButton.IsVisible = false;
            RefreshActivity.IsVisible = true;
            RefreshActivity.IsRunning = true;
            hm.RefreshHotels();
            //Also refresh bookings so the user doesnt have to refresh twice
            BookingViewModel.Instance.RefreshBookings();
            RefreshButton.IsVisible = true;
            RefreshActivity.IsVisible = false;
            RefreshActivity.IsRunning = false;
        }
    }
}