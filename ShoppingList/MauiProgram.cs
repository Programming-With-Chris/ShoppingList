using ShoppingList.ViewModels;

namespace ShoppingList;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<UserListService>();
		builder.Services.AddSingleton<UserListViewModel>(); 
		builder.Services.AddTransient<UserListDetailViewModel>(); 
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<UserListDetails>();



		return builder.Build();
	}
}
