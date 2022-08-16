using ShoppingList.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting; 

namespace ShoppingList;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Font Awesome 6 Free-Regular-400.otf");
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
