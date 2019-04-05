using HotelClassLibrary;
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
	public partial class RegistrationPage : ContentPage
	{
        public static DateTime MaximumDate = DateTime.Today.AddYears(-18);
		public RegistrationPage ()
		{
			InitializeComponent ();
            
		}

        private void DateOfBirthPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var selectedDateOfBirth = dateOfBirthPicker.Date;
            if (DateTime.Today.Year - selectedDateOfBirth.Year < 18)
            {

            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string errormessage = "";

            string email = EntryEmail.Text;
            string password = EntryPassword.Text;
            string fName = EntryFName.Text;
            string lName = EntrySName.Text;
            string phone = EntryPhonenumber.Text;
            DateTime dob = dateOfBirthPicker.Date;
            //Make sure all entries are present
            if (email == null || password == null || fName == null
                || lName == null || phone == null)
            {
                errormessage += "Please complete all fields\n";
            }
            else
            {
                password = PasswordHasher.Hash(password);
                Customer c = new Customer
                {
                    Email = email,
                    Password = password,
                    First_name = fName,
                    Last_name = lName,
                    Phone_number = phone,
                    dateOfBirth = dob
                };
                var response = await UserRestService.Instance.SaveUserAsync(c);
                if (response == ErrorEnum.USER_EXISTS_ERROR)
                {
                    errormessage += "This email address is already in use. Do you already have an account?\n";
                }

                if (errormessage != "")
                {
                    responseLabel.Text = errormessage;
                    responseLabel.IsVisible = true;
                }
                else
                {
                    responseLabel.Text = "Successfully Added!";
                    responseLabel.TextColor = Color.Green;
                    responseLabel.IsVisible = true;
                }
            }
            

        }
    }
}