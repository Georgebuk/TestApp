using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TestApp.Web_Service;
using Xamarin.Forms;

namespace TestApp.ViewModel
{
    public class HotelViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Hotel> unfilteredHotels = new ObservableCollection<Hotel>();
        private ObservableCollection<Hotel> hotels = new ObservableCollection<Hotel>();

        public ObservableCollection<Hotel> Hotels
        {
            get { return hotels; }
            set
            {
                hotels = value;
                if (value.Count > 0)
                {
                    NoHotels_IsVisible = false;
                    Search_IsVisible = true;
                    FilterLabel_IsVisible = false;
                }
                else
                {
                    if (unfilteredHotels.Count == 0)
                    {
                        NoHotels_IsVisible = true;
                        Search_IsVisible = false;
                        FilterLabel_IsVisible = false;
                    }
                    else
                    {
                        NoHotels_IsVisible = false;
                        Search_IsVisible = true;
                        FilterLabel_IsVisible = true;
                    }
                }
                OnPropertyChanged(nameof(Hotels));
            }
        }

        //When is refreshing is true the refresh animation continues to trigger
        //When isRefreshing is false the animation stops
        private bool _isRefreshing = false;

        private bool _noHotels_IsVisible;
        private bool _search_IsVisible;
        private bool _filterLabel_IsVisible;

        public bool NoHotels_IsVisible
        {
            get
            {
                return _noHotels_IsVisible;
            }
            set
            {
                _noHotels_IsVisible = value;
                OnPropertyChanged(nameof(NoHotels_IsVisible));
            }
        }

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

        //Notifies the view when a property is updated
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }

        public HotelViewModel()
        {
            Hotels = new ObservableCollection<Hotel>();
            GetHotels();
        }

        public void FilterHotels(string filter)
        {
            List<Hotel> filteredHotels =
                unfilteredHotels.Where(w => w.City.Contains(filter)
                || w.HotelName.Contains(filter) 
                || w.HotelPostcode.Contains(filter)
                || w.AddressLine1.Contains(filter)
                || w.AddressLine2.Contains(filter)).ToList();
            ObservableCollection<Hotel> hotelF = new ObservableCollection<Hotel>();
            foreach (Hotel h in filteredHotels)
                hotelF.Add(h);

            Hotels = hotelF;
        }

        private async void GetHotels()
        {
            HotelRestService service = HotelRestService.Instance;
            Hotels.Clear();
            var hotels = await service.RefreshDataAsync();
            ObservableCollection<Hotel> hts = new ObservableCollection<Hotel>();

            foreach (Hotel h in hotels)
                hts.Add(h);
            unfilteredHotels = hts;
            Hotels = hts;
        }

        //Command to bind refreshing behaviour from view to view model
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true; //Start refreshing animation

                    GetHotels(); //Get bookings for user

                    IsRefreshing = false; //Stop refreshing animation
                });
            }
        }
    }
}
