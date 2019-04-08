using HotelClassLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestApp
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Booking> booking = new ObservableCollection<Booking>();
        public ObservableCollection<Booking> Bookings
        {
            get { return booking; }
            set
            {
                booking = value;
                if (value.Count > 0)
                {
                    Search_IsVisible = true;
                    HideLabel_IsVisible = false;
                }
                else
                {
                    Search_IsVisible = false;
                    HideLabel_IsVisible = true;
                }
                OnPropertyChanged(nameof(Bookings));
            }
        }

        //When is refreshing is true the refresh animation continues to trigger
        //When isRefreshing is false the animation stops
        private bool _isRefreshing = false;

        private bool _hideBooking_IsVisible;
        private bool _search_IsVisible;

        public bool Search_IsVisible
        {
            get
            {
                return _search_IsVisible;
            }
            set
            {
                _search_IsVisible = value;
                OnPropertyChanged(nameof(Search_IsVisible));
            }
        }

        public bool HideLabel_IsVisible
        {
            get
            {
                return _hideBooking_IsVisible;
            }
            set
            {
                _hideBooking_IsVisible = value;
                OnPropertyChanged(nameof(HideLabel_IsVisible));
            }
        }

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
            this.PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }

        public BookingViewModel()
        {
            Bookings = new ObservableCollection<Booking>();
            getBookings();
        }

        //Calls the web servive to get a list of all bookings for loggin in customer
        private async void getBookings()
        {
            try
            {
                BookingRestService service = BookingRestService.Instance;
                Bookings.Clear();
                var bookings = await service.RefreshDataAsync(Globals.loggedInCustomer.CustId);
                ObservableCollection<Booking> book = new ObservableCollection<Booking>();
                foreach (Booking b in bookings)
                    book.Add(b);

                Bookings = book;
            }
            catch (Exception e)
            {

            }
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

        public void updateBookings(string filter)
        {
            
        }
    }
}
