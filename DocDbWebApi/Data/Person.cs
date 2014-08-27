using Newtonsoft.Json;

namespace DocDbWebApi.Data
{
	public class Person
	{
        [JsonProperty(PropertyName = "id")] // <-- need to add this mapping to avoid issues
        public string Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }

	}
}