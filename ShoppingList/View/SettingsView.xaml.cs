using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class SettingsView : ContentPage
{

	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;

	}
}

