namespace ShoppingList;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

//#if NET6_TARGET
//public static void Main(string[] args) {}

//#endif
}
