using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp
{
    //This class will be replaced with the properties dictionary https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/application-class
    public class Globals
    {
        public static Customer loggedInCustomer
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("loggedInCustomer"))
                    return JsonConvert.DeserializeObject<Customer>(Application.Current.Properties["loggedInCustomer"].ToString());
                else
                    return null;
            }
            set
            {
                if (value == null)
                    Application.Current.Properties.Remove("loggedInCustomer");
                else
                    Application.Current.Properties["loggedInCustomer"] = JsonConvert.SerializeObject(value);
            }
        }
        public static string WEBAPIURI = "http://192.168.0.24:45455/api/";
    }
}
