using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class SettingsViewModel : BaseViewModel
{


    //private bool _startAtBackOfStore; 

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

    public SettingsViewModel()
    {
    }

}