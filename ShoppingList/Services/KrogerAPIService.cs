using System.Text.Json;
using ShoppingList.Model.Api;

namespace ShoppingList.Services;

public class KrogerAPIService
{
    HttpClient _client;
    AccessTokenRes accessToken;
    DateTime expireTime; 

    public KrogerAPIService()
    {
        _client = new HttpClient();
        expireTime = DateTime.Now;

    }

    public async Task<ApiConfig> GetStartupConfigAsync()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("krogerapiconfig.json");
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var output = JsonSerializer.Deserialize<ApiConfig>(json);
        return output;
    }

    public async Task<bool> SetAuthTokensAsync(ApiConfig apiConfig)
    {

        if (accessToken is not null && DateTime.Now < expireTime)
            return false; 

        Guard.IsNotNull(apiConfig, nameof(apiConfig));

        var authData = $"{apiConfig.ClientId}:{apiConfig.ClientSecret}";
        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

        _client.DefaultRequestHeaders.Authorization = new("Basic", authHeaderValue);

        StringContent content = new("grant_type=client_credentials&scope=product.compact", Encoding.ASCII, "application/x-www-form-urlencoded"); 
        
        HttpResponseMessage res = await _client.PostAsync(apiConfig.KrogerUrl + "connect/oauth2/token", content); 

        if (res.IsSuccessStatusCode)
        {
            var resCont = await res.Content.ReadAsStringAsync();
            accessToken = JsonSerializer.Deserialize<AccessTokenRes>(resCont);
            expireTime = DateTime.Now; 
            expireTime = expireTime.AddSeconds(accessToken.expires_in); 

            return true; 
        } else
        {
            throw new Exception("Unable to Authenticate, Sorting Functionality Offline"); 
        }

    }

    public async Task<Dictionary<string, string>> GetLocationNearZipAsync(string zip, ApiConfig apiConfig)
    {

        Guard.IsNotNullOrEmpty(zip, nameof(zip));
        Guard.IsNotNull(apiConfig, nameof(apiConfig));
        // add code to check that we got a zip (and not, idk, say, some type of script attack lol)

        Dictionary<string, string> closeKrogerNames = new Dictionary<string, string>(); 


        string zipQuery = $"?filter.zipCode.near={zip}&filter.radiusInMiles=25&filter.chain=KROGER";
        Uri uri = new($"{apiConfig.KrogerUrl}locations{zipQuery}");

        _client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken.access_token);

        HttpResponseMessage res = await _client.GetAsync(uri);

        if (res.IsSuccessStatusCode)
        {
            string content = await res.Content.ReadAsStringAsync();
            var jsonRes = JsonSerializer.Deserialize<Rootobject>(content); 

            foreach (var store in jsonRes.data)
            {
                if (store.chain.Equals("KROGER"))
                {
                    closeKrogerNames.Add(store.locationId, store.name); 
                }
            }

            if (closeKrogerNames.Count == 0)
                throw new Exception("No Krogers found within a 25 mile radius");

            return closeKrogerNames; 
        }
        else
        {
            throw new Exception("No Krogers found within a 25 mile radius"); 
        }
    }




    public async Task<(ItemLocationData, Item)> GetProductLocationDataAsync(string term, string locationId, ApiConfig apiConfig)
    {
        Guard.IsNotNullOrEmpty(term, nameof(term)); 
        Guard.IsNotNullOrEmpty(locationId, nameof(locationId)); 
        Guard.IsNotNull(apiConfig, nameof(apiConfig));

        if (Int32.Parse(locationId) == 0)
            throw new FeatureNotEnabledException($"Location ID: {locationId} is zero and not valid (Is one not set and it went with the default?)"); 
	


        string productQuery = $"?filter.locationId={locationId}&filter.term={term}&filter.fulfillment=ais&filter.limit=50";

        Uri uri = new($"{apiConfig.KrogerUrl}products{productQuery}");

        _client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken.access_token);

        HttpResponseMessage res = await _client.GetAsync(uri);

        if (res.IsSuccessStatusCode)
        {
            string content = await res.Content.ReadAsStringAsync();
            var jsonRes = JsonSerializer.Deserialize<Root>(content);

            foreach(var jsonItem in jsonRes.data)
            {
                if (jsonItem.aisleLocations.Count > 0)
                {
                    //Change selection data matching to be more accurate depending on what we searched vs what the user input?
                    ItemLocationData returnILD = new()
                    {
                        BayNumber = jsonItem.aisleLocations.ElementAt(0).bayNumber ?? "0",
                        Description = jsonItem.aisleLocations.ElementAt(0).description ?? "No Description",
                        Number = jsonItem.aisleLocations.ElementAt(0).number ?? "0",
                        NumberOfFacing = jsonItem.aisleLocations.ElementAt(0).numberOfFacings ?? "0", 
                        Side =  jsonItem.aisleLocations.ElementAt(0).side ?? "L", 
                        ShelfNumber =  jsonItem.aisleLocations.ElementAt(0).shelfNumber ?? "0",
                        ShelfPositionInBay = jsonItem.aisleLocations.ElementAt(0).shelfPositionInBay ?? "0"
                    };

                    // Just packaging up some of the 'extra' data to pass back into the calling method's item.
                    Item item = new()
                    {
                        Name = term,
                        Description = jsonItem.description,
                        Category = jsonItem.categories[0],
                    };

                    if (jsonItem.items[0] is not null)
                    { 
                        if (jsonItem.items[0].price is not null)
                        { 
                            item.EstimatedPrice = jsonItem.items[0].price.promo; 

						    if (item.EstimatedPrice == 0m)
								item.EstimatedPrice = jsonItem.items[0].price.regular;
			
			            }
		            }

                    return (returnILD, item); 
                }
            }

            return (null, null); 
        }
        else
        {
            throw new Exception("Error encountered when grabbing product info: " + res.ReasonPhrase); 
        }
    }
}
