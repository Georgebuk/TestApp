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
using HotelClassLibrary;
using ZXing;

namespace TestApp.Droid
{
    public class BookingInformationFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }

    public class QRCodeFragment : Fragment
    {
        Booking booking;
        
        public QRCodeFragment(Booking booking)
        {
            this.booking = booking;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //BarcodeWriter writer = new BarcodeWriter()
            //{
            //    Format = BarcodeFormat.QR_CODE,
            //    Options = new ZXing.Common.EncodingOptions
            //    {
            //        Height = 800,
            //        Width = 800
            //    }
            //};
            //var result = writer.Write(booking.QrcodeString);

            View view = inflater.Inflate(Resource.Layout.QRCodePageFragment, container, false);
            //view.FindViewById<ImageView>(Resource.Id.imageView2).SetImageBitmap(result);

            return view;
        }
    }
}