using HotelClassLibrary;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Customer c = new Customer
            {
                CustId = 1,
                First_name = "George",
                Last_name = "Boulton",
                Email = "george.boulton@hotmail.co.uk",
                Password = "LHvGkIp870LnugAwmLYbeJgvbIAD8+kyZZkTJR4QIIPUWQ9j",
                Phone_number = "07411762329",
                Bookings = new List<Booking>()
            };
            Globals.loggedInCustomer = c;

            if(Globals.loggedInCustomer != null)
                MainPage = new BottomTabsPage();
            else
            {
                MainPage = new LoginRegisterTabbedPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
