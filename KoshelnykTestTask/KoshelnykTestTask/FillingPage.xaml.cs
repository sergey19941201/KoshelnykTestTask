using System.Runtime.Remoting.Channels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingPage : ContentPage
    {
        public static string chosenCountryTitle;//variable to know the title of chosen country to find countryId
        public static string chosenCityTitle;//variable to know the title of chosen city to find university
        private int selectedCountryId;//this variable is used to find cities of the country that had been chosen before
        private int selectedCityId;//this variable is used to find universities of the city that had been chosen before
        private ListView listView = new ListView();//"drop-down" listview to choose cities or universities
        private string partOfWord;//this string is used to get cities or universities by the part of its title
        private string chooseCityOrUniversityIndicator;//Variable indicates if the user enters city or university now

        GettingCountry gettingCountry = new GettingCountry();
        GettingCity gettingCity = new GettingCity();
        GettingUniversity gettingUniversity = new GettingUniversity();
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
            };

            SearchBar citySearchBar = new SearchBar()
            {
                Placeholder = "Город"
            };

            SearchBar universitySearchBar = new SearchBar()
            {
                Placeholder = "Университет"
            };

            citySearchBar.TextChanged += delegate
            {
                //listView.ItemsSource = GettingCountry.listOfCountries;
                chooseCityOrUniversityIndicator = "city";
                universitySearchBar.Text = "";
                partOfWord = citySearchBar.Text;
                getCity();
            };

            countryPicker.SelectedIndexChanged += (sender, args) =>
            {
                chosenCountryTitle = countryPicker.Items[countryPicker.SelectedIndex];//setting the value of chosen country
                selectedCountryId = gettingCountry.retrievingChoosenCountryId();//setting the id of chosen country by calling method to find all the cities of it 
                universitySearchBar.Text = "";
                citySearchBar.Text = "";
            };

            foreach (var country in GettingCountry.listOfCountries)
            {
                countryPicker.Items.Add(country.Title);
            }

            /*foreach (string country in GettingCountry.CountriesList)
            {
                //countryPicker.Items.Add(country);
            }*/


            /*{
                // Source of data items.
                //ItemsSource = GettingCountry.CountriesList
            };*/
            listView.SeparatorColor = Color.Blue;//setting separator color

            listView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return; // don't do anything if we just de-selected the row
                chosenCityTitle = e.SelectedItem.ToString();//setting the value of chosen city
                selectedCityId = gettingCity.retrievingChoosenCityId();//setting the id of chosen city by calling method to find all the universities of it 
                citySearchBar.Text = e.SelectedItem.ToString();
                ((ListView)sender).SelectedItem = null; // de-select the row
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

        private async void getCity()
        {
            //need_all=0 that means that we get only main cities
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCities&need_all=0&Count=1000&country_id=" + selectedCountryId + "&q=" + partOfWord;
            GettingCity gettingCity = new GettingCity();
            await gettingCity.FetchAsync(url);
            listView.ItemsSource = gettingCity.listOfCities;
            //await MainPage.Navigation.PushAsync(new FillingPage());
        }
    }
}