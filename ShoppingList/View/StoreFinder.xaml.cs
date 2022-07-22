using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class StoreFinder : ContentPage
{
	StoreFinderViewModel _sfvm; 

	public StoreFinder(StoreFinderViewModel storeFinderViewModel)
	{
		InitializeComponent();
		BindingContext = storeFinderViewModel;
		_sfvm = storeFinderViewModel;

	}


	public void OnSearchButtonPressed(object sender, EventArgs e)
	{
		var test = ((SearchBar)sender).Text;
		 _sfvm.DoSearchQuery(test); 
		//_ulvm.ItemWasChecked(itemThatWasClicked);  
	}
}

