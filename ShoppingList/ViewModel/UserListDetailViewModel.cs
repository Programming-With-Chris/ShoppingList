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
    }

    UserList userList;


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
}


