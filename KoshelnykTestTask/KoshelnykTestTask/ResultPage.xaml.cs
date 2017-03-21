using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    { 
        public ResultPage()
        {
            Label header = new Label
            {
                Text = "Данные бланка",
                FontSize = 26,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.StartAndExpand,
                TextColor = Color.Blue,
            };

            Label resultLabel = new Label
            {
                Text =
                "Имя: " + FillingPage.name+ 
                "\nФамилия: " + FillingPage.surname+
                "\nСтрана: " + FillingPage.chosenCountryTitle+
                "\nГород: " + FillingPage.chosenCityTitle+
                "\nУниверситет: " + FillingPage.university,
                FontSize = 22,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Navy,
            };

            /*Label surnameLabel = new Label
            {
                Text = "Фамилия: " + FillingPage.surname,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Navy
            };
            Label countryLabel = new Label
            {
                Text = "Страна: " + FillingPage.chosenCountryTitle,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Navy
            };
            Label cityLabel = new Label
            {
                Text = "Город: " + FillingPage.chosenCityTitle,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Navy
            };

            Label universityLabel = new Label
            {
                Text = "Университет: " + FillingPage.university,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Navy
            };*/

            Button backButton = new Button()
            {
                TextColor = Color.Green,
                Text = "Назад",
                FontSize = 22,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };

            backButton.Clicked += async delegate
            {
                await Navigation.PushAsync(new FillingPage());
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    /*nameLabel,
                    surnameLabel,
                    countryLabel,
                    cityLabel,
                    universityLabel,*/
                    resultLabel,
                    backButton
                }
            };
        }
    }
}
