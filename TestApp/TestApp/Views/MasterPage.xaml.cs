using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestApp.Pages;
using TestApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
		public MasterPage ()
		{
			InitializeComponent ();

            try
            {
                List<MasterPageItem> items = new List<MasterPageItem>();
                //Application.Current.Properties["loggedInCustomer"] = JsonConvert.SerializeObject(Globals.loggedInCustomer);
                if (Globals.loggedInCustomer != null)
                {
                    items.Add(new MasterPageItem { Title = "Home", IconSource = "settingscog.png", TargetType = typeof(NewMainPage) });
                    items.Add(new MasterPageItem { Title = "Bookings", IconSource = "booking.png", TargetType = typeof(BookingsPage) });                  
                    items.Add(new MasterPageItem { Title = "Account Settings", IconSource = "accountcirclenopadding.png", TargetType = typeof(AccountSettings) });
                    items.Add(new MasterPageItem { Title = "Search for Hotel", IconSource = "searchicon.png", TargetType = typeof(HotelPage) });
                }
                else
                {
                    items.Add(new MasterPageItem { Title = "Login", IconSource = "key.png", TargetType = typeof(LoginPage) });
                    items.Add(new MasterPageItem { Title = "Register", IconSource = "accountcirclenopadding.png", TargetType = typeof(RegistrationPage) });
                }

                listView.ItemsSource = items;
            }
            catch (Exception e)
            {
                string ex = e.ToString();
            }
        }
	}
}