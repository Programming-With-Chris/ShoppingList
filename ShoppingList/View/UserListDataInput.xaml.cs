using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class UserListDataInput : ContentPage
{

	public UserListDataInput(UserListDataInputViewModel userListDataInputViewModel)
	{
		InitializeComponent();
		BindingContext = userListDataInputViewModel;

	}

/*	public void OnUserListNameCompleted(object sender, EventArgs e)
	{
		string name = ((Entry)sender).Text;

		Console.WriteLine(name);
		Shell.Current.DisplayAlert("Your list name:", name, "Ok"); 
	}*/
}

