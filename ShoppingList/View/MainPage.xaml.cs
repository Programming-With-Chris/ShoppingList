using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(UserListViewModel ulViewModel)
	{
		InitializeComponent();
		BindingContext = ulViewModel;

	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
	}
}

