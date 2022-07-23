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

    public async Task<ApiConfig> GetStartupConfig()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("krogerapiconfig-test.json");
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var output = JsonSerializer.Deserialize<ApiConfig>(json);
        return output;
    }

    public async Task<bool> SetAuthTokens(ApiConfig apiConfig)
    {

        if (accessToken is not null && DateTime.Now < expireTime)
            return false; 

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

    public async Task<Dictionary<string, string>> GetLocationNearZip(string zip, ApiConfig apiConfig)
    {
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




    public async Task<ShoppingList.Model.ItemLocationData> GetProductInfo(string term, string locationId, ApiConfig apiConfig)
    {
        string productQuery = $"?filter.locationId={locationId}&filter.term={term}&filter.fulfillment=ais&filter.limit=50";

        Uri uri = new($"{apiConfig.KrogerUrl}products{productQuery}");

        _client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken.access_token);

        HttpResponseMessage res = await _client.GetAsync(uri);

        if (res.IsSuccessStatusCode)
        {
            string content = await res.Content.ReadAsStringAsync();
            var jsonRes = JsonSerializer.Deserialize<Root>(content);

            foreach(var item in jsonRes.data)
            {
                if (item.aisleLocations.Count > 0)
                {
                    //Change selection data matching to be more accurate depending on what we searched vs what the user input?
                     ShoppingList.Model.ItemLocationData returnItem = new()
                    {
                        BayNumber = item.aisleLocations.ElementAt(0).bayNumber,
                        Description = item.aisleLocations.ElementAt(0).description,
                        Number = item.aisleLocations.ElementAt(0).number,
                        NumberOfFacing = item.aisleLocations.ElementAt(0).numberOfFacings, 
                        Side =  item.aisleLocations.ElementAt(0).side, 
                        ShelfNumber =  item.aisleLocations.ElementAt(0).shelfNumber,
                        ShelfPositionInBay = item.aisleLocations.ElementAt(0).shelfPositionInBay
                    };

                    return returnItem; 
                }
            }

            return null; 
        }
        else
        {
            throw new Exception("Error encountered when grabbing product info: " + res.ReasonPhrase); 
        }
    }
}
