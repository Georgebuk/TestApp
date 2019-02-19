using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class BookingRestService
    {
        HttpClient Client;
        public List<Booking> Bookings { get; private set; }
        private static BookingRestService instance;

        public static BookingRestService Instance
        {
            get
            {
                if (instance == null)
                    instance = new BookingRestService();
                return instance;
            }
        }


        private BookingRestService()
        {
            Client = new HttpClient();
            Client.MaxResponseContentBufferSize = 256000;
            
        }
        public async Task<List<Booking>> RefreshDataAsync()
        {
            //Url = http://192.168.0.24:57162/api/booking
            string bookingAPIURI = "http://192.168.0.24:57162/api/booking/{0}";
            bookingAPIURI = string.Format(bookingAPIURI, Globals.loggedInCustomer.CustId);
            var uri = new Uri(bookingAPIURI);

            List<Booking> bookings = new List<Booking>();
            try
            {
                var response = await Client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    content = content.Replace("\\", string.Empty);
                    content = content.Trim('"');
                    bookings = JsonConvert.DeserializeObject<List<Booking>>(content);
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return bookings;
        }

        public async Task SaveBookingAsync(Booking item, bool isNewItem)
        {
            string bookingAPIURI = "http://192.168.0.24:57162/api/booking";
            var Uri = new Uri(bookingAPIURI);

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                    response = await Client.PostAsync(Uri, content);
                else
                    response = await Client.PutAsync(Uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("Item successfully saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR OCCURED WHEN SAVING BOOKING: {0}", ex.Message);
            }
        }
    }
}
