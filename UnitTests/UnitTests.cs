using HotelClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestApp;
using TestApp.Web_Service;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        static HttpClient Client = new HttpClient();

        //Destroy then reinitiate the database in the web service
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            string setupURI = Globals.WEBAPIURI + "Setup/setupDB";
            var uri = new Uri(setupURI);
            try
            {
                var response = Client.GetAsync(uri);
            }
            catch (Exception e)
            {

            }
        }

        [TestMethod]
        public async Task HotelGetHotelsTest()
        {
            var hotels = await HotelRestService.Instance.RefreshDataAsync();
            Assert.AreEqual(2, hotels.Count);
            Hotel hotel1 = hotels[0];
            Assert.AreEqual(1, hotel1.HotelID);
            Assert.AreEqual("Premier Inn", hotel1.HotelName);
            Hotel hotel2 = hotels[1];
            Assert.AreEqual(2, hotel2.HotelID);
            Assert.AreEqual("A Different Premier Inn", hotel2.HotelName);
        }

        [TestMethod]
        public async Task BookingGetBookingsForUserTest()
        {
            var bookings = await BookingRestService.Instance.RefreshDataAsync();
            Booking booking1 = bookings[0];
            Assert.AreEqual(1, booking1.BookedRoom.RoomID);
            Assert.AreEqual("Premier Inn", booking1.Hotel.HotelName);
            Booking booking2 = bookings[1];
            Assert.AreEqual(2, booking2.BookedRoom.RoomID);
            Assert.AreEqual("Premier Inn", booking2.Hotel.HotelName);
        }

        [TestMethod]
        public async Task AddBookingForUserTest()
        {
            DateTime date = DateTime.Today;
            //Create test booking
            Booking booking = new Booking
            {
                Customer = Globals.loggedInCustomer,
                Hotel = new Hotel { HotelID = 1 },
                DateBookingMade = date,
                DateOfBooking = date,
                BookingFinishDate = date.AddDays(5),
                Activated = false,
                HideBooking = false,
                QrcodeString = "a92a5376-1dbb-4135-9db8-01e54b3e673d"
            };

            //Save the booking
            await BookingRestService.Instance.SaveBookingAsync(booking, true);
            //Refresh booking data
            var bookings = await BookingRestService.Instance.RefreshDataAsync();
            //Check if found booking is equal

            Assert.AreEqual(date, bookings[2].DateBookingMade);
            Assert.AreEqual(1, bookings[2].Hotel.HotelID);
            Assert.AreEqual(date.AddDays(5), bookings[2].BookingFinishDate);
        }
    }
}
