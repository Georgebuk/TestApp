using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using HotelClassLibrary;
using Newtonsoft.Json;
using ZXing;


namespace TestApp.Droid
{
    [Activity(Label = "SelectedBookingPage")]
    public class SelectedBookingPage : Activity
    {
        Fragment[] _fragments;
        Booking ThisBooking;
        static readonly string Tag = "ActionBarTabsSupport";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            
            SetContentView(Resource.Layout.SelectedBooking);

            ThisBooking = JsonConvert.DeserializeObject<Booking>(Intent.GetStringExtra("BookingJSON"));

            _fragments = new Fragment[]
            {
                new BookingInformationFragment(),
                new QRCodeFragment(ThisBooking)
            };

            AddTabToActionBar(Resource.String.booking_information_label, Resource.Drawable.booking);
            AddTabToActionBar(Resource.String.qrcode_label, Resource.Drawable.accountcircle);
        }

        void AddTabToActionBar(int labelResourceId, int iconResourceId)
        {
            ActionBar.Tab tab = ActionBar.NewTab()
                                         .SetText(labelResourceId)
                                         .SetIcon(iconResourceId);
            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }
    }
}