using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
[QueryProperty("Items", "Items")]
public partial class ItemInputViewModel : BaseViewModel
{
    readonly ItemService _itemService;
    readonly KrogerAPIService _krogerAPIService;

    [ObservableProperty]
    UserList userList;

    [ObservableProperty]
    public string itemName;

    [ObservableProperty]
    public string itemDescription;
    
    [ObservableProperty]
    public string itemCategory;

    [ObservableProperty]
    public string itemAisle;

    [ObservableProperty]
    public decimal itemEstimatedPrice; 

    [ObservableProperty]
    public int itemParentId;

    public ItemLocationData locationData; 


    public ItemInputViewModel()
    {
        _itemService = new();
        _krogerAPIService = new(); 
    }


    [RelayCommand]
    public async void OnItemEntryCompleted()
    {
        if (IsBusy)
           return;

        Item newItem = new()
        {
            Name = ItemName,
            Description = ItemDescription,
            Category = ItemCategory,    
            Aisle = ItemAisle,
            EstimatedPrice = ItemEstimatedPrice,
            ParentId = UserList.Id
        };

        var apiConfig = await _krogerAPIService.GetStartupConfig();

        var done = await _krogerAPIService.SetAuthTokens(apiConfig);
        
        //Will do this in background thread later
        var result =  await _krogerAPIService.GetProductLocationData(newItem.Name, Preferences.Get("KrogerLocation", "0000000"), apiConfig);

        ItemLocationData ild = result.Item1;
        Item item = result.Item2;

        newItem.LocationData = ild;
        newItem.Description = item.Description;
        newItem.Category = item.Category; 

        newItem.Aisle = ild.Description; 

        newItem = _itemService.CreateItem(newItem);

        newItem.LocationData.ParentId = newItem.Id; 
        UserList.Items.Add(newItem);

        ListSorter.SortUserListItems(UserList);
        ListSorter.SortUserListItems(userList); 

        await Shell.Current.GoToAsync($"{nameof(UserListDetails)}?", true,
            new Dictionary<string, object>
            {
                {"UserList", userList}
            }); 

    }

    [RelayCommand]
    public async void OnCancelButtonPressed()
    {

        await Shell.Current.GoToAsync($"{nameof(UserListDetails)}?", true,
            new Dictionary<string, object>
            {
                {"UserList", userList}
            }); 
    }
}