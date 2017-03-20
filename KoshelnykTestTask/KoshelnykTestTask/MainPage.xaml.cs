using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //header
            Label header = new Label
            {
                //setting properties:
                Text = "Выполняется загрузка стран",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Blue
            };
            //ActivityIndicator
            ActivityIndicator actInd = new ActivityIndicator()
            {
                //setting color
                Color = Color.Lime,
                //setting running property
                IsRunning = true
            };
            //Building the page
            this.Content = new StackLayout
            {
                //adding header and activity indicator as a children to StackLayout
                Children =
                {
                    header,
                    actInd,
                }
            };
        }
    }
}
