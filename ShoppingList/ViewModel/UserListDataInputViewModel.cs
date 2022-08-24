using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class UserListDataInputViewModel : BaseViewModel
{
    readonly UserListService _userListService;
    readonly ItemService _itemService; 

    [ObservableProperty]
    UserList userList;

    [ObservableProperty]
    public string ulName;

    [ObservableProperty]
    public string ulTargetStore;

    public UserListDataInputViewModel()
    {
        _userListService = new();
        _itemService = new(); 
    }

    public List<string> TypeList
    {
        get
        {
            return Enum.GetNames(typeof(UserList.ListType)).ToList(); 
        }
    }

    [ObservableProperty]
    public bool prepopulateList; 

    [ObservableProperty]
    private UserList.ListType userListType; 


    [RelayCommand]
    public async void OnUserListCompleted()
    {
        userList = new(); 

        userList.Name = UlName;
        userList.TargetStore = ulTargetStore;
        userList.Type = UserListType; 

        
        userList = _userListService.CreateUserList(userList);

        if (PrepopulateList)
        {
            UserList lastListOfThatType = _userListService.GetLastUserListOfType(userList);
            lastListOfThatType.Items = _itemService.GetUserListItems(lastListOfThatType); 
            
            foreach(var item in lastListOfThatType.Items)
            {
                item.IsCompleted = false; 
                userList.Items.Add(item);
                item.ParentId = userList.Id; 
                _itemService.CreateItem(item); 
            }
        }


        await Shell.Current.GoToAsync("..?id=" + userList.Id + "&createflag=true"); 

    }


    [RelayCommand]
    public async void OnCancel()
    {

        await Shell.Current.GoToAsync("..?createflag=false"); 

    }
}