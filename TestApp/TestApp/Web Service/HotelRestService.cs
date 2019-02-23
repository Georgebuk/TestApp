using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        private static HotelRestService instance;

        public static HotelRestService Instance
        {
            get
            {
                if (instance == null)
                    instance = new HotelRestService();
                return instance;
            }
        }

        private HotelRestService()
        {
            Client = new HttpClient();
            Client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Hotel>> RefreshDataAsync()
        {
            //Url = http://192.168.0.24:57162/api/hotel

            string bookingAPIURI = Globals.WEBAPIURI + "Hotel";
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
                }
            }
            catch (Exception ex)
            {
                string meme = ex.ToString();
            }
            return hotels;
        }

        public async Task<bool> CheckRoomsForDates(Booking booking)
        {
            string checkRoomAPI = Globals.WEBAPIURI + "hotel/CheckAvailable/{0}";
            var json = JsonConvert.SerializeObject(booking);
            bool roomAvailable = true;
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(checkRoomAPI, content);
                if (response.IsSuccessStatusCode)
                {
                    var available = await response.Content.ReadAsStringAsync();
                    if (available.Contains("False"))
                        roomAvailable = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR OCCURED WHEN CHECKING IF ROOM AVAILABLE: {0}", ex.Message);
            }
            return roomAvailable;
        }

        public Task SaveBookingAsync(Booking item, bool isNewItem)
        {
            throw new NotImplementedException();
        }
    }
}
