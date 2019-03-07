using HotelClassLibrary;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestApp
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Booking> Bookings { get; set; }

        //When is refreshing is true the refresh animation continues to trigger
        //When isRefreshing is false the animation stops
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        //Notifies the view when a property is updated
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
  
        public BookingViewModel()
        {
            Bookings = new ObservableCollection<Booking>();
            getBookings();
        }

        //Calls the web servive to get a list of all bookings for loggin in customer
        private async void getBookings()
        {
            BookingRestService service = BookingRestService.Instance;
            Bookings.Clear();
            var bookings = await service.RefreshDataAsync();
            foreach (Booking b in bookings)
                Bookings.Add(b);
        }
        //Command to bind refreshing behaviour from view to view model
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true; //Start refreshing animation

                    getBookings(); //Get bookings for user

                    IsRefreshing = false; //Stop refreshing animation
                });
            }
        }
    }
}
