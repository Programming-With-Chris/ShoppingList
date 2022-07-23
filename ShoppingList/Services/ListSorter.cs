using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
    public class ListSorter
    {

        public static List<Item> SortUserListItems(UserList userList)
        {
            // Code to sort the Aisles here, let's do aphabetical for now

            List<Item> list = userList.Items;
            //var sortedItems = List<Item>(); 

            
            var sortedItems = list.OrderBy(x => x.LocationData?.Number).ToList();

            var anotherSort = sortedItems.OrderBy(x => x.IsCompleted).ToList(); 

            userList.Items = anotherSort;

            return anotherSort; 
        }
        
    }
}
