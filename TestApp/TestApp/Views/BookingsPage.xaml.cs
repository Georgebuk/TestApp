using Newtonsoft.Json;
using System;
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
                    //Start a new activity natively in Android passing in the selected booking
                    //The booking info will be used to populate the new activity
                    DependencyService.Register<IViewBarcodePage>();
                    string bookingjson = JsonConvert.SerializeObject(e.Item);
                    DependencyService.Get<IViewBarcodePage>().StartNativeIntentOrActivity(bookingjson);
                }
                catch (Exception ex)
                {
                    string meme = ex.ToString();
                }
            }
        }

        private void BookingsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
