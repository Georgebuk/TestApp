using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TestApp.Web_Service;
using Xamarin.Forms;

namespace TestApp.ViewModel
{
    public class HotelViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Hotel> Hotels { get; set; }

        //When is refreshing is true the refresh animation continues to trigger
        //When isRefreshing is false the animation stops
        private bool _isRefreshing = false;

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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public HotelViewModel()
        {
            Hotels = new ObservableCollection<Hotel>();
            getHotels();
        }

        private async void getHotels()
        {
            HotelRestService service = HotelRestService.Instance;
            Hotels.Clear();
            var hotels = await service.RefreshDataAsync();
            foreach (Hotel h in hotels)
                Hotels.Add(h);
        }

        //Command to bind refreshing behaviour from view to view model
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true; //Start refreshing animation

                    getHotels(); //Get bookings for user

                    IsRefreshing = false; //Stop refreshing animation
                });
            }
        }
    }
}
