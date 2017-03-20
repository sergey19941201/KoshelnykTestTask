using Xamarin.Forms;

namespace KoshelnykTestTask
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            //setting the MainPage
            MainPage = new NavigationPage(new KoshelnykTestTask.MainPage());
		}

        protected override void OnStart ()
		{
		   getCountry();
		}

        /*In this async method gettingCountry.FetchAsync(url) is called for retrieving data
        and when it gets all the data, FillingPage() starts*/
        private async void getCountry()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";
            GettingCountry gettingCountry = new GettingCountry();
            await gettingCountry.FetchAsync(url);
            await MainPage.Navigation.PushAsync(new FillingPage());
        }
	}
}
