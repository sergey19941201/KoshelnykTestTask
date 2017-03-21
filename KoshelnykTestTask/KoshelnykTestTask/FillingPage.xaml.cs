using System.Runtime.Remoting.Channels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingPage : ContentPage
    {
        public static string chosenCountryTitle;//we need to know the title of chosen country to find countryId
        private int selectedCountryId;//this variable is used to find cities of the country that had been chosen before
        public FillingPage()
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
                VerticalOptions = LayoutOptions.Center,
            };

            countryPicker.SelectedIndexChanged += (sender, args) =>
            {
                chosenCountryTitle = countryPicker.Items[countryPicker.SelectedIndex];//setting the value of chosen country
                selectedCountryId=gettingCountry.retrievingChoosenCountryId();//setting the id of chosen country by calling method to find all cities of it 
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
