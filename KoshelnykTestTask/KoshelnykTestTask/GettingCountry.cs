using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Android.Content;
using Java.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
//using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace KoshelnykTestTask
{
    public class GettingCountry
    {
        public async Task<string> Data_down()
        {

            var url = "https://api.vk.com/api.php?oauth=1&method=database.getCountries&v=5.5&need_all=1&count=236";

            return url;
        }

        //public async Task<string> FetchAsync(string url)
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
/*
            JsonValue firstitem = json;
            var mydata = JObject.Parse(json);

            cityTextGlobal = (mydata["name"]).ToString();

            string GovnoData = (mydata["main"]).ToString();

            //spliting string
            string[] values = GovnoData.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                if (i == 0)
                {
                    //tempGlobal = values[i];
                    GovnoTemperature = values[i];
                }
            }
            tempGlobal = null;
            foreach (char c in GovnoTemperature)
            {
                if (c == '.')
                {
                    break;
                }
                if (c == '-' || char.IsDigit(c) == true || c == '.')
                {
                    tempGlobal += c.ToString();
                }
            }
            navigationFunc(intent);*/
            return jsonString;
        }

    }

    public class RootObject
    {
        public string CountryName { get; set; }
    }
}
