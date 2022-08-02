using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class UserListDetails : ContentPage
{
	UserListDetailViewModel _ulvm; 

	public UserListDetails(UserListDetailViewModel userListDetailViewModel)
	{
		InitializeComponent();
		BindingContext = userListDetailViewModel;
		_ulvm = userListDetailViewModel;

	}

	public void OnCheckboxClicked(object sender, CheckedChangedEventArgs e )
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
}

