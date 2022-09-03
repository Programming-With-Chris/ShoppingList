//using Microsoft.Maui.Graphics.Platform;

namespace ShoppingList.View;

public partial class UserListDetails : ContentPage
{
	UserListDetailViewModel _ulvm;

	public UserListDetails(UserListDetailViewModel userListDetailViewModel)
	{
		InitializeComponent();
		BindingContext = userListDetailViewModel;

		_ulvm = userListDetailViewModel;
		_ulvm.HasUndo = false; 

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		var undoButton = this.FindByName("UndoButton") as CircularButton; 
		undoButton?.FadeTo(0, 500);

    }

    private void OnCheckboxClicked(object sender, CheckedChangedEventArgs e)
	{

		CheckBox thisCheckbox = sender as CheckBox;

		Frame currentFrame = thisCheckbox.BindingContext as Frame;

		Item itemThatWasClicked = currentFrame.BindingContext as Item;


		if (thisCheckbox.IsChecked)
			currentFrame.FadeTo(.4, 1000);
		else
			currentFrame.FadeTo(1, 1000);

		itemThatWasClicked.IsCompleted = thisCheckbox.IsChecked;
		_ulvm.ItemWasChecked(itemThatWasClicked);

	}

	private async void NewItemButtonPressed(object sender, EventArgs e)
	{

		var circularButton = sender as CircularButton;
		await circularButton?.BounceOnPressAsync();
		_ulvm.NewItemDialog(); 
	}

	private async void UndoButtonPressed(object sender, EventArgs e)
	{

		var circularButton = sender as CircularButton;
		await circularButton.BounceOnPressAsync();
		_ulvm.UndoButtonPressed(); 
	}
	private async void FrameTapped(object sender, EventArgs e)
	{

		var frame = sender as Frame;
		await frame.ScaleTo(1.1, 100, Easing.BounceIn);
		await frame.ScaleTo(1.0, 75, Easing.BounceOut); 

		var item = ((TappedEventArgs)e).Parameter as Item; 
		_ulvm.GoToItemDetail(item); 
	}
}
