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
        public async Task<string[]> RefreshDataAsync()
        {
            //Url = http://192.168.0.24:57162/api/values
            var uri = new Uri("http://192.168.0.24:57162/api/values");
            string[] strings = {"",""};
            try
            {
                var response = await Client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    strings = JsonConvert.DeserializeObject<string[]>(content);
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return strings;
        }

        public Task SaveBookingAsync(Booking item, bool isNewItem)
        {
            throw new NotImplementedException();
        }
    }
}
