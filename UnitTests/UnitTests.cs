
using HotelClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        static Customer TestCustomer;
        //Destroy then reinitiate the database in the web service
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            TestCustomer = new Customer {
                CustId = 1,
                First_name = "George",
                Last_name = "Boulton",
                Email = "george.boulton@hotmail.co.uk",
                Password = "LHvGkIp870LnugAwmLYbeJgvbIAD8+kyZZkTJR4QIIPUWQ9j",
                Phone_number = "07411762329",
                Bookings = new List<Booking>()
            };
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
            Assert.AreEqual("Hotel", hotel1.HotelName);
            Hotel hotel2 = hotels[1];
            Assert.AreEqual(2, hotel2.HotelID);
            Assert.AreEqual("A Different Hotel", hotel2.HotelName);
        }

        [TestMethod]
        public async Task BookingGetBookingsForUserTest()
        {
            
            var bookings = await BookingRestService.Instance.RefreshDataAsync(TestCustomer.CustId);
            Booking booking1 = bookings[0];
            Assert.AreEqual(1, booking1.BookedRoom.RoomID);
            Assert.AreEqual("Hotel", booking1.Hotel.HotelName);
            Booking booking2 = bookings[1];
            Assert.AreEqual(2, booking2.BookedRoom.RoomID);
            Assert.AreEqual("Hotel", booking2.Hotel.HotelName);
        }

        [TestMethod]
        public async Task AddBookingForUserTest()
        {
            DateTime date = DateTime.Today;
            //Create test booking
            Booking booking = new Booking
            {
                Customer = TestCustomer,
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
            var bookings = await BookingRestService.Instance.RefreshDataAsync(TestCustomer.CustId);
            //Check if found booking is equal

            Assert.AreEqual(date, bookings[3].DateBookingMade);
            Assert.AreEqual(1, bookings[3].Hotel.HotelID);
            Assert.AreEqual(date.AddDays(5), bookings[3].BookingFinishDate);
        }

        [TestMethod]
        public async Task getUserTest()
        {
            //Attempt to log in with wrong password
            Customer c = await UserRestService.Instance.GetUser(TestCustomer.Email, "notthepassword");
            //ID will be 0 if login is denied
            Assert.AreEqual(0, c.CustId);

            //Attempt to log in with correct details
            c = await UserRestService.Instance.GetUser(TestCustomer.Email, "meme");
            Assert.AreNotEqual(null, c);
            Assert.AreEqual(1, c.CustId);
            Assert.AreEqual(TestCustomer.First_name, c.First_name);
            Assert.AreEqual(TestCustomer.Last_name, c.Last_name );
            Assert.AreEqual(TestCustomer.Phone_number, c.Phone_number);
        }

        [TestMethod]
        public async Task CreateNewUserTest()
        {
            Customer c = new Customer
            {
                First_name = "Test",
                Last_name = "Testington",
                Email = "test.test@test.com",
                Password = "Passw0rd",
                Phone_number = "07511732329",
                dateOfBirth = new DateTime(1990, 8, 21)
            };

            await UserRestService.Instance.SaveUserAsync(c);
            Customer c2 = await UserRestService.Instance.GetUser("test.test@test.com", "Passw0rd");
            Assert.AreNotEqual(null, c2);
            Assert.AreEqual("Test", c2.First_name);
            Assert.AreEqual("Testington", c2.Last_name);
            Assert.AreEqual("07511732329", c2.Phone_number);
        }
    }
}
