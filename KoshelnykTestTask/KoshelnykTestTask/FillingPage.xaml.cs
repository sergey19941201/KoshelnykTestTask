using System.Runtime.Remoting.Channels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingPage : ContentPage
    {
        public FillingPage()
        {
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
                VerticalOptions = LayoutOptions.Center,
                //SelectedIndex = 1
                
            };

            countryPicker.SelectedIndexChanged += (sender, args) =>
            {
                string colorName = countryPicker.Items[countryPicker.SelectedIndex];
            };

            foreach (var country in GettingCountry.listOfCountries)
            {
                countryPicker.Items.Add(country.Title);
            }

            /*foreach (string country in GettingCountry.CountriesList)
            {
                //countryPicker.Items.Add(country);
            }*/

            ListView listView = new ListView();
            /*{
                // Source of data items.
                //ItemsSource = GettingCountry.CountriesList

            };*/
            listView.ItemSelected += //async
                (sender, e) =>
                {
                    //FillingPage itm = (FillingPage)e.SelectedItem;
                    if (e.SelectedItem == null) return; // don't do anything if we just de-selected the row
                    var n = e.SelectedItem;
                ((ListView)sender).SelectedItem = null; // de-select the row
            };

            SearchBar citySearchBar = new SearchBar()
            {
                Placeholder = "Город"
            };

            citySearchBar.TextChanged += delegate
            {
                listView.ItemsSource = GettingCountry.listOfCountries;
            };

            SearchBar universitySearchBar = new SearchBar()
            {
                Placeholder = "Университет"
            };

            Button executeButton = new Button()
            {
                TextColor = Color.Green,
                Text = "Выполнить",
                FontSize = 22
            };

            executeButton.Clicked += delegate
            {
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
                    citySearchBar,
                    universitySearchBar,
                    listView,
                    executeButton
                }
            };
        }
    }
}
