namespace ShoppingList.View;

public partial class ArchivedListView : ContentPage
{
	ArchivedListViewModel _alvm;

	public ArchivedListView(ArchivedListViewModel archivedListViewModel)
	{
		InitializeComponent();
		BindingContext = archivedListViewModel;
		_alvm = archivedListViewModel;
		_alvm.GetArchivedLists(); 

	}

    //There is currently a bug in .net maui where OnAppearing doesn't trigger on Shell Items 
    // like it should - Leaving this here anyways for when it's fixed
    // https://github.com/dotnet/maui/issues/8164
    protected override void OnAppearing()
	{
		base.OnAppearing(); 
		_alvm.GetArchivedLists(); 
    
    }

	private async void FrameTapped(object sender, EventArgs e)
	{

		var frame = sender as Frame;
		await frame.ScaleTo(1.1, 75, Easing.BounceIn);
		await frame.ScaleTo(1.0, 75, Easing.BounceOut); 

		var userList = ((TappedEventArgs)e).Parameter as UserList;

		_alvm.RestoreUserList(userList); 

    }
}

