using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HotelCustomControl : ContentView
	{
        public static readonly BindableProperty HotelLabelProperty =
            BindableProperty.Create("HotelLabelText", typeof(string), typeof(HotelCustomControl), default(string));
        public string HotelLabelText
        {
            get { return (string)GetValue(HotelLabelProperty); }
            set { SetValue(HotelLabelProperty, value); }
        }

        public static readonly BindableProperty CityLabelProperty =
            BindableProperty.Create("CityLabelText", typeof(string), typeof(HotelCustomControl), default(string));
        public string CityLabelText
        {
            get { return (string)GetValue(CityLabelProperty); }
            set { SetValue(CityLabelProperty, value); }
        }

        public static readonly BindableProperty HotelImageProperty =
            BindableProperty.Create("HotelImage", typeof(string), typeof(HotelCustomControl), default(string));
        public string HotelImage
        {
            get{ return (string)GetValue(HotelImageProperty); }
            set{ SetValue(HotelImageProperty, value); }
        }
        public HotelCustomControl ()
		{
			InitializeComponent ();

            hotelCity.SetBinding(Label.TextProperty, new Binding("CityLabelText", source: this));
            hotelName.SetBinding(Label.TextProperty, new Binding("HotelLabelText", source: this));
            hotelImage.SetBinding(Image.SourceProperty, new Binding("HotelImage", source: this));

            //Register a click event for each control
            var tapGestureRecognister = new TapGestureRecognizer();
            tapGestureRecognister.Tapped += (s, e) =>
            {
              
            };
            hotelFrame.GestureRecognizers.Add(tapGestureRecognister);
        }
	}
}