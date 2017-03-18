﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public partial class MainPage : ContentPage
    {
        // Dictionary to get Color from color name.
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

        public MainPage()
        {
            Label header = new Label
            {
                Text = "Picker",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Blue
            };

            Entry entry = new Entry()
            {
                Placeholder = "Password",
                IsPassword = true
            };

            Entry entry1 = new Entry()
            {
                Placeholder = "Username"
            };

            Picker EditText = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Picker picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.SelectedIndexChanged += (sender, args) =>
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
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);


            CreateLoginForm();

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    EditText,
                    entry,
                    entry1,
                    picker,
                    boxView
                }
            };

        }

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
