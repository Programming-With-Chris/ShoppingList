using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class ItemDetail : ContentPage
{

	public ItemDetail(ItemDetailViewModel itemDetailViewModel)
	{
		InitializeComponent();
		BindingContext = itemDetailViewModel;

	}

/*	public void OnUserListNameCompleted(object sender, EventArgs e)
	{
		string name = ((Entry)sender).Text;

		Console.WriteLine(name);
		Shell.Current.DisplayAlert("Your list name:", name, "Ok"); 
	}*/
}

