using HotelClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Web_Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            //Notify the user that the application is loading something
            loadingActivity.IsVisible = true;
            loadingActivity.IsRunning = true;

            //Reset error labels
            errorLabelNoConnection.IsVisible = false;
            errorLabelNoUser.IsVisible = false;
            errorLabelNoEntry.IsVisible = false;

            string email = EntryEmail.Text;
            string password = EntryPassword.Text;
            //Check if user has entered data
            if (email == null || password == null)
            {
                //If no input display arror message
                errorLabelNoEntry.IsVisible = true;
            }
            else
            {
                //Look for user in web service
                Customer c = await UserRestService.Instance.GetUser(email, password);
                //If user exists
                if (c == null)
                {
                    errorLabelNoConnection.IsVisible = true;
                }
                else if (c.CustId != 0)
                {
                    //Set the logged in user
                    Globals.loggedInCustomer = c;
                    //Redirect to a new main page to re-populate side menu
                    Application.Current.MainPage = new NewMainPage();
                }
                else
                {
                    //USer is not found. Display not found error
                    errorLabelNoUser.IsVisible = true;
                }
            }

            //Disable loading notification
            loadingActivity.IsVisible = false;
            loadingActivity.IsRunning = false;
        }

    }
}