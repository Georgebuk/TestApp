using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSettings : ContentPage
	{
		public AccountSettings ()
		{
			InitializeComponent ();

			accountLabel.Text =  "Logged in with" + Globals.loggedInCustomer.Email;

			var tapGestureRecognister = new TapGestureRecognizer();
			tapGestureRecognister.Tapped += (s, e) =>
			{
				//Simulated logging out, will change when accounts are implemented
				DisplayAlert("Alert","logged out", "OK");
			};
			notYouLabel.GestureRecognizers.Add(tapGestureRecognister);
		}

		
	}
}