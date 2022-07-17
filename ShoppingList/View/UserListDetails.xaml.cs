using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class UserListDetails : ContentPage
{
	public UserListDetails(UserListDetailViewModel userListDetailViewModel)
	{
		InitializeComponent();
		BindingContext = userListDetailViewModel;
		//userListDetailViewModel.RefreshUserListDetailScreen();

	}
	
	
}

