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
        Items = new();
        //Title = UserList.Name; 
    }

    UserList userList;

    public ObservableCollection<Item> Items { get; set; } 

    public UserList UserList
    {
        get => userList;
        set
        {
            userList = value;
            ListSorter.SortUserListItems(userList);
            Items.Clear(); 
            foreach (var item in userList.Items)
            {
                Items.Add(item); 
            }
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

        await Shell.Current.GoToAsync($"{nameof(ItemInput)}", true,
            new Dictionary<string, object>
            {
                {"UserList", userList}
            }); 
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

        /*var apiConfig = await _krogerAPIService.GetStartupConfig();

        var done = await _krogerAPIService.SetAuthTokens(apiConfig);

        foreach (var item in UserList.Items)
        {
            ItemLocationData ild = await _krogerAPIService.GetProductInfo(item.Name, Preferences.Get("KrogerLocation", "0000000"), apiConfig);
            item.LocationData = ild;
        }*/

        ListSorter.SortUserListItems(userList); 
        ListSorter.SortUserListItems(UserList);

        Items.Clear(); 
        foreach (var item in userList.Items)
        {
            Items.Add(item); 
        }

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

        //ListSorter.SortUserListItems(userList);
        //ListSorter.SortUserListItems(UserList);
        UserList.Items = ListSorter.SortUserListItems(userList);

        Items.Clear(); 
        foreach (var ulItem in userList.Items)
        {
            Items.Add(ulItem); 
        }

        //OnPropertyChanged(nameof(UserList));
        //var newUserList = UserList;
        //UserList = newUserList; 
        //UserList = new UserList(userList); 
    }

    [RelayCommand]
    public void DeleteItem(Item item)
    {
        _itemService.DeleteItem(item);
        UserList.Items.Remove(item); 

        UserList.Items = ListSorter.SortUserListItems(userList);

        UserList = UserList; 

    }
}


