using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingUniversity : ContentPage
    {
        public static List<RootObject> listOfUniversitiesRoot = new List<RootObject>();//This List contains Id and Titles of universities
        public List<string> listOfUniversities = new List<string>();//list with names of the universities
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
            
            var responseUniversities = JArray.Parse(JObject.Parse(jsonString)["response"].ToString());//parsing data from jsonstring

            foreach (var universityInResponse in responseUniversities)//the foreach-loop
            {
                var rootObject = new RootObject((int)universityInResponse["id"],(string)universityInResponse["title"]);
                listOfUniversities.Add(rootObject.Title);//adding to the list with names of the universities
            }
            
            return listOfUniversitiesRoot;//returned list
        }
    }
}
