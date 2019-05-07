using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TestApp.Web_Service;
using Xamarin.Forms;

namespace TestApp.ViewModel
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Booking> unfilteredCollection = new ObservableCollection<Booking>();
        ObservableCollection<Booking> booking = new ObservableCollection<Booking>();

        private static BookingViewModel _instance;
        //Binded to the listview, when bookings is set depending on the 
        //value the UI elements that are visible are set
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
                    FilterLabel_IsVisible = false;
                }
                else
                {
                    if (unfilteredCollection.Count == 0)
                    {
                        Search_IsVisible = false;
                        HideLabel_IsVisible = true;
                        FilterLabel_IsVisible = false;
                    }
                    else
                    {
                        Search_IsVisible = true;
                        HideLabel_IsVisible = false;
                        FilterLabel_IsVisible = true;
                    }
                }
                OnPropertyChanged(nameof(Bookings));
            }
        }
        //Filters the bookings collection based on string typed in search bar
        public void FilterBookings(string filter)
        {
            List<Booking> filteredBookings =
                unfilteredCollection.Where(w => w.Hotel.HotelName.Contains(filter)
                || w.DateOfBookingAsString.Contains(filter)
                || w.RoomName.Contains(filter)
                || w.Hotel.HotelPostcode.Contains(filter)
                || w.Hotel.AddressLine1.Contains(filter)
                || w.Hotel.AddressLine2.Contains(filter)
                || w.Hotel.City.Contains(filter)).ToList();
            ObservableCollection<Booking> bookingF = new ObservableCollection<Booking>();
            foreach (Booking b in filteredBookings)
            {
                bookingF.Add(b);
            }
            Bookings = bookingF;

        }

        //When is refreshing is true the refresh animation continues to trigger
        //When isRefreshing is false the animation stops
        private bool _isRefreshing = false;

        //Parameters that hold if UI elemts are visible
        private bool _hideBooking_IsVisible;
        private bool _search_IsVisible;
        private bool _filterLabel_IsVisible;
        //Returns whether the "no bookings found with search" label is visible
        public bool FilterLabel_IsVisible
        {
            get
            {
                return _filterLabel_IsVisible;
            }
            set
            {
                _filterLabel_IsVisible = value;
                OnPropertyChanged(nameof(FilterLabel_IsVisible));
            }
        }
        //Returns whether the listview is visible
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
        //Returns whether the user has no bookings label is visible
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

        public static BookingViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BookingViewModel();
                return _instance;
            }
        }

        private BookingViewModel()
        {
            Bookings = new ObservableCollection<Booking>();
            GetBookings();
        }

        //Calls the web servive to get a list of all bookings for loggin in customer
        private async void GetBookings()
        {
            try
            {
                BookingRestService service = BookingRestService.Instance;
                Bookings.Clear();
                var bookings = await service.RefreshDataAsync(Globals.loggedInCustomer.CustId);

                //Order by date
                bookings = bookings.OrderByDescending(o => o.DateOfBooking).ToList();
                ObservableCollection<Booking> book = new ObservableCollection<Booking>();
                foreach (Booking b in bookings)
                    book.Add(b);

                unfilteredCollection = book;
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

                    GetBookings(); //Get bookings for user

                    IsRefreshing = false; //Stop refreshing animation
                });
            }
        }

        public void RefreshBookings()
        {
            IsRefreshing = true;
            GetBookings();
            IsRefreshing = false;
        }

        public void updateBookings(string filter)
        {

        }
    }
}
