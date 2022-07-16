using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels; 

public partial class UserListViewModel : BaseViewModel
{
   
    private UserListService _uls;
    public ObservableCollection<UserList> UserLists { get; } = new(); 


    public UserListViewModel(UserListService uls)
    {
        _uls = uls;
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
    public void CreateUserList()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            UserList ul = new();
            ul.Name = "another test";
            ul.TargetStore = "chris's house"; 

            var userList = _uls.CreateUserList(ul);

            if (UserLists.Count != 0)
                UserLists.Clear();

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
        }
    }
}