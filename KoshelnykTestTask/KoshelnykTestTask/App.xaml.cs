using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace KoshelnykTestTask
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            
            MainPage = new NavigationPage(new KoshelnykTestTask.MainPage());
		}

        protected override void OnStart ()
		{
            // Handle when your app starts
		   getCountry();
		}

        private async void getCountry()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";
            GettingCountry gettingCountry = new GettingCountry();
            await gettingCountry.FetchAsync(url);
            await MainPage.Navigation.PushAsync(new FillingPage());
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
