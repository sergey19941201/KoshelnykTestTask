using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Android.App;
using Android.Content;
using Java.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

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
            string countryData = "df";
            var readJson = JObject.Parse(json);
            
            string countryName = readJson["response"]["items"].ToString();
           // int n = countryName.getI
            //string first = countryName[0].ToString();
            string n="";
            

            return jsonString;
        }
    }

    /*public class RootObject
    {
        public string CountryName { get; set; }
    }*/
}
