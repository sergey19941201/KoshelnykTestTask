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
	public partial class FillingPage : ContentPage
	{
        public FillingPage ()//List<List<RootObject>> countryL
        {
            GettingCountry gettingCountry = new GettingCountry();

            Label header = new Label
            {
                Text = "Заполните бланк",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Blue
            };

            Entry nameEntry = new Entry()
            {
                Placeholder = "Имя",
            };

            Entry surnameEntry = new Entry()
            {
                Placeholder = "Фамилия"
            };

            Picker countryPicker = new Picker()
            {
                Title = "Страна",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string country in GettingCountry.CountriesList)
            {
                countryPicker.Items.Add(country);
            }

            SearchBar townSearchBar = new SearchBar()
            {
                Placeholder = "Город",
                SearchCommand = new Command(() =>
                {
                })
            };

            SearchBar universitySearchBar = new SearchBar()
            {
                Placeholder = "Университет",
                SearchCommand = new Command(() =>
                {
                })
            };

            Button myButton = new Button()
           {
               TextColor = Color.Green,
               Text = "Выполнить",
               FontSize = 22
           };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    nameEntry,
                    surnameEntry,
                    countryPicker,
                    townSearchBar,
                    universitySearchBar,
                    myButton
                }
            };
        }
    }
}
