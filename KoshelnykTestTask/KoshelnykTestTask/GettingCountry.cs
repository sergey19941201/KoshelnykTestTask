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
using System.ComponentModel;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Java.Security;
using Javax.Security.Auth;
using Xamarin.Forms;

namespace KoshelnykTestTask
{
    public class GettingCountry
    {
        public async Task<RootObject> FetchAsync(string url)
        {
            string jsonString;
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }

            var readJson = JObject.Parse(jsonString);
            string countryName = readJson["response"]["items"].ToString();

            var deserialized = JsonConvert.DeserializeObject<RootObject>(jsonString);

            return deserialized;
        }
    }
}
