namespace ShoppingList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(UserListDetails), typeof(UserListDetails)); 
		Routing.RegisterRoute(nameof(UserListDataInput), typeof(UserListDataInput));
		Routing.RegisterRoute(nameof(ItemInput), typeof(ItemInput));
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
	}
}
