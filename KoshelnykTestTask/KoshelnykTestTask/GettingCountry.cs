using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingCountry : ContentPage
    {
        //this List<string> will be displayed in countryPicker
        public static List<string> CountriesList = new List<string>();
        
        //This List contains Id and Titles of all countries
        public async Task<List<RootObject>> FetchAsync(string url)
        {
            //string for getting data from the url
            string jsonString;
            //getting data process goes here
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }
            //declaring the List that will be returned
            var listOfCountries = new List<RootObject>();
            //parsing data from jsonstring
            var responseCountries = JArray.Parse(JObject.Parse(jsonString)["response"]["items"].ToString());
            //the foreach-loop
            foreach (var countryInResponse in responseCountries)
            {
                var rootObject = new RootObject((int)countryInResponse["id"], (string)countryInResponse["title"]);
                //here the program adds the name of each country to the list
                CountriesList.Add(rootObject.Title);
            }
            //returned value
            return listOfCountries;
        }
    }
}
