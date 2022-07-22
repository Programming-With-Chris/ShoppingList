using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class ItemDetail : ContentPage
{

	public ItemDetail(ItemDetailViewModel itemDetailViewModel)
	{
		InitializeComponent();
		BindingContext = itemDetailViewModel;

	}
}

