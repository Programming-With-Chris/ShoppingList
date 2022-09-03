using ShoppingList.Model.Api;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ShoppingList.View;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class StoreFinderViewModel : BaseViewModel
{
    KrogerAPIService _kapis;

    [ObservableProperty]
    Dictionary<string, string> locations;


    public ObservableCollection<string> StoreNames { get; } = new();

    [ObservableProperty]
    string zipSearched; 


    public StoreFinderViewModel(KrogerAPIService krogerAPIService)
    {
        _kapis = krogerAPIService;
    }

    [RelayCommand]
    public async void GoBackToMain()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}"); 
    }

    [RelayCommand]
    public async void DoSearchQuery(string zipcode)
    {
        ApiConfig apiconfig = await _kapis.GetStartupConfigAsync();
        var done = await _kapis.SetAuthTokensAsync(apiconfig);

        try
        {
            locations = await _kapis.GetLocationNearZipAsync(zipcode, apiconfig);

            if (StoreNames.Count > 0)
                StoreNames.Clear();

            foreach (var location in locations.Values.ToList<string>())
            {
                StoreNames.Add(location); 
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("None Found", e.Message, "Ok");
            return; 
        }

    }
    [RelayCommand]
    public async void SetUserKroger(string selectedStoreName)
    {
        var locationId = locations.FirstOrDefault(x => x.Value == selectedStoreName).Key;

        Preferences.Set("KrogerStoreName", selectedStoreName);
        Preferences.Set("KrogerLocation", locationId);

        await Shell.Current.GoToAsync($"..?KrogerLocation={locationId}&KrogerStoreName={selectedStoreName}");
    }

    [RelayCommand]
    public async void CancelQuery()
    {
        await Shell.Current.GoToAsync($".."); 
    }
}