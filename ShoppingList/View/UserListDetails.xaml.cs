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
		//userListDetailViewModel.RefreshUserListDetailScreen();

	}

	public void OnCheckboxClicked(object sender, CheckedChangedEventArgs e )
	{
		Item itemThatWasClicked = (Item)((CheckBox)sender).BindingContext;
		itemThatWasClicked.IsCompleted = ((CheckBox)sender).IsChecked; 
		_ulvm.ItemWasChecked(itemThatWasClicked);  
	}
}

