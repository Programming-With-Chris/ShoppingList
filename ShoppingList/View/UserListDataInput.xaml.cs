using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class UserListDataInput : ContentPage
{
	private UserListDataInputViewModel _uldiv = new();

	public UserListDataInput(UserListDataInputViewModel userListDataInputViewModel)
	{
		InitializeComponent();
		BindingContext = userListDataInputViewModel;
		_uldiv = userListDataInputViewModel; 

	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = sender as Picker;


		_uldiv.UserListType = (UserList.ListType) picker.SelectedIndex; 

	}
}

