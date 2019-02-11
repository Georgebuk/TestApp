using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class RestService : IRestService
    {
        HttpClient Client;
        public List<Booking> Bookings { get; private set; }

        public RestService()
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

        public Task SaveBookingAsync(Booking item, bool isNewItem)
        {
            throw new NotImplementedException();
        }
    }
}
