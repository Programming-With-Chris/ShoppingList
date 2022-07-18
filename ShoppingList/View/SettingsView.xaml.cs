using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class SettingsView : ContentPage
{

	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;

	}

/*	public void OnUserListNameCompleted(object sender, EventArgs e)
	{
		string name = ((Entry)sender).Text;

		Console.WriteLine(name);
		Shell.Current.DisplayAlert("Your list name:", name, "Ok"); 
	}*/
}

