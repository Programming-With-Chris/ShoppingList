using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
	public MainPage(UserListViewModel ulViewModel)
	{
		InitializeComponent();
		BindingContext = ulViewModel;
		ulViewModel.GetUserLists(); 

	}
}

