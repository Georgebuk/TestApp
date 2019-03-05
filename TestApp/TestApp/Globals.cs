using HotelClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    //This class will be replaced with the properties dictionary https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/application-class
    public class Globals
    {
        public static Customer loggedInCustomer = new Customer();
        public static string WEBAPIURI = "http://192.168.0.24:45455/api/";
    }
}
