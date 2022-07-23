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


    [RelayCommand]
    public async void OnUserListCompleted()
    {
        userList = new(); 

        userList.Name = UlName;
        userList.TargetStore = ulTargetStore; 

        userList = _userListService.CreateUserList(userList);

        await Shell.Current.GoToAsync("..?id=" + userList.Id + "&createflag=true"); 

    }


    [RelayCommand]
    public async void OnCancel()
    {

        await Shell.Current.GoToAsync("..?createflag=false"); 

    }
}