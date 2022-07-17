using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class UserListDetails : ContentPage
{
	int count = 0;

	public UserListDetails(UserListDetailViewModel userListDetailViewModel)
	{
		InitializeComponent();
		BindingContext = userListDetailViewModel;
		

	}
}

