using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KoshelnykTestTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingPage : ContentPage
    {
        public static string name;//user name
        public static string surname;//user surname
        public static string chosenCountryTitle;//variable to know the title of chosen country to find countryId
        public static string chosenCityTitle;//variable to know the title of chosen city to find university
        public static string university;//university

        public static bool returningToRepairDataIndicator;//this uses for detecting programmatically in case the user returns this page to repair the data
        public static int selectedCountryIx;//this is the index of selected country from a picker to set the country by default if he wants to repair his data
        public static int selectedCountryId;//this variable is used to find cities of the country that had been chosen before
        public static int selectedCityId;//this variable is used to find universities of the city that had been chosen before

        private ListView listView = new ListView();//"drop-down" listview to choose cities or universities

        //cityOrUniversityIndicator is used for detecting programmatically what resource for listView must be used. (list of cities or list of universities) 
        private static string cityOrUniversityIndicator;

        //declaring the page elements
        private Label header = new Label();
        private Entry nameEntry = new Entry();
        private Entry surnameEntry = new Entry();
        private Picker countryPicker = new Picker();
        private SearchBar citySearchBar = new SearchBar();
        private SearchBar universitySearchBar = new SearchBar();
        private Button executeButton = new Button();

        //declaring classes which are used for parsing data from url
        private GettingCountry gettingCountry = new GettingCountry();
        private GettingCity gettingCity = new GettingCity();
        private GettingUniversity gettingUniversity = new GettingUniversity();


        public FillingPage()
        {
            //setting header properties
            header.Text = "Заполните бланк";
            header.FontSize = 26;
            header.HorizontalOptions = LayoutOptions.Center;
            header.VerticalOptions = LayoutOptions.CenterAndExpand;
            header.TextColor = Color.Blue;

            //setting name and surname entry placeholders
            nameEntry.Placeholder = "Имя";
            surnameEntry.Placeholder = "Фамилия";

            //setting countryPicker properties
            countryPicker.Title = "Страна";
            countryPicker.VerticalOptions = LayoutOptions.Center;

            citySearchBar.Placeholder = "Город";//setting citySearchBar placeholder
            universitySearchBar.Placeholder = "Университет";//setting universitySearchBar placeholder

            //execute button properties
            executeButton.TextColor = Color.Green;
            executeButton.Text = "Выполнить";
            executeButton.FontSize = 22;

            try
            {
                //declaring variables to prevent twice-firing alert message
                int preventTwiceFiringAlertSurname = 0;
                int preventTwiceFiringAlertCountry = 0;
                int preventTwiceFiringAlertCity = 0;
                int preventTwiceFiringAlertUniversity = 0;
                //textChanged event of surnameEntry
                surnameEntry.TextChanged += delegate
                {
                    if (String.IsNullOrEmpty(nameEntry.Text))
                        //if name entry IsNullOrEmpty it displays the alert message
                    {
                        if (preventTwiceFiringAlertSurname == 0) //construction to prevent twice-firing alert message
                        {
                            DisplayAlert("Внимание", "Введите имя", "OK"); //alert message
                            surnameEntry.Text = ""; //setting text
                            preventTwiceFiringAlertSurname++;
                        }
                        else
                        {
                            preventTwiceFiringAlertSurname = 0;
                        }
                    }
                };

                //SelectedIndexChanged event of countryPicker
                countryPicker.SelectedIndexChanged += (sender, args) =>
                {
                    if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text))
                        //detecting if the previous fields are not empty
                    {
                        if (preventTwiceFiringAlertCountry == 0) //construction to prevent twice-firing alert message
                        {
                            DisplayAlert("Внимание", "Заполните все поля выше", "OK"); //displaying alert
                            countryPicker.SelectedIndex = -1; //setting empty picker if the previous fields empty
                            preventTwiceFiringAlertCountry++;
                        }
                        else
                        {
                            preventTwiceFiringAlertCountry = 0;
                        }
                    }
                    else
                    {
                        chosenCountryTitle = countryPicker.Items[countryPicker.SelectedIndex];
                        //setting the value of chosen country
                        selectedCountryIx = countryPicker.SelectedIndex;
                        //setting the index of counry in picker to set the counry if the user will return to repair this page
                        selectedCountryId = gettingCountry.retrievingChoosenCountryId();
                        //setting the id of chosen country by calling method to find all the cities of it 
                    }
                };

                //textChanged event of citySearchBar
                citySearchBar.TextChanged += delegate
                {
                    cityOrUniversityIndicator = "city"; //setting cityOrUniversityIndicator for listview recource
                    if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text) ||
                        countryPicker.SelectedIndex == -1) //detecting if the previous fields are not empty
                    {
                        if (preventTwiceFiringAlertCity == 0) //construction to prevent twice-firing alert message
                        {
                            DisplayAlert("Внимание", "Заполните все поля выше", "OK"); //displaying alert
                            citySearchBar.Text = "";
                            preventTwiceFiringAlertCity++;
                        }
                        else
                        {
                            preventTwiceFiringAlertCity = 0;
                        }
                    }
                    else
                    {
                        getCityTask(); //method to retrieve city
                    }
                };

                //textChanged event of universitySearchBar
                universitySearchBar.TextChanged += delegate
                {
                    if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text) ||
                        countryPicker.SelectedIndex == -1 || String.IsNullOrEmpty(citySearchBar.Text))
                        //detecting if the previous fields are not empty
                    {
                        if (preventTwiceFiringAlertUniversity == 0) //construction to prevent twice-firing alert message
                        {
                            DisplayAlert("Внимание", "Заполните все поля выше", "OK"); //displaying alert
                            universitySearchBar.Text = ""; //setting text
                            preventTwiceFiringAlertUniversity++;
                        }
                        else
                        {
                            preventTwiceFiringAlertUniversity = 0;
                        }
                    }
                    else
                    {
                        cityOrUniversityIndicator = "university";
                        //setting cityOrUniversityIndicator for listview recource
                        getUniversityTask(); //getting the universities
                    }
                };

                foreach (var country in GettingCountry.listOfCountries) //foreach loop for countries
                {
                    countryPicker.Items.Add(country.Title); //adding countries to the picker
                }

                listView.SeparatorColor = Color.Blue; //setting separator color for listView

                //ItemSelected event of listView
                listView.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem == null) return; //don't do anything if we just de-selected the row

                    if (cityOrUniversityIndicator == "city")
                        //if it is true the resource of listview will be list of cities
                    {
                        chosenCityTitle = e.SelectedItem.ToString(); //setting the value of chosen city
                        selectedCityId = gettingCity.retrievingChoosenCityId();
                        //setting the id of chosen city by calling method to find all the universities of it 
                        citySearchBar.Text = e.SelectedItem.ToString(); //setting text of the selected city
                    }
                    if (cityOrUniversityIndicator == "university")
                        //if it is true the resource of listview will be list of universities
                    {
                        university = e.SelectedItem.ToString(); //setting the value of chosen university
                        universitySearchBar.Text = e.SelectedItem.ToString(); //setting text of the selected university
                    }

                    ((ListView) sender).SelectedItem = null; //de-select the row
                };

                //executeButton click event
                executeButton.Clicked += async delegate
                {
                    if (String.IsNullOrEmpty(surnameEntry.Text) || String.IsNullOrEmpty(nameEntry.Text) ||
                        countryPicker.SelectedIndex == -1 || String.IsNullOrEmpty(citySearchBar.Text) ||
                        String.IsNullOrEmpty(universitySearchBar.Text)) //detecting if the previous fields are not empty
                    {
                        DisplayAlert("Внимание", "Заполните все поля", "OK"); //displaying alert
                    }
                    else
                    {
                        //Assignment field values to variables
                        name = nameEntry.Text;
                        surname = surnameEntry.Text;
                        chosenCityTitle = citySearchBar.Text;
                        university = universitySearchBar.Text;
                        await Navigation.PushAsync(new ResultPage());
                    }
                };

                // Accomodate iPhone status bar.
                this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

                //if the user decides to repair some data
                if (returningToRepairDataIndicator == true)
                {
                    setPreviousValues(); //filling fields if the user decides to repair some data
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
            catch{}
        }

        private void setPreviousValues()//filling fields if the user decides to repair some data
        {
            nameEntry.Text = name;
            surnameEntry.Text = surname;
            countryPicker.SelectedIndex = selectedCountryIx;
            citySearchBar.Text = chosenCityTitle;
            universitySearchBar.Text = university;
        }

        private async Task<bool> getCityTask()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCities&need_all=0&Count=1000&country_id=" + selectedCountryId + "&q=" + citySearchBar.Text;//setting current url. the need_all=0 parameter means that the program gets only main cities
            GettingCity gettingCity = new GettingCity();//THIS needs to be here to prevent bugs
            await gettingCity.FetchAsync(url);//executing await method to get list of cities
            listView.ItemsSource = gettingCity.listOfCities;//setting list of cities as ItemsSource for the listView
            return true;
        }

        private async Task<bool> getUniversityTask()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getUniversities&city_id=" + selectedCityId + "&q=" + universitySearchBar.Text;//setting current url
            listView.ItemsSource = await gettingUniversity.FetchAsync(url);//setting list of universities as ItemsSource for the listView
            return true;
        }
    }
}