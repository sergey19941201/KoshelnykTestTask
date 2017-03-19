using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Net;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //GettingCountry gettingCountry = new GettingCountry();
            
            Label header = new Label
            {
                Text = "Выполняется подгрузка стран",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Blue
            };

            ActivityIndicator actInd = new ActivityIndicator()
            {
                Color = Color.Lime,
                IsRunning = true
            };

            Button myButton = new Button()
            {
                TextColor = Color.Green,
                Text = "Выполнить",
                FontSize = 22
            };

            myButton.Clicked += OnButtonClicked;

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    actInd,
                    myButton
                }
            };
            //StartFillingPage();
            //await getCountry();
        }

        

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new FillingPage());
            getCountry();
        }
        private async Task<bool> getCountry()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";
            GettingCountry gettingCountry = new GettingCountry();
            await gettingCountry.FetchAsync(url);
            return true;
        }

        public async Task<bool> StartFillingPage()
        {
            Navigation.PushAsync(new FillingPage());
            return true;
        }

    }
}
