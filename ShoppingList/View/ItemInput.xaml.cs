using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class ItemInput : ContentPage
{

	public ItemInput(ItemInputViewModel itemInputViewModel)
	{
		InitializeComponent();
		BindingContext = itemInputViewModel;

	}

/*	public void OnUserListNameCompleted(object sender, EventArgs e)
	{
		string name = ((Entry)sender).Text;

		Console.WriteLine(name);
		Shell.Current.DisplayAlert("Your list name:", name, "Ok"); 
	}*/
}

