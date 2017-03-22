using System;
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
        private int selectedCountryId;//this variable is used to find cities of the country that had been chosen before
        private int selectedCityId;//this variable is used to find universities of the city that had been chosen before
        private ListView listView = new ListView();//"drop-down" listview to choose cities or universities
        private string partOfWord;//this string is used to get cities or universities by the part of its title
        public static string university;

        GettingCountry gettingCountry = new GettingCountry();
        GettingCity gettingCity = new GettingCity();
        GettingUniversity gettingUniversity = new GettingUniversity();

        private Entry nameEntry = new Entry();
        private Entry surnameEntry = new Entry();

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
                    selectedIndexChangedIssueFixed++;
                    selectedIndexChangedIssueFixedCityField++;
                }
                else
                {
                    selectedIndexChangedIssueFixed = 0;
                    selectedIndexChangedIssueFixedCityField = 0;
                }
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

            citySearchBar.TextChanged += delegate
            {
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
                    partOfWord = citySearchBar.Text;
                    getCity();
                }
            };

            SearchBar universitySearchBar = new SearchBar()
            {
                Placeholder = "Университет"
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

                chosenCityTitle = e.SelectedItem.ToString(); //setting the value of chosen city
                selectedCityId = gettingCity.retrievingChoosenCityId();
                //setting the id of chosen city by calling method to find all the universities of it 
                citySearchBar.Text = e.SelectedItem.ToString();

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
                 getUniversityTask();
                 name = nameEntry.Text;
                 surname = surnameEntry.Text;
                 //await Navigation.PushAsync(new ResultPage());
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

        private async void getUniversityTask()
        {
            //need_all=0 that means that we get only main cities
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getUniversities&city_id=314";// + selectedCityId;
            //var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";
            //GettingCity gettingCity = new GettingCity();
            await gettingUniversity.FetchAsync(url);
            listView.ItemsSource = gettingUniversity.listOfUniversities;
            //await MainPage.Navigation.PushAsync(new FillingPage());
        }
    }
}