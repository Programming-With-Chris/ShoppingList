using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
    public class UserListService
    {
        DatabaseHandler _db; 
        
        public UserListService()
        {
            _db = new DatabaseHandler();
        }

        public List<UserList> GetUserLists()
        {
            var returnLists = _db.GetAllQuery<UserList>(); 
            return returnLists; 
        }

        public List<UserList> GetUserListByName(string name)
        {

            var returnLists = _db.GetQueryByName<UserList>(name);
            return returnLists; 
        }

        public UserList GetUserListById(UserList ul)
        {
            var returnLists = _db.GetQueryById<UserList>(ul.Id);
            return returnLists; 
        }
        public List<Item> GetUserListItems(UserList ul)
        {
            var returnLists = _db.GetQueryById<UserList>(ul.Id);
            return returnLists.Items; 
        }

        public void CreateUserList(UserList newlist)
        {
            _db.Insert(newlist); 
        }

    }
}

