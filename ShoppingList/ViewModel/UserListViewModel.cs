using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("NewListId","id")]
public partial class UserListViewModel : BaseViewModel
{
   
    private UserListService _uls;
    private ItemService _itemService;
    private int _newListId; 

    public ObservableCollection<UserList> UserLists { get; } = new();

    //TODO there is probably a better way to do this? idk
    public int NewListId
    {
        get { return _newListId; }
        set
        {
            _newListId = value;
            UserList ul = new UserList()
            {
                Id = value
            };
            ul = _uls.GetUserListById(ul); 
            UserLists.Add(ul);
        }
    }  

    public UserListViewModel(UserListService uls)
    {
        _uls = uls;
        _itemService = new();
    }

    [ICommand]
    public void GetUserLists()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var userLists = _uls.GetUserLists();

            if (UserLists.Count != 0)
                UserLists.Clear(); 

            foreach (var userList in userLists)
            {
                UserLists.Add(userList); 
            }

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Shell.Current.DisplayAlert("Error!",
                $"Unable to get UserLists: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
    [ICommand]
    public async void CreateUserList()
    {
        if (IsBusy)
            return;


        await Shell.Current.GoToAsync($"{nameof(UserListDataInput)}", true,
            new Dictionary<string, object>
            {
                {"UserLists", UserLists}
            }); 
        
       /* try
        {
            IsBusy = true;
            UserList ul = new();
            ul.Name = "another test";
            ul.TargetStore = "chris's house"; 

            var userList = _uls.CreateUserList(ul);

            UserLists.Add(userList);

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Shell.Current.DisplayAlert("Error!",
                $"Unable to get UserLists: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }*/
    }

    [ICommand]
    public async void GoToListItems(UserList ul)
    {
        if (IsBusy || ul is null)
            return;
        try
        {
            ul.Items = _itemService.GetItemByParentId(ul);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Info","No Items Yet. Create Some!", "Ok");
            ul.Items.Clear(); 
        }
        await Shell.Current.GoToAsync($"{nameof(UserListDetails)}?id={ul.Id}", true,
            new Dictionary<string, object>
            {
                {"UserList", ul}
            }); 
    }
}