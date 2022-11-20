using Newtonsoft.Json;
using RestSharp;

// this test uses the NuGet packages "Newtonsoft.Json 13.0.2-beta3" and "RestSharp 108.0.3"

// set the options for the RestClient
var RestClientOptions = new RestClientOptions("https://api.apilayer.com/geo")
{
    ThrowOnAnyError = true,
    MaxTimeout = -1
};

// create the client from which data should be fetched
RestClient client = new RestClient(RestClientOptions);

// create requests for the data. In this case from the location country/cities. We're specifically looking for cities
// from "NL" (Netherlands) here. This can however be dynamic and changed to other countries.
RestRequest request = new RestRequest("country/cities/NL");
request.AddHeader("apikey", "4mwGx3UhK4QrIOTKZTHkxZV4o3mFevMY");

// get the response from the server and turn the response into a usable list based on a class which represents
// the generated JSON
var restResponse = await client.ExecuteAsync(request);
List<CityData> data = JsonConvert.DeserializeObject<List<CityData>>(restResponse.Content);

// loop through data to show the cities from region "Friesland"
foreach (var d in data)
{
    if (d.State_Or_Region.Equals("Friesland"))
    {
        Console.WriteLine(d.Name);
    }
}

// class based on format of the JSON fetched from the API. Any property could be removed from this and it would still
// return the right data, but without those rows
public class CityData
{
    public int Geo_ID { get; set; }
    public string Name { get; set; }
    public string State_Or_Region { get; set; }
    public Country Country { get; set; }
    public double Latitude { get; set; }
    public double Longtitude { get; set; }
}

// JSON returns a "Country" set, this class allows us to get the data within this set
public class Country
{
    public string Code { get; set; }
    public string Name { get; set; }
}