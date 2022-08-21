using ShoppingList.ViewModels;
using ShoppingList.Controls;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
	UserListViewModel _ulvm;
	public MainPage(UserListViewModel ulViewModel)
	{
		InitializeComponent();
		BindingContext = ulViewModel;
		_ulvm = ulViewModel;
		_ulvm.GetUserLists(); 

	}

	private async void NewListButtonPressed(object sender, EventArgs e)
	{

		var circularButton = sender as CircularButton;
		await circularButton.BounceOnPressAsync();

		_ulvm.CreateUserList(); 

    }
	private async void FrameTapped(object sender, EventArgs e)
	{

		var frame = sender as Frame;
		await frame.ScaleTo(1.1, 75, Easing.BounceIn);
		await frame.ScaleTo(1.0, 75, Easing.BounceOut); 

		var userList = ((TappedEventArgs)e).Parameter as UserList; 

		_ulvm.GoToListItems(userList); 

    }
}

