using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingCity : ContentPage
    {
        public static List<RootObject> listOfCitiesRoot = new List<RootObject>();//This List contains Id and Titles of cities
        public List<string> listOfCities = new List<string>();//list with names of the cities
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
            
            var responseCities = JArray.Parse(JObject.Parse(jsonString)["response"].ToString());//parsing data from jsonstring

            foreach (var cityInResponse in responseCities)//the foreach-loop
            {
                var rootObject = new RootObject((int)cityInResponse["cid"],(string)cityInResponse["title"]);
                listOfCitiesRoot.Add(rootObject);//here the program adds Id and Title of each city to the list
                listOfCities.Add(rootObject.Title);//adding to the list with names of the cities
            }
            
            return listOfCitiesRoot;//returned list
        }

        public int retrievingChoosenCityId()
        {
            foreach (var item in listOfCitiesRoot)
            {
                if (item.Title == FillingPage.chosenCityTitle)
                {
                    return item.Id;
                }
            };
            return 0;
        }
    }
}
