namespace ShoppingList.View;

public partial class StoreFinder : ContentPage
{
	StoreFinderViewModel _sfvm; 

	public StoreFinder(StoreFinderViewModel storeFinderViewModel)
	{
		InitializeComponent();
		BindingContext = storeFinderViewModel;
		_sfvm = storeFinderViewModel;

	}


	private void OnSearchButtonPressed(object sender, EventArgs e)
	{
		var zipCode = ((SearchBar)sender).Text;
		 _sfvm.DoSearchQuery(zipCode); 
		//_ulvm.ItemWasChecked(itemThatWasClicked);  
	}
}

