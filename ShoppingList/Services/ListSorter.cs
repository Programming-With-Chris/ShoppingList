namespace ShoppingList.Services;

public class ListSorter
{

    public static bool FrozenFoodLast { get; set; }

    public static bool StartAtBackOfStore { get; set; }


    /// <summary>
    ///  Sorts a UserList's Items in a predetermined order <br/>
    ///  The Sorting is like this - <br/>
    ///    1. Break list down by categories - Meat, Produce, Dairy, Aisle, and FrozenAisle <br/>
    ///    2. Order those category lists in a certain way (Aisles are by aisle number, Meat Dairy and Produce are by Bay Number) <br/>
    ///    3. Recombine those lists back in this order - Produce, then Aisles, Then Dairy, Then Meat, Then Frozen (if setting is turned on). <br/>
    ///    4. Reverse List is StartAtBackOfStore is turned on
    ///     
    /// </summary>
    /// <param name="userList"></param>
    /// <returns>Returns the sorted Items, as well as sets the give userlist's items to be the new sorted list, updating the passed object</returns>
    /// <exception cref="ArgumentNullException">For Both UserList and UserList.Items</exception>
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
        List<Item> unsortedList = new(); 


        var sortedItems = new List<Item>(); 

        bool wasSorted = false; 

        foreach(var item in list)
        {
            if (item.Aisle is not null &&
		        item.Category is not null) 
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
	    
	        } else
            {
                unsortedList.Add(item);
	        }
        }

        // need a way to be defensive in the case where the item doesn't have loc data or a bay num, etc
        meatList = meatList.OrderByDescending(x => Int32.Parse(x.LocationData.BayNumber)).ToList(); 
        dairyList = dairyList.OrderByDescending(x => Int32.Parse(x.LocationData.BayNumber)).ToList();
        produceList = produceList.OrderBy(x => Int32.Parse(x.LocationData.BayNumber)).ToList();

        //research bay number order on aisles (do we want to do alternating asc/desc to form a 'route'?
        aisleList = aisleList.OrderBy(x => Int32.Parse(x.LocationData.Number)).ToList();


        
        sortedItems.AddRange(produceList); 
        sortedItems.AddRange(aisleList);
        sortedItems.AddRange(dairyList);
        sortedItems.AddRange(meatList);
        sortedItems.AddRange(frozenList);
        sortedItems.AddRange(unsortedList);


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

