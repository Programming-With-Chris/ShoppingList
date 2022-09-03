using SkiaSharp.Extended.UI.Controls;

namespace ShoppingList.View;

public partial class MainPage : ContentPage
{
    readonly UserListViewModel _ulvm;

	public MainPage(UserListViewModel ulViewModel)
	{
		InitializeComponent();
		BindingContext = ulViewModel;
		_ulvm = ulViewModel;
		_ulvm.GetUserLists(); 

		// If the user just has 1 list, just go to that detail page, saving them a click
		if (_ulvm.UserLists.Count == 1)
			_ulvm.GoToListItems(_ulvm.UserLists[0]);

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_ulvm.GetUserLists();

		var cartLottie = this.FindByName("CartLottie") as SKLottieView;
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

