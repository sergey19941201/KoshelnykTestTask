using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace KoshelnykTestTask
{
    public class GettingUniversity
    {
        public static List<string> listOfUniversities = new List<string>();//list with names of the universities
        private string jsonString; //string for getting data from the url
        public async Task<List<string>> FetchAsync(string url)
        {
            //getting data process goes here
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }

            var universityRepository = JToken.Parse(jsonString)["response"]
                //Filter the integer value by selecting only objects
                .OfType<JObject>()
                //Deserialize each object to a RootObject
                .Select(o => o.ToObject<RootObject>())
                //Return in a List<RootObject>
                .ToList();
            listOfUniversities = universityRepository.Select(u => u.Title).ToList();

            return listOfUniversities; //returned list
        }
    }
}

