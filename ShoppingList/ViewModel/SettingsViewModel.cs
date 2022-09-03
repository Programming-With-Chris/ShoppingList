using System.Collections.ObjectModel;
using System.Diagnostics;
using ShoppingList.View;

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

    [RelayCommand]
    public void UpdatePrimaryColorPressed(string themeName)
    {
        Preferences.Set("Theme", themeName); 

        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            foreach(ResourceDictionary dictionaries in mergedDictionaries)
            {
                var primaryFound = dictionaries.TryGetValue(themeName + "Primary", out var primary);
                if (primaryFound)
                    dictionaries["Primary"] = primary; 

                var secondaryFound = dictionaries.TryGetValue(themeName + "Secondary", out var secondary);
                if (secondaryFound)
                    dictionaries["Secondary"] = secondary; 

                var tertiaryFound = dictionaries.TryGetValue(themeName + "Tertiary", out var tertiary);
                if (tertiaryFound)
                    dictionaries["Tertiary"] = tertiary; 

                var accentFound = dictionaries.TryGetValue(themeName + "Accent", out var accent);
                if (accentFound)
                    dictionaries["Accent"] = accent; 
                
                var darkAccentFound = dictionaries.TryGetValue(themeName + "DarkAccent", out var darkAccent);
                if (darkAccentFound)
                    dictionaries["DarkAccent"] = darkAccent; 


            }
        }

    }
}