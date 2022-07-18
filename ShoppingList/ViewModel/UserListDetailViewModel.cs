using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class UserListDetailViewModel : BaseViewModel
{
    readonly ItemService _itemService;

    public UserListDetailViewModel()
    {
        _itemService = new();
    }

    [ObservableProperty]
    UserList userList;

    [ObservableProperty]
    List<Item> items; 

    
    [ICommand]
    public async void CreateItem(UserList ul)
    {
        if (IsBusy)
            return;

        await Shell.Current.GoToAsync($"{nameof(ItemInput)}", true,
            new Dictionary<string, object>
            {
                {"UserList", UserList}
            }); 
    }

    [ICommand]
    public async void GoBackToListScreen()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    public void RefreshUserListDetailScreen()
    {

        UserList.Items.Clear();

        UserList.Items = _itemService.GetUserListItems(UserList); 
    }

    [ICommand]
    public async void GoToItemDetail(Item item)
    {
        await Shell.Current.DisplayAlert(item.Name, $"Category: {item.Category} \nDescription: {item.Description} \n" +
            $"Aisle: {item.Aisle}", "Ok"); 
    }

    [ICommand]
    public void ItemWasChecked(Item item)
    { 
        _itemService.UpdateItem(item);
    }
    

}


