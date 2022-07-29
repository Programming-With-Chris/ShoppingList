namespace ShoppingList.Services;

public class ListSorter
{

    public static bool FrozenFoodLast { get; set; }

    public static bool StartAtBackOfStore { get; set; }


    public static List<Item> SortUserListItems(UserList userList)
    {

        Guard.IsNotNull(userList, nameof(userList)); 
        Guard.IsNotNull(userList.Items, nameof(userList.Items));

        SetPreferences(); 

        
        List<Item> list = userList.Items;
        List<Item> meatList = new();
        List<Item> produceList = new();
        List<Item> aisleList = new();
        List<Item> dairyList = new();
        List<Item> frozenList = new(); 


        var sortedItems = new List<Item>(); 

        bool wasSorted = false; 

        foreach(var item in list)
        {
            switch (item.Aisle.ToUpper())
            {
                case "MEAT":
                    meatList.Add(item);
                    wasSorted = true; 
                    break;
                case "SEAFOOD":
                    meatList.Add(item);
                    wasSorted = true;
                    break;
                case "DAIRY":
                    dairyList.Add(item);
                    wasSorted = true; 
                    break;
                case "PRODUCE":
                    produceList.Add(item);
                    wasSorted = true;
                    break; 
            }

            if (!wasSorted)
            {
                if (item.Category.ToUpper().Contains("FROZEN") && FrozenFoodLast && !StartAtBackOfStore)
                {
                    frozenList.Add(item);
                } else
                {
                    aisleList.Add(item);
                }
            }

            wasSorted = false; 
        }

        meatList = meatList.OrderBy(x => Int32.Parse(x.LocationData.BayNumber)).ToList(); 
        dairyList = dairyList.OrderBy(x => Int32.Parse(x.LocationData.BayNumber)).ToList();

        aisleList = aisleList.OrderBy(x => Int32.Parse(x.LocationData.Number)).ToList(); 


        //research bay number order on aisles (do we want to do alternating asc/desc to form a 'route'?


        foreach(var item in produceList)
        {
            sortedItems.Add(item); 
        }

        
        foreach(var item in aisleList)
        {
            sortedItems.Add(item); 
        }

        foreach(var item in dairyList)
        {
            sortedItems.Add(item); 
        }

        foreach(var item in meatList) 
        {
            sortedItems.Add(item); 
        }

        foreach(var item in frozenList)
        {
            sortedItems.Add(item); 
        }

        if(StartAtBackOfStore)
        {
            sortedItems.Reverse(); 
        }

        // Finish with IsCompleted Sort, so completed items are at the bottom of the list
        var anotherSort = sortedItems.OrderBy(x => x.IsCompleted).ToList(); 

        userList.Items = anotherSort;

        return anotherSort; 
    }

    public static void SetPreferences()
    {
        FrozenFoodLast = Preferences.Get("FrozenFoodLast", true);
        StartAtBackOfStore = Preferences.Get("StartAtBackOfStore", false); 
    }
}

