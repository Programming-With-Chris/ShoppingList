using System.Timers;

namespace ShoppingList.ViewModels;

[QueryProperty("UserList", "UserList")]
public partial class UserListDetailViewModel : BaseViewModel
{
    readonly ItemService _itemService;
    readonly KrogerAPIService _krogerAPIService;
    
    System.Timers.Timer undoTimer; 
    Stack<Item> undoItemBuffer; 

    public UserListDetailViewModel(ItemService itemService, KrogerAPIService krogerAPIService)
    {
        _itemService = itemService;
        _krogerAPIService = krogerAPIService;
        undoItemBuffer = new Stack<Item>();

        Console.Write("Test Output");

        undoTimer = new System.Timers.Timer(5000);
        undoTimer.Elapsed += new ElapsedEventHandler(UndoTimerTick); 
    }

    UserList userList;

    [ObservableProperty]
    string newItemName;


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

    [ObservableProperty]
    public bool hasUndo;


    [RelayCommand]
    public async void GoBackToListScreen()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        //await Shell.Current.GoToAsync($"..");
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

        UserListNotifers();

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

        //UserListNotifers();

    }


    [RelayCommand]
    public async void NewItemDialog()
    {
        if (IsBusy)
            return;

        IsBusy = true; 

        string result = await Shell.Current.DisplayPromptAsync("New Item", "Enter The New Item:");

        if (result is not null)
            OnItemEntryCompleted(result);

        IsBusy = false; 
    }

    [RelayCommand]
    public async void OnItemEntryCompleted(string itemName)
    {

        Item newItem = new()
        {
            Name = itemName
        };

        var apiConfig = await _krogerAPIService.GetStartupConfigAsync();

        var done = await _krogerAPIService.SetAuthTokensAsync(apiConfig);

        //Will do this in background thread later
        var result = await _krogerAPIService.GetProductLocationDataAsync(newItem.Name, Preferences.Get("KrogerLocation", "0000000"), apiConfig);

        ItemLocationData ild = result.Item1;
        Item item = result.Item2;

        newItem.LocationData = ild;
        newItem.Description = item.Description;
        newItem.Category = item.Category;

        newItem.Aisle = ild.Description;
        newItem.ParentId = UserList.Id;
        
        newItem = _itemService.CreateItem(newItem);

        newItem.LocationData.ParentId = newItem.Id;
        UserList.Items.Add(newItem);

        ListSorter.SortUserListItems(userList);


        //UserList = UserList;
        UserListNotifers();

    }

    [RelayCommand]
    public void DeleteItem(Item item)
    {
        undoItemBuffer.Push(item); 

        _itemService.DeleteItem(item);
        UserList.Items.Remove(item); 

        UserList.Items = ListSorter.SortUserListItems(userList);


        HasUndo = true; 
        // We stop and restart the timer in case they delete things back to back, it won't take 
        // away the button after 5 seconds from the first delete
        undoTimer.Stop();
        undoTimer.Start();

        UserListNotifers(); 
    }

    [RelayCommand]
    public void UndoButtonPressed()
    {
        //restart the timer on press, to extend the time they can press it
        undoTimer.Stop();
        undoTimer.Start();


        Item undoneItem; 
        var wasUndone = undoItemBuffer.TryPop(out undoneItem); 

        if (wasUndone)
        {
            undoneItem = _itemService.CreateItem(undoneItem);
            undoneItem.LocationData.ParentId = undoneItem.Id;

            UserList.Items.Add(undoneItem);
            UserListNotifers(); 
	    }
        else
        { 
	        //maybe an error toast or something here
	    }

    
    }

    private void UserListNotifers()
    {
        OnUserListChanged(UserList);
        OnPropertyChanged(nameof(UserList));
        OnPropertyChanged(nameof(UserList.Items));
    }

    private void UndoTimerTick(object sender, EventArgs e)
    {
        HasUndo = false;
        undoTimer.Stop();
    
    }

    

}


