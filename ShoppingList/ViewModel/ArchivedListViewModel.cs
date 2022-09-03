using System.Collections.ObjectModel;
using System.Diagnostics;
using ShoppingList.Model.Api;
using ShoppingList.View;

namespace ShoppingList.ViewModels;

public partial class ArchivedListViewModel : BaseViewModel
{
   
    private UserListService _uls;
    private ItemService _itemService;

    public ObservableCollection<UserList> UserLists { get; } = new();

    public bool CreateFlag { get; set; } = false; 

    public ArchivedListViewModel(UserListService userListService, ItemService itemService)
    {
        _uls = userListService;
        _itemService = itemService;
        Title = "My Archived Lists"; 
    }

    [RelayCommand]
    public void GetArchivedLists()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var userLists = _uls.GetArchivedUserLists();

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
    [RelayCommand]
    public async void CreateUserList()
    {
        if (IsBusy)
            return;


        await Shell.Current.GoToAsync($"{nameof(UserListDataInput)}", true,
            new Dictionary<string, object>
            {
                {"UserLists", UserLists}
            }); 
        
    }

    [RelayCommand]
    public async void RestoreUserList(UserList restoreList)
    {
        if (IsBusy)
            return; 

        bool shouldRestore =   await Shell.Current.DisplayAlert("Restore List?", "Would You Like To Restore This List?",
	                                                            "Yes", "Cancel");

        if (shouldRestore)
            _uls.UnarchiveUserList(restoreList);


        GetArchivedLists(); 
    }
}