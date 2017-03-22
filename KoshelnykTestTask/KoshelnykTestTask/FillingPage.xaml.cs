using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingPage : ContentPage
    {
        public static string name;
        public static string surname;
        public static string chosenCountryTitle;//variable to know the title of chosen country to find countryId
        public static string chosenCityTitle;//variable to know the title of chosen city to find university
        public static string university;

        public static bool returningToRepairData;

        public static int selectedCountryId;//this variable is used to find cities of the country that had been chosen before
        public static int selectedCityId;//this variable is used to find universities of the city that had been chosen before

        private ListView listView = new ListView();//"drop-down" listview to choose cities or universities
        //private string partOfWord;//this string is used to get cities or universities by the part of its title
        
        private static string cityOrUniversityIndicator;

        GettingCountry gettingCountry = new GettingCountry();
        GettingCity gettingCity = new GettingCity();
        GettingUniversity gettingUniversity = new GettingUniversity();

        private Entry nameEntry = new Entry();
        private Entry surnameEntry = new Entry();
        private Picker countryPicker = new Picker();
        private SearchBar universitySearchBar = new SearchBar();
        private SearchBar citySearchBar = new SearchBar();

        public FillingPage()
        {
            int selectedIndexChangedIssueFixed = 0;
            int selectedIndexChangedIssueFixedCityField = 0;

            Label header = new Label
            {
                Text = "Заполните бланк",
                FontSize = 26,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Blue
            };

            nameEntry.Placeholder = "Имя";
            surnameEntry.Placeholder = "Фамилия";

            nameEntry.TextChanged += delegate
            {
                selectedIndexChangedIssueFixed++;
                selectedIndexChangedIssueFixedCityField++;
            };

            surnameEntry.TextChanged += delegate
            {
                if (String.IsNullOrEmpty(nameEntry.Text))
                {
                    DisplayAlert("Внимание", "Введите имя", "OK");
                    surnameEntry.Text = "";
                    selectedIndexChangedIssueFixed++;
                    selectedIndexChangedIssueFixedCityField++;
                }
                else
                {
                    selectedIndexChangedIssueFixed = 0;
                    selectedIndexChangedIssueFixedCityField = 0;
                }
            };

                countryPicker.Title = "Страна";
                countryPicker.VerticalOptions = LayoutOptions.Center;

                citySearchBar.Placeholder = "Город";

            citySearchBar.TextChanged += delegate
            {
                cityOrUniversityIndicator = "city";
                //listView.ItemsSource = GettingCountry.listOfCountries;
                if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text) ||
                    countryPicker.SelectedIndex == -1)
                {
                    if (selectedIndexChangedIssueFixedCityField == 0)
                    {
                        DisplayAlert("Внимание", "Заполните все поля выше", "OK");
                        citySearchBar.Text = "";
                        selectedIndexChangedIssueFixedCityField++;
                    }
                }
                else
                {
                   // partOfWord = citySearchBar.Text;
                    getCityTask();
                }
            };


            universitySearchBar.Placeholder = "Университет";

            universitySearchBar.TextChanged += delegate
            {
                if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text) ||
                    countryPicker.SelectedIndex == -1 || String.IsNullOrEmpty(citySearchBar.Text))
                {
                    DisplayAlert("Внимание", "Заполните все поля выше", "OK");
                    universitySearchBar.Text = "";
                }
                else
                {
                    cityOrUniversityIndicator = "university";
                    // partOfWord = citySearchBar.Text;
                    getUniversityTask(); //getting the universities
                }
            };

            countryPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text))
                {
                    if (selectedIndexChangedIssueFixed == 0)
                    {
                        DisplayAlert("Внимание", "Заполните все поля выше", "OK");
                        selectedIndexChangedIssueFixedCityField++;
                    }
                    countryPicker.SelectedIndex = -1;
                    selectedIndexChangedIssueFixed++;
                    selectedIndexChangedIssueFixedCityField = 0;
                }
                else
                {
                    chosenCountryTitle = countryPicker.Items[countryPicker.SelectedIndex];//setting the value of chosen country
                    selectedCountryId = gettingCountry.retrievingChoosenCountryId();//setting the id of chosen country by calling method to find all the cities of it 
                }

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


                if (cityOrUniversityIndicator == "city")
                {
                    chosenCityTitle = e.SelectedItem.ToString(); //setting the value of chosen city
                    selectedCityId = gettingCity.retrievingChoosenCityId();
                    //setting the id of chosen city by calling method to find all the universities of it 
                    citySearchBar.Text = e.SelectedItem.ToString();
                }
                if (cityOrUniversityIndicator == "university")
                {
                    university= e.SelectedItem.ToString();
                    universitySearchBar.Text= e.SelectedItem.ToString();
                }

                ((ListView)sender).SelectedItem = null; // de-select the row
            };



            Button executeButton = new Button()
            {
                TextColor = Color.Green,
                Text = "Выполнить",
                FontSize = 22
            };

            executeButton.Clicked += async delegate
            {
                returningToRepairData = false;
                 //getUniversityTask();
                 name = nameEntry.Text;
                 surname = surnameEntry.Text;
                 //countryPicker.SelectedIndex = 2;
                 chosenCityTitle= citySearchBar.Text;
                  university= universitySearchBar.Text;
                 await Navigation.PushAsync(new ResultPage());
             };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);









            if (returningToRepairData == true)
            {
                nameEntry.Text = name;
                surnameEntry.Text = surname;
                countryPicker.SelectedIndex = 2;
                citySearchBar.Text = chosenCityTitle;
                universitySearchBar.Text = university;
            }









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

        public async Task<bool> setPreviousValues()
        {
            return true;
        }

        private async void getCityTask()
        {
            //need_all=0 that means that we get only main cities
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCities&need_all=0&Count=1000&country_id=" + selectedCountryId + "&q=" + citySearchBar.Text;
            GettingCity gettingCity = new GettingCity();
            await gettingCity.FetchAsync(url);
            listView.ItemsSource = gettingCity.listOfCities;
            //await MainPage.Navigation.PushAsync(new FillingPage());
        }

        private async void getUniversityTask()
        {
            //need_all=0 that means that we get only main cities
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getUniversities&city_id=" + selectedCityId+"&q="+ universitySearchBar.Text;
            //var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";
            //GettingCity gettingCity = new GettingCity();
            listView.ItemsSource = await gettingUniversity.FetchAsync(url);
            // gettingUniversity.listOfUniversities;
            //await MainPage.Navigation.PushAsync(new FillingPage());
        }
    }
}