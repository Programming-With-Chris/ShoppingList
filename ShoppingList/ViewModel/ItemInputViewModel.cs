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

    public ObservableCollection<Item> Items { get; set; } = new();     

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
        
        ItemLocationData ild = await _krogerAPIService.GetProductInfo(newItem.Name, Preferences.Get("KrogerLocation", "0000000"), apiConfig);

        newItem.LocationData = ild;

        newItem.Aisle = ild.Description; 

        newItem = _itemService.CreateItem(newItem);


        UserList.Items.Add(newItem);

        ListSorter.SortUserListItems(UserList);
        ListSorter.SortUserListItems(userList); 


        Items.Clear(); 
        foreach (var item in UserList.Items)
        {
            Items.Add(item);
        }

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