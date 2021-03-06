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
		builder.Services.AddTransient<UserListDataInputViewModel>(); 
		builder.Services.AddTransient<ItemInputViewModel>(); 
		builder.Services.AddTransient<ItemDetailViewModel>(); 
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<UserListDetails>();
		builder.Services.AddTransient<UserListDataInput>();
		builder.Services.AddTransient<ItemInput>();
		builder.Services.AddTransient<ItemDetail>();
		builder.Services.AddSingleton<SettingsView>();
		builder.Services.AddSingleton<SettingsViewModel>();
		builder.Services.AddSingleton<StoreFinder>();
		builder.Services.AddSingleton<StoreFinderViewModel>();



		return builder.Build();
	}
}
