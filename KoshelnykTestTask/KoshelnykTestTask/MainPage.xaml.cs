using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Label header = new Label //header
            {
                //setting properties:
                Text = "Выполняется загрузка стран",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Blue
            };
            
            ActivityIndicator actInd = new ActivityIndicator()//ActivityIndicator
            {
               Color = Color.Lime, //setting color
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
        }
    }
}
