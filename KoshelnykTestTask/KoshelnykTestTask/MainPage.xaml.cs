using System;
using System.Collections.Generic;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;*/
using System.Threading.Tasks;
//using Android.Net;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Label header = new Label
            {
                Text = "Выполняется загрузка стран",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Blue
            };

            ActivityIndicator actInd = new ActivityIndicator()
            {
                Color = Color.Lime,
                IsRunning = true
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    actInd,
                }
            };
        }
    }
}
