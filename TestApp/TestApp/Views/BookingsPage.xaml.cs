using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TestApp.Pages;
using TestApp.ViewModel;
using Xamarin.Forms;

namespace TestApp
{
    public partial class BookingsPage : ContentPage
    {
        BookingViewModel bm;

        
        public BookingsPage()
        {
            InitializeComponent();
            bm = BookingViewModel.Instance;
            BindingContext = bm;
        }

        private void BookingListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                Booking b = (Booking) e.Item;
                try
                {
                    //If the booking has expired do not allow to the user to open the booking
                    if(!b.Completed)
                        LoadBookingPage(b);
                }
                catch (Exception ex)
                {
                    string meme = ex.ToString();
                }
            }
        }

        private async void LoadBookingPage(Booking b)
        {
            var selectedBookingPage = new NavigationPage(new SelectedBookingPage(b));
            await Navigation.PushAsync(selectedBookingPage);
        }

        private void BookingsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                string filter = bookingsSearch.Text;
                bm.FilterBookings(filter);
            });
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            RefreshButton.IsVisible = false;
            RefreshActivity.IsVisible = true;
            RefreshActivity.IsRunning = true;
            bm.RefreshBookings();
            //Also refresh hotels so the user doesnt have to refresh twice
            HotelViewModel.Instance.RefreshHotels();
            RefreshButton.IsVisible = true;
            RefreshActivity.IsVisible = false;
            RefreshActivity.IsRunning = false;
            

        }
    }
}
