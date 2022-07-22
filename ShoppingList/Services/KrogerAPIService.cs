using System.Text.Json;
using ShoppingList.Model.Api;

namespace ShoppingList.Services;

public class KrogerAPIService
{
    HttpClient _client;
    List<string> config;
    AccessTokenRes accessToken; 

    public KrogerAPIService()
    {
        _client = new HttpClient();

    }

    public async Task<Dictionary<string, string>> GetLocationNearZip(string zip, ApiConfig apiConfig)
    {
        Dictionary<string, string> closeKrogerNames = new Dictionary<string, string>(); 

        string zipQuery = $"?filter.zipCode.near={zip}";
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
            return closeKrogerNames; 
        }
        else
        {
            throw new Exception("No Krogers found within a 10 mile radius"); 
        }
    }


    public async Task<ApiConfig> GetStartupConfig()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("krogerapiconfig.json");
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var output = JsonSerializer.Deserialize<ApiConfig>(json);
        return output;
    }


    public async Task<bool> SetAuthTokens(ApiConfig apiConfig)
    {
        var authData = $"{apiConfig.ClientId}:{apiConfig.ClientSecret}";
        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

        _client.DefaultRequestHeaders.Authorization = new("Basic", authHeaderValue);
        

        StringContent content = new("grant_type=client_credentials", Encoding.ASCII, "application/x-www-form-urlencoded"); 
        
        HttpResponseMessage res = await _client.PostAsync(apiConfig.KrogerUrl + "connect/oauth2/token", content); 

        if (res.IsSuccessStatusCode)
        {
            var resCont = await res.Content.ReadAsStringAsync();
            accessToken = JsonSerializer.Deserialize<AccessTokenRes>(resCont);

            return true; 
        } else
        {
            throw new Exception("Unable to Authenticate, Sorting Functionality Offline"); 
        }

    }

}
