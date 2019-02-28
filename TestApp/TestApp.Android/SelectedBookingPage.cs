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
using ZXing;


namespace TestApp.Droid
{
    [Activity(Label = "SelectedBookingPage")]
    public class SelectedBookingPage : Activity
    {
        Fragment[] _fragments;
        static readonly string Tag = "ActionBarTabsSupport";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                SetContentView(Resource.Layout.SelectedBooking);
            }
            catch (Exception ex)
            { }

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.SelectedBooking);

            _fragments = new Fragment[]
            {
                new BookingInformationFragment(),
                new QRCodeFragment()
            };

            AddTabToActionBar(Resource.String.booking_information_label, Resource.Drawable.booking);
            AddTabToActionBar(Resource.String.qrcode_label, Resource.Drawable.accountcircle);

            //BarcodeWriter writer = new BarcodeWriter()
            //{
            //    Format = BarcodeFormat.QR_CODE,
            //    Options = new ZXing.Common.EncodingOptions
            //    {
            //        Height = 200,
            //        Width = 200
            //    }
            //};
            //ImageView image = FindViewById<ImageView>(Resource.Id.imageView1);
            //var result = writer.Write("TestTestTESTtest");



            //image.SetImageBitmap(result);

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