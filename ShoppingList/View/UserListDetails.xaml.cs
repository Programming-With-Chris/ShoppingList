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

		_ulvm.Items = new(); 
	}

	public void OnCheckboxClicked(object sender, CheckedChangedEventArgs e )
	{
		CheckBox thisCheckbox = (CheckBox)sender;

		Frame currentFrame = (Frame)(thisCheckbox).BindingContext;

		Item itemThatWasClicked = (Item)(currentFrame).BindingContext;



		if (thisCheckbox.IsChecked)
			currentFrame.FadeTo(.4, 1000);
		else
			currentFrame.FadeTo(1, 1000); 

		itemThatWasClicked.IsCompleted = ((CheckBox)sender).IsChecked; 
		_ulvm.ItemWasChecked(itemThatWasClicked);
		 
    }
}

