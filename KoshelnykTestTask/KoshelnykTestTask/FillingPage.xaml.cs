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
        // Dictionary to get Color from color name.
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
        };

        public FillingPage ()
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

            SearchBar countrySearchBar = new SearchBar()
            {
                Placeholder = "Страна",
                SearchCommand = new Command(() =>
                {

                })
            };

            countrySearchBar.TextChanged += delegate
            {
                countrySearchBar.Text = "sdsd";
            };

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

            // myButton.Clicked += OnButtonClicked;

            /*Picker EditText = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };*/

            /*Picker picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }*/

            Picker picker = new Picker
            {
                Title = "myPicker",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string country in gettingCountry.CountriesList)
            {
                picker.Items.Add(country);
            }

            // Create BoxView for displaying picked Color
            /*BoxView boxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };*/

            /*picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    boxView.Color = Color.Default;
                }
                else
                {
                    string colorName = picker.Items[picker.SelectedIndex];
                    boxView.Color = nameToColor[colorName];
                }
            };*/

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);


            CreateLoginForm();

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    //EditText,
                    nameEntry,
                    surnameEntry,
                    countrySearchBar,
                    townSearchBar,
                    universitySearchBar,
                    myButton,
                    picker,
                    //boxView
                }
            };
            /*this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    //EditText,
                    /*nameEntry,
                    surnameEntry,
                    picker,
                    boxView
                }
            };*/
        }

        /*private void MySearchBarOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
       {
           // Has Backspace or Cancel has been pressed?
           if (textChangedEventArgs.NewTextValue == string.Empty)
           {
               // Yes, which one?
               if (textChangedEventArgs.OldTextValue.Length > 1)
               {
                   // Cancel has most probably been pressed
                   //Debug.WriteLine("Cancel Pressed");
               }
               else
               {
                   // Backspace pressed on single character
                   // Cancel pressed on single character
                   //Debug.WriteLine("Backspace or Cancel Pressed");
               }
           }

       }*/

       



        View CreateLoginForm()
        {
            var usernameEntry = new Entry { Placeholder = "Username" };
            var passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            return new StackLayout
            {
                Children =
                {
                    usernameEntry,
                    passwordEntry
                }
            };
        }
    }
}
