namespace ShoppingList.ViewModels; 

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy; 
}
