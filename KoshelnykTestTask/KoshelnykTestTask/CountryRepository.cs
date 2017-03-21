using System;
using System.Collections.Generic;
using System.Text;

namespace KoshelnykTestTask
{
    //Here I have set properties for FetchAsync(string url)
    public class RootObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public RootObject(int countryId, string countryTitle)
        {
            this.Id = countryId;
            this.Title = countryTitle;
        }
    }
}
