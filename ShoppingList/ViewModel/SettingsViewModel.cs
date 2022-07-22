﻿using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class SettingsViewModel : BaseViewModel
{


    public bool StartAtBackOfStore
    {
        get => Preferences.Get("StartAtBackOfStore", false);
        set
        {
            Preferences.Set("StartAtBackOfStore", value);
        }
    }
    

    public bool FrozenFoodLast
    {
        get => Preferences.Get("FrozenFoodLast", false);
        set
        {
            Preferences.Set("FrozenFoodLast", value);
        }
    }

    public string KrogerLocation
    {
        get => Preferences.Get("KrogerLocation", "N/A");
        set
        {
            Preferences.Set("KrogerLocation", value); 
        }
    }

    public SettingsViewModel()
    {
    }

    [ICommand]
    public async void GoBackToMain()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}"); 
    }

    [ICommand]
    public async void OpenLocationFinderDialog()
    {
        await Shell.Current.GoToAsync($"{nameof(StoreFinder)}"); 
    }
}