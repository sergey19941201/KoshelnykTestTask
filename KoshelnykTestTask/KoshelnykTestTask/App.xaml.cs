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
	}
}
