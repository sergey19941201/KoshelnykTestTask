using System.Threading.Tasks;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Label header = new Label //header
            {
                //setting header properties:
                Text = "Выполняется загрузка стран",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Blue
            };
            
            ActivityIndicator actInd = new ActivityIndicator()//ActivityIndicator
            {
               Color = Color.Blue, //setting color
               IsRunning = true //setting running property
            };
            
            this.Content = new StackLayout //Building the page
            {
                //adding header and activity indicator as a children to StackLayout
                Children =
                {
                    header,
                    actInd,
                }
            };
            getCountry();//start getting countries
        }

        /*In this async method gettingCountry.FetchAsync(url) is called for retrieving data
        and when it gets all the data, FillingPage() starts*/
        private async Task<bool> getCountry()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";
            GettingCountry gettingCountry = new GettingCountry();
            await gettingCountry.FetchAsync(url);
            await Navigation.PushAsync(new FillingPage());
            return true;
        }
    }
}
