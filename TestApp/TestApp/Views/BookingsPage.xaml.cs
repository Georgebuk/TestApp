using Newtonsoft.Json;
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
                DependencyService.Register<IViewBarcodePage>();
                string bookingjson = JsonConvert.SerializeObject(e.Item);
                DependencyService.Get<IViewBarcodePage>().StartNativeIntentOrActivity(bookingjson);
            }
        }

        
    }
}
