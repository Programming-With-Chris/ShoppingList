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
    public void CreateItem(UserList ul)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Item newItem = new()
            {
                Name = "testitem1",
                Category = "bbq",
                Aisle = "B4",
                Description = "a new test item! So cool!",
                EstimatedPrice = 10.00m, 
                ParentId = ul.Id
            }; 

            var item = _itemService.CreateItem(newItem);

            userList.Items.Add(item);

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Shell.Current.DisplayAlert("Error!",
                $"Unable to add new Item!: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }



    

}


