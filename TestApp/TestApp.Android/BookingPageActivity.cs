using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestApp.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(BookingPageActivity))]
namespace TestApp.Droid
{
    [Activity(Label = "BookingPageActivity")]
    public class BookingPageActivity : IViewBarcodePage
    {
        public void StartNativeIntentOrActivity(string bookingJSON)
        {
            var intent = new Intent(Android.App.Application.Context, typeof(SelectedBookingPage));
            intent.PutExtra("BookingJSON", bookingJSON);
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}