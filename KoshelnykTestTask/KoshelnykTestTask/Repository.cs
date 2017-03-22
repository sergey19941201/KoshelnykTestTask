namespace KoshelnykTestTask
{
    //Here I have set properties for FetchAsync(string url) to get countries, cities and universities 
    public class RootObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public RootObject(int Id, string Title)
        {
            this.Id = Id;
            this.Title = Title;
        }
    }
}
