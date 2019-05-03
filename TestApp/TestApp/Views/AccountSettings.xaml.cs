using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.ViewModel;
using TestApp.Web_Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSettings : ContentPage
	{
        //UserViewModel um;
		public AccountSettings ()
		{
			InitializeComponent ();

			accountLabel.Text =  "Logged in as: " + Globals.loggedInCustomer.Email;
		}

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            if (Globals.loggedInCustomer != null)
            {
                Globals.loggedInCustomer = null;
                Application.Current.MainPage = new LoginRegisterTabbedPage();
            }

        }
    }
}