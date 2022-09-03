using System.Timers;
using System.Diagnostics;
using ShoppingList.View;

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

        HasUndo = false;

        OnPropertyChanged(nameof(HasUndo)); 
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

            if (UserList.Name.Length > 10)
                Title = $"{UserList.Name.Substring(0, 10)}...        - est. price: {UserList.TotalPrice}"; 
            else
                Title = $"{UserList.Name}...        - est. price: {UserList.TotalPrice}"; 

            OnUserListChanged(value); 
            OnPropertyChanged(nameof(UserList));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(UserList.Items));
        }
    }

    [ObservableProperty]
    public bool isRefreshing;

    [ObservableProperty]
    public bool hasUndo = false;


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
            $"Aisle: {item.Aisle} \n Estimated Price: {item.EstimatedPrice}", "Ok"); 
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


        try { 
	
			var apiConfig = await _krogerAPIService.GetStartupConfigAsync();

			var done = await _krogerAPIService.SetAuthTokensAsync(apiConfig);

			var result = await _krogerAPIService.GetProductLocationDataAsync(itemName, Preferences.Get("KrogerLocation", "0000000"), apiConfig);

			ItemLocationData ild = result.Item1;
			Item item = result.Item2;


            item.ParentId = UserList.Id;

            var newItem = new Item(item, ild); 

			newItem = _itemService.CreateItem(newItem); 
			newItem.LocationData.ParentId = newItem.Id;
			UserList.Items.Add(newItem);

			ListSorter.SortUserListItems(userList);

			UserListNotifers();
	
	
	    } catch(FeatureNotEnabledException e) //this is probably a misuse of this exception type, but oh well, it kinda fits lol
        {
            var promptedZipNotSetYet = Preferences.Get("FirstTimePromptForZip", true); 
	
            if (promptedZipNotSetYet)
            {   
		        var goToSettings = await Shell.Current.DisplayAlert("Error!",
			    $"Unable To Get Kroger Data, You Need To Set Your Kroger Location In The Settings. Go There Now?", "Ok", "Cancel");

		        if (goToSettings)
		        {
			        await Shell.Current.GoToAsync($"{nameof(SettingsView)}"); 
			    } else {

                    var newItem = new Item()
                    {
                        Name = itemName,
                        ParentId = UserList.Id,
                        LocationData = new ItemLocationData()
                    }; 

			        newItem = _itemService.CreateItem(newItem); 
			        newItem.LocationData.ParentId = newItem.Id;
			        UserList.Items.Add(newItem);

			        ListSorter.SortUserListItems(userList);

			        UserListNotifers();

			    }
	        } else
            {
                var newItem = new Item()
                {
                    Name = itemName,
                    ParentId = UserList.Id,
                    LocationData = new ItemLocationData()
                };

                newItem = _itemService.CreateItem(newItem);
                newItem.LocationData.ParentId = newItem.Id;
                UserList.Items.Add(newItem);

                ListSorter.SortUserListItems(userList);

                UserListNotifers();

            }

            Preferences.Set("FirstTimePromptForZip", false); 


        }
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
        if (UserList.Name.Length > 10)
            Title = $"{UserList.Name.Substring(0, 10)}...        - est. price: {UserList.TotalPrice}"; 
        else   
	         Title = $"{UserList.Name}...        - est. price: {UserList.TotalPrice}"; 

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


