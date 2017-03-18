using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KoshelnykTestTask
{
    public class GettingCountry
    {
        public async Task<string> FetchAsync()
        {
            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";

            string jsonString;

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }

            var json = jsonString;
            var countryData = JObject.Parse(json);
            string country = (countryData["title"]).ToString();
            string n="";

            return jsonString;
        }

    }

    /*public class RootObject
    {
        public string CountryName { get; set; }
    }*/
}
