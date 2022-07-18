using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.ViewModels;

[QueryProperty("Item", "Item")]
public partial class ItemDetailViewModel : BaseViewModel
{
    readonly ItemService _itemService;

    [ObservableProperty]
    Item item;

    public ItemDetailViewModel()
    {
        _itemService = new();
    }

}