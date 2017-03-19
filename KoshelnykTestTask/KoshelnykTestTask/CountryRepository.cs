using System;
using System.Collections.Generic;
using System.Text;

namespace KoshelnykTestTask
{
    public class Item
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
    }
}
