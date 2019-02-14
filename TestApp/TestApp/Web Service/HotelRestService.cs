using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelClassLibrary;
using Newtonsoft.Json;

namespace TestApp.Web_Service
{
    public class HotelRestService
    {
        HttpClient Client;
        public HotelRestService()
        {
            Client = new HttpClient();
            Client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Hotel>> RefreshDataAsync()
        {
            //Url = http://192.168.0.24:57162/api/hotel

            string bookingAPIURI = "http://192.168.0.24:57162/api/Hotel";
            var uri = new Uri(bookingAPIURI);

            List<Hotel> hotels = new List<Hotel>();
            try
            {
                var response = await Client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    content = content.Replace("\\", string.Empty);
                    content = content.Trim('"');
                    hotels = JsonConvert.DeserializeObject<List<Hotel>>(content);
                    string meme = "";
                }
            }
            catch (Exception ex)
            {
                string meme = ex.ToString();
            }
            return hotels;
        }

        public Task SaveBookingAsync(Booking item, bool isNewItem)
        {
            throw new NotImplementedException();
        }
    }
}
