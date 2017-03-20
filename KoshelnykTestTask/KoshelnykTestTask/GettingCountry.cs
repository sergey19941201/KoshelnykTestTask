using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingCountry : ContentPage
    {
        public static List<string> CountriesList = new List<string>();
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
            }

            return listOfCountries;
        }
    }
}
