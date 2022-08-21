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

	public async void NewListButtonPressed(object sender, EventArgs e)
	{

		var circularButton = sender as CircularButton;
		await circularButton.BounceOnPressAsync();

		_ulvm.CreateUserList(); 

    }
}

