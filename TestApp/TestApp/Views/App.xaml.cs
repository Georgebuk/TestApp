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

            if (Globals.loggedInCustomer != null)
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
