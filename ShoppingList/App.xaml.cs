namespace ShoppingList;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        SetTheme(); 
		MainPage = new AppShell();
	}

	public void SetTheme()
	{

        string themeName; 
        var isFirstLoad = Preferences.Get("FirstLoad", true); 
        if (isFirstLoad)
        {
            var currTheme = Application.Current.RequestedTheme;
            if (currTheme == AppTheme.Dark)
                themeName = "Royalty";
            else
                themeName = "Nautical";
        }
        else
        {
		    themeName = Preferences.Get("Theme", "Nautical"); 
        }

        Preferences.Set("FirstLoad", false);


        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            foreach(ResourceDictionary dictionaries in mergedDictionaries)
            {
                var primaryFound = dictionaries.TryGetValue(themeName + "Primary", out var primary);
                if (primaryFound)
                    dictionaries["Primary"] = primary; 

                var secondaryFound = dictionaries.TryGetValue(themeName + "Secondary", out var secondary);
                if (secondaryFound)
                    dictionaries["Secondary"] = secondary; 

                var tertiaryFound = dictionaries.TryGetValue(themeName + "Tertiary", out var tertiary);
                if (tertiaryFound)
                    dictionaries["Tertiary"] = tertiary; 

                var accentFound = dictionaries.TryGetValue(themeName + "Accent", out var accent);
                if (accentFound)
                    dictionaries["Accent"] = accent; 
                
                var darkAccentFound = dictionaries.TryGetValue(themeName + "DarkAccent", out var darkAccent);
                if (darkAccentFound)
                    dictionaries["DarkAccent"] = darkAccent; 
            }
        }


	}

//#if NET6_TARGET
//public static void Main(string[] args) {}

//#endif
}
