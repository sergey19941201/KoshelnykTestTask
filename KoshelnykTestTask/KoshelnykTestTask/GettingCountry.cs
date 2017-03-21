using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingCountry
    {
        public static List<RootObject> listOfCountries = new List<RootObject>();//This List contains Id and Titles of all countries
        private string jsonString; //string for getting data from the url
        public async Task<List<RootObject>> FetchAsync(string url)
        {
            //getting data process goes here
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }
            
            var responseCountries = JArray.Parse(JObject.Parse(jsonString)["response"]["items"].ToString());//parsing data from jsonstring

            foreach (var countryInResponse in responseCountries)//the foreach-loop
            {
                var rootObject = new RootObject((int)countryInResponse["id"], (string)countryInResponse["title"]);
                listOfCountries.Add(rootObject);//here the program adds Id and Title of each country to the list
            }
            
            return listOfCountries;//returned list
        }

        public int retrievingChoosenCountryId()
        {
            foreach (var item in listOfCountries)
            {
                if (item.Title == FillingPage.chosenCountryTitle)
                {
                    return item.Id;
                }
            };
            return 0;
        }
    }
}
