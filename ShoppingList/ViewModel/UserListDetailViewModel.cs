using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
//[QueryProperty("CreateFlag", "createflag")]
//[QueryProperty("NewItemId", "id")]
public partial class UserListDetailViewModel : BaseViewModel
{
    readonly ItemService _itemService;
    int _newItemId; 

    public UserListDetailViewModel()
    {
        _itemService = new();
    }

    [ObservableProperty]
    UserList userList;

    [ObservableProperty]
    List<Item> items; 


    //public bool CreateFlag { get; set; } = false; 

    //TODO there is probably a better way to do this? idk
   /* public int NewItemId
    {
        get { return _newItemId; }
        set
        {
            if (CreateFlag)
            {
                _newItemId = value;
                Item newItem = new()
                {
                    Id = value
                };
                newItem = _itemService.GetItemById(newItem);
                UserList.Items.Add(newItem);
                CreateFlag = false; 
            }
        }
    }*/  
    
    
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
        //Shell.Current.GoToAsync("..?createflag=false"); 
        /*await Shell.Current.GoToAsync("..?createflag=false", true,
            new Dictionary<string, object>
            {
                {"UserList", userList}
            });*/
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    public void RefreshUserListDetailScreen()
    {

        UserList.Items.Clear();

        UserList.Items = _itemService.GetUserListItems(UserList); 
    }
    

}


