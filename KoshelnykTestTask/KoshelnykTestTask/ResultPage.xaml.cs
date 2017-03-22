using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        //declaring the page elements
        private Label header = new Label();
        private Label resultLabel = new Label();
        private Button backButton = new Button();
        public ResultPage()
        {
            //setting properties to header
            header.Text = "Данные бланка";
            header.FontSize = 26;
            header.HorizontalOptions = LayoutOptions.Center;
            header.VerticalOptions = LayoutOptions.StartAndExpand;
            header.TextColor = Color.Blue;
            //setting properties to resultLabel
            resultLabel.Text =
                "Имя: " + FillingPage.name +
                "\nФамилия: " + FillingPage.surname +
                "\nСтрана: " + FillingPage.chosenCountryTitle +
                "\nГород: " + FillingPage.chosenCityTitle +
                "\nУниверситет: " + FillingPage.university;
            resultLabel.FontSize = 22;
            resultLabel.HorizontalOptions = LayoutOptions.Center;
            resultLabel.VerticalOptions = LayoutOptions.Center;
            resultLabel.TextColor = Color.Navy;
            //setting properties to button
            backButton.TextColor = Color.Green;
            backButton.Text = "Редактировать";
            backButton.FontSize = 22;
            backButton.VerticalOptions = LayoutOptions.EndAndExpand;
            //button clicked event
            backButton.Clicked += async delegate
            {
                FillingPage.returningToRepairDataIndicator = true;//varaiable to know that the user decided to repair some data. If it true, it automatically fills textFields
                await Navigation.PushAsync(new FillingPage());//starting FillingPage
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
