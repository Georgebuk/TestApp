using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();

		}

		private void test()
		{
		}

		private void Test_Clicked(object sender, EventArgs e)
		{
            RestService service = new RestService();
            service.RefreshDataAsync();
		}
	}
}