namespace ShoppingList.View;

public partial class SettingsView : ContentPage
{

	SettingsViewModel _svm;

	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
		_svm = settingsViewModel; 

	}

	private async void ThemeButtonPressed(object sender, EventArgs e)
	{
		var circularButton = sender as CircularButton;
		var themeName = ((TappedEventArgs)e).Parameter.ToString();
		
		await circularButton.BounceOnPressAsync();

		_svm.UpdatePrimaryColorPressed(themeName);

	}
}

