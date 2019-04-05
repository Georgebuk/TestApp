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

            //um = new UserViewModel();
            //BindingContext = um;

			//accountLabel.Text =  "Logged in with" + Globals.loggedInCustomer.Email;

			//var tapGestureRecognister = new TapGestureRecognizer();
			//tapGestureRecognister.Tapped += (s, e) =>
			//{
			//	//Simulated logging out, will change when accounts are implemented
			//	DisplayAlert("Alert","logged out", "OK");
			//};
			//notYouLabel.GestureRecognizers.Add(tapGestureRecognister);
		}

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            Customer c = await UserRestService.Instance.GetUser("george.boulton@hotmail.co.uk", "meme");
            Application.Current.Properties["LoggedInUser"] = JsonConvert.SerializeObject(c);
        }
    }
}