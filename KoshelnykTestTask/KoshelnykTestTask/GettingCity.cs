using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingCity : ContentPage
    {
        public static List<RootObject> listOfCities = new List<RootObject>();//This List contains Id and Titles of cities
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
                listOfCities.Add(rootObject);//here the program adds Id and Title of each city to the list
            }
            
            return listOfCities;//returned list
        }

        public int retrievingChoosenCityId()
        {
            foreach (var item in listOfCities)
            {
                /*if (item.Title == FillingPage.chosenCountryTitle)
                {
                    return item.Id;
                }*/
            };
            return 0;
        }
    }
}
