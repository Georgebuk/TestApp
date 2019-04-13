using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TestApp.Pages;
using Xamarin.Forms;

namespace TestApp
{
    public partial class BookingsPage : ContentPage
    {
        BookingViewModel bm;

        
        public BookingsPage()
        {
            InitializeComponent();
            bm = new BookingViewModel();
            BindingContext = bm;
        }

        private void BookingListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                try
                {
                    LoadBookingPage((Booking) e.Item);
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
    }
}
