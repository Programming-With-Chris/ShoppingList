using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
[QueryProperty("KrogerLocation", "KrogerLocation")]
[QueryProperty("KrogerStoreName", "KrogerStoreName")]
public partial class SettingsViewModel : BaseViewModel
{

    public bool StartAtBackOfStore
    {
        get => Preferences.Get("StartAtBackOfStore", false);
        set
        {
            Preferences.Set("StartAtBackOfStore", value);
            OnPropertyChanged(nameof(StartAtBackOfStore)); 
        }
    }
    

    public bool FrozenFoodLast
    {
        get => Preferences.Get("FrozenFoodLast", false);
        set
        {
            Preferences.Set("FrozenFoodLast", value);
            OnPropertyChanged(nameof(FrozenFoodLast));
        }
    }

    public string KrogerLocation
    {
        get => Preferences.Get("KrogerLocation", "N/A");
        set
        {
            Preferences.Set("KrogerLocation", value);
            OnPropertyChanged(nameof(KrogerLocation)); 
        }
    }


    public string KrogerStoreName
    {
        get => Preferences.Get("KrogerStoreName", "N/A");
        set
        {
            Preferences.Set("KrogerStoreName", value);
            OnPropertyChanged(nameof(KrogerStoreName)); 
        }
    }

    [RelayCommand]
    public async void GoBackToMain()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}"); 
    }

    [RelayCommand]
    public async void OpenLocationFinderDialog()
    {
        await Shell.Current.GoToAsync($"{nameof(StoreFinder)}"); 
    }

    [RelayCommand]
    public async void CancelQuery()
    {
        await Shell.Current.GoToAsync($".."); 
    }
}