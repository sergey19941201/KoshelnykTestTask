using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    { 
        FillingPage fillingPage = new FillingPage();
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

            Button backButton = new Button()
            {
                TextColor = Color.Green,
                Text = "Назад",
                FontSize = 22,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };

            backButton.Clicked += async delegate
            {
                FillingPage.returningToRepairData = true;
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
                    resultLabel,
                    backButton
                }
            };
        }
    }
}
