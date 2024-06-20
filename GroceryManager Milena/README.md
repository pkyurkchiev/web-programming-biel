## Development
### Step 1
- Download the "GroceryManager" from Github -> folder "course-work/StartingPoint"
- Open the project in Visual Studio - Community Edition 2019
### Step 2
- Solution Explorer -> right mouse button -> Add -> New Project
- Add "Blazor WebAssembly App" -> GM.Client
### Step 3
- Solution Explorer -> right button on "Solution Grocery Manager" -> Properties
- Choose "Multiple startup projects"
- Set GM.Client -> Start
- GM.Models -> None
- GM.Server -> Start without debugging
- "Start" the project - you will see the both server (blank page) & client page.
### DB
- The Blazor application will use dynamic in memory DB.
### Step 4
- Add folder in "GM.Client" -> ViewModels.
- In this folder "ViewModels" add new class "GloceryViewModel.cs".
### Step 5
- Add another folder under "GM.Client" -> Data.
- Add new item -> interface, in folder "Data" -> "IGloceryDataAccess.cs".
- Under GM.Client / Data -> Add class "GlocerySimpleData.cs".
- Under GM.Client / Data -> Add class "MockGlocerySimpleData.cs".
### Step 6 
- GM.Client / Dependecies -> right mouse button -> choose "Add project refference".
- Check the mark on the row "GM.Models".
### Step 7 
- Under GM.Client / Dependecies / Packages -> right button -> Manage NuGet Packages -> Add "Simple.Odata.Client" (5.25) -> Install -> I Agree.
### Step 8
- Get the whole WOM.Client from the Exercises (WorkOutManager) and paste it here.
- Rename and rebuild the whole new GM.Client with the Grocery related info (classes, methods, properties).
### Step 9
- Change my GM.Server locahost:xxxxx port with the new one
in GM.Client / Data / GrocerySimpleData.cs -> line 16
http://localhost:49248/odata

