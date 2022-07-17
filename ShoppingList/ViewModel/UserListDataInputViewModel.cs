using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class UserListDataInputViewModel : BaseViewModel
{
    readonly UserListService _userListService;

    [ObservableProperty]
    UserList userList;

    [ObservableProperty]
    public string ulName;

    [ObservableProperty]
    public string ulTargetStore;

    public UserListDataInputViewModel()
    {
        _userListService = new();
    }


    [ICommand]
    public async void OnUserListCompleted()
    {
        // if (IsBusy)
        //   return;

        //string name = "Test Name";// ((Entry)sender).Text;
        userList = new(); 

        userList.Name = UlName;
        userList.TargetStore = ulTargetStore; 

        userList = _userListService.CreateUserList(userList);

        await Shell.Current.GoToAsync("..?id=" + userList.Id + "&createflag=true"); 
        //await Shell.Current.DisplayAlert("Test alert", "Information recieved :" + UlName + ", " + UlTargetStore, "Ok");

    }

}