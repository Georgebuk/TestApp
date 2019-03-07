using HotelClassLibrary;
using System.Drawing;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace TestApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedBookingPage : ContentPage
	{
        Booking ThisBooking;
		public SelectedBookingPage (Booking booking)
		{
			InitializeComponent ();
            ThisBooking = booking;

            //BarcodeWriter<byte[]> writer = new BarcodeWriter<byte[]>()
            //{
            //    Format = BarcodeFormat.QR_CODE,
            //    Options = new ZXing.Common.EncodingOptions
            //    {
            //        Height = 200,
            //        Width = 200
            //    }
            //};
            //byte[] barcode = writer.Write("teststring");
            //ImageSource imageSource = ImageSource.FromStream(() => {
            //    MemoryStream ms = new MemoryStream(barcode);
            //    ms.Position = 0;
            //    return ms;
            //});

            //BookingQR.Source = imageSource;
            //MemoryStream stream = new MemoryStream();
            //qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //byte[] meme = stream.ToArray();
            //BookingQR.Source = ImageSource.FromStream(() => new MemoryStream(meme));
        }
	}
}