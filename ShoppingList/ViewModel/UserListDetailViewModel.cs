using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class UserListDetailViewModel : BaseViewModel
{
    readonly ItemService _itemService;
    readonly KrogerAPIService _krogerAPIService;

    public UserListDetailViewModel()
    {
        _itemService = new();
        _krogerAPIService = new();
        newItemName = " + ";

    }

    UserList userList;

    [ObservableProperty]
    string newItemName; 


    public UserList UserList
    {
        get => userList;
        set
        {
            userList = value;
            ListSorter.SortUserListItems(userList);
            Title = UserList.Name; 
            OnUserListChanged(value); 
            OnPropertyChanged(nameof(UserList));
            OnPropertyChanged(nameof(UserList.Items));
        }
    }

    [ObservableProperty]
    public bool isRefreshing; 

    [RelayCommand]
    public async void CreateItem(UserList ul)
    {
        if (IsBusy)
            return;

        var newItem = new Item();
        newItem.Name = "new item"; 
        ul.Items.Add(newItem);
        ul = ul;  

        /*await Shell.Current.GoToAsync($"{nameof(ItemInput)}", true,
            new Dictionary<string, object>
            {
                {"UserList", userList}
            }); */
    }

    [RelayCommand]
    public async void GoBackToListScreen()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    public void OnUserListChanged(UserList value)
    {
        ListSorter.SortUserListItems(userList); 
    }

    [RelayCommand]
    public void RefreshUserListDetailScreen()
    {

        IsRefreshing = true;
        UserList.Items.Clear();

        UserList.Items = _itemService.GetUserListItems(UserList); 

        ListSorter.SortUserListItems(userList); 

        // Forces CollectionView to update on refresh, otherwise, it doesn't work!
        UserList = UserList; 

        IsRefreshing = false; 
    }

    [RelayCommand]
    public async void GoToItemDetail(Item item)
    {
        await Shell.Current.DisplayAlert(item.Name, $"Category: {item.Category} \nDescription: {item.Description} \n" +
            $"Aisle: {item.Aisle}", "Ok"); 
    }

    [RelayCommand]
    public void ItemWasChecked(Item item)
    { 
        _itemService.UpdateItem(item);

        UserList.Items = ListSorter.SortUserListItems(userList);

        //This forces on check to refresh collection view, but breaks opening a list for some reason
        //UserList = UserList; 

    }

    [RelayCommand]
    public void DeleteItem(Item item)
    {
        _itemService.DeleteItem(item);
        UserList.Items.Remove(item); 

        UserList.Items = ListSorter.SortUserListItems(userList);

        UserList = UserList; 

    }

    [RelayCommand]
    public async void NewItemDialog()
    {
        if (IsBusy)
            return;

        IsBusy = true; 

        string result = await Shell.Current.DisplayPromptAsync("New Item", "Enter The New Item:");

        if (result is not null)
            OnItemEntryCompleted(result);

        IsBusy = false; 
    }

    [RelayCommand]
    public async void OnItemEntryCompleted(string itemName)
    {

        Item newItem = new()
        {
            Name = itemName
        };

        var apiConfig = await _krogerAPIService.GetStartupConfigAsync();

        var done = await _krogerAPIService.SetAuthTokensAsync(apiConfig);

        //Will do this in background thread later
        var result = await _krogerAPIService.GetProductLocationDataAsync(newItem.Name, Preferences.Get("KrogerLocation", "0000000"), apiConfig);

        ItemLocationData ild = result.Item1;
        Item item = result.Item2;

        newItem.LocationData = ild;
        newItem.Description = item.Description;
        newItem.Category = item.Category;

        newItem.Aisle = ild.Description;
        newItem.ParentId = UserList.Id;
        
        newItem = _itemService.CreateItem(newItem);

        newItem.LocationData.ParentId = newItem.Id;
        UserList.Items.Add(newItem);

        ListSorter.SortUserListItems(userList);


        UserList = UserList;

    }
}


