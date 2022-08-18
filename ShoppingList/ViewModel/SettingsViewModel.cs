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

    [RelayCommand]
    public void UpdatePrimaryColorPressed(string colorName)
    {
        Color colorSelected;
        Color newPrimary;
        Color newSecondary;
        Color newTertiary;
        Color newBackground; 

        var wasSuccessfulColor = Color.TryParse(colorName, out colorSelected);
        var theme = Application.Current.RequestedTheme; 

        if (theme == AppTheme.Light)
        {

        }

        switch (colorName)
        {
            case "CadetBlue":
                newPrimary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newSecondary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newTertiary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newBackground = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                break;
            case "Pink":
                newPrimary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newSecondary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newTertiary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newBackground = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                break;
            case "MediumPurple":
                newPrimary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newSecondary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newTertiary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newBackground = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                break;
            case "WhiteSmoke":
                newPrimary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newSecondary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newTertiary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newBackground = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                break;
            default:
                newPrimary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newSecondary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newTertiary = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                newBackground = theme == AppTheme.Light ? Colors.CadetBlue : Colors.CadetBlue;
                break;

        }
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            foreach(ResourceDictionary dictionaries in mergedDictionaries)
            {
                dictionaries["Primary"] = newPrimary;
                dictionaries["Secondary"] = newSecondary;
                dictionaries["Tertiary"] = newTertiary;
                dictionaries["BackgroundLight"] = newBackground;

            }
        }

    }
}