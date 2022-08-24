# ShoppingList


## Getting Started Running The App - 
- If you want to connect it to the Kroger API, you'll need to sign up for a Kroger dev account here - https://developer.kroger.com/ 
- Then go to "Manage" -> "Apps" and create a new app. Once you've done that, create a json file in your project with the following structure - obviously with your values filled in.
    
        {
            "ClientId": "",
            "ClientSecret": "",
            "RedirectUri": "",
            "KrogerUrl": "https://api.kroger.com/v1/"
        }

- Then in KrogerAPIService, you'll just need to point it in the "GetStartupConfig" method to your file. 

- If you don't want to go through all those steps, then you'll need to do some code updates to remove the API calls throughout the app. Unless I decide to go in and make those conditional (which I probably will at some point, in case a user doesn't have internet connection).
  
- After that, i think you'll just need to do the usual dotnet restore, etc, and you should be good to go? if not let me know in the YT comments, in case I'm forgetting something else. But ultimately, this is just an example app to give you ideas on things you can do in your own apps, and to see how I implemented them. 



## Major Features needed for MVP
- ✔ Sorting by aisle (Data from API, sorting by ??? (that might be where the ML comes in))
- ✔ Create new list starting with last week's list

## The Build Roadmap
- ✔ Base app build to give me a UI to test in 
- ✔ Database and data persistence features (In our case, Sqlite)
- ✔ Add APIs to grab information we need - At the moment, this is just hooked up to the Kroger API, more stores added in the future
- ✔ Sorting Algorithms - Building an efficient path through the store
- Basic List Creation features - A good 'starting point' for a user, maybe different list types we can keep up with (weekly groceries vs stuff for a party, etc)
- UI work to make the app actually look good
- Personal testing in a Grocery store 
- Maybe a soft launch at this point?
- Feature Roadmap starts

## Feature Roadmap
- Supporting more stores.
- Add a 'non-grocery' list option, in case a user just wants to create a regular checklist.
- As I think of more features, we'll put them here.
