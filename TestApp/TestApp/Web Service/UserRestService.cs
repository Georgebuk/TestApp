using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Web_Service
{
    public class UserRestService
    {
        HttpClient Client;

        private static UserRestService instance;

        public static UserRestService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserRestService();
                return instance;
            }
        } 

        private UserRestService()
        {
            Client = new HttpClient();
            Client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Customer> GetUser(string email, string password)
        {
            string loginURI = Globals.WEBAPIURI + "user";
            var URI = new Uri(loginURI);
            UserDetailsAPIModel model = new UserDetailsAPIModel
            {
                Email = email,
                Password = password
            };
            var param = JsonConvert.SerializeObject(model);

            var content = new StringContent(param, Encoding.UTF8, "application/json");
            try
            {
                var response = await Client.PostAsync(URI, content);
                var jsonstring = await response.Content.ReadAsStringAsync();
                jsonstring = jsonstring.Replace("\\", string.Empty);
                jsonstring = jsonstring.Trim('"');
                return JsonConvert.DeserializeObject<Customer>(jsonstring);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR OCCURED WHEN GETTING USER: {0}", ex.Message);
            }
            return null;
        }

        public async Task<ErrorEnum> SaveUserAsync(Customer c)
        {
            string newUserURI = Globals.WEBAPIURI + "user/register";
            var URI = new Uri(newUserURI);

            var param = JsonConvert.SerializeObject(c);
            var content = new StringContent(param, Encoding.UTF8, "application/json");
            try
            {
                var response = await Client.PostAsync(URI, content);
                if(response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    jsonResponse = jsonResponse.Replace("\\", string.Empty);
                    jsonResponse = jsonResponse.Trim('"');
                    return JsonConvert.DeserializeObject<ErrorEnum>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR OCCURED ATTEMPTING TO SAVE NEW USER: {0}", ex.Message);
            }
            return ErrorEnum.CONNECTION_ERROR;
        }
    }
}
