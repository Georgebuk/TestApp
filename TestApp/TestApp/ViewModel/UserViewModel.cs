using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.ViewModel
{
    public class UserViewModel
    {
        public Customer LoggedInCustomer = null;

        private bool _loginIsVisible;
        private bool _accountIsVisible;

        //LoginIdVisible and AccountIsVisible determine which page to show
        //on the AccountSettings page
        public bool LoginIsVisible
        {
            get
            {
                if (LoggedInCustomer == null)
                    return true;
                else
                    return false;
            }
        }

        public bool AccountIsVisible
        {
            get
            {
                if (LoggedInCustomer == null)
                    return false;
                else
                    return true;
            }
        }

        public UserViewModel()
        {
            if(Globals.loggedInCustomer != null)
                LoggedInCustomer = JsonConvert.DeserializeObject<Customer>(Application.Current.Properties["loggedInUser"].ToString());
        }
    }
}
