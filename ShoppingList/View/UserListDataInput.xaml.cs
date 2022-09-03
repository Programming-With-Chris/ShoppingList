namespace ShoppingList.View;

public partial class UserListDataInput : ContentPage
{
	private UserListDataInputViewModel _uldiv = new();

	public UserListDataInput(UserListDataInputViewModel userListDataInputViewModel)
	{
		InitializeComponent();
		BindingContext = userListDataInputViewModel;
		_uldiv = userListDataInputViewModel;

		this.TypeOfListPicker.SelectedIndex = 0; 

	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = sender as Picker;


		_uldiv.UserListType = (UserList.ListType) picker.SelectedIndex; 

	}

	private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{

		var checkbox = sender as CheckBox;
		_uldiv.PrepopulateList = checkbox.IsChecked; 
	}

	private async void NewItemButtonPressed(object sender, EventArgs e)
	{
		var circularButton = sender as CircularButton;
		await circularButton.BounceOnPressAsync();
		_uldiv.OnUserListCompleted(); 

    }
	private async void CancelButtonPressed(object sender, EventArgs e)
	{
		var circularButton = sender as CircularButton;
		await circularButton.BounceOnPressAsync();
		_uldiv.OnCancel(); 

    }
}

