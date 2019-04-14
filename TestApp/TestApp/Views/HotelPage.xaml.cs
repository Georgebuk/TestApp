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

namespace TestApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HotelPage : ContentPage
	{
        HotelViewModel hm;
		public HotelPage ()
		{
			InitializeComponent ();
            hm = new HotelViewModel();
            BindingContext = hm;
		}

        private void HotelListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            loadHotelPage((Hotel)e.Item);
        }

        private async void loadHotelPage(Hotel h)
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
    }
}