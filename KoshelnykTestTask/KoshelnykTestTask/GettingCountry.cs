using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

/*using Android.App;
using Android.Content;
using Java.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;*/
using System.Collections.Generic;
/*using System.ComponentModel;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;*/
using Android.Content.Res;
using Xamarin.Forms;

/*using Java.Security;
using Javax.Security.Auth;
using Xamarin.Forms;*/

namespace KoshelnykTestTask
{
    public class GettingCountry : ContentPage
    {
        public static List<string> CountriesList = new List<string>();

        MainPage mainPage = new MainPage();
        public async Task<List<RootObject>> FetchAsync(string url)
        {
            string jsonString;
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }

            var listOfCountries = new List<RootObject>();

            var responseCountries = JArray.Parse(JObject.Parse(jsonString)["response"]["items"].ToString());

            foreach (var countryInResponse in responseCountries)
            {
                var rootObject = new RootObject((int)countryInResponse["id"], (string)countryInResponse["title"]);

                CountriesList.Add(rootObject.Title);
                //listOfCountries.Add(rootObject);
            }

            //mainPage.StartFillingPage();
            //await Navigation.PushAsync(new FillingPage());

            return listOfCountries;
        }

        /*public async Task<bool> StartFillingPage()
        {
            await Navigation.PushAsync(new FillingPage());
            return true;
        }*/
    }
}
