# PodCraft

----

**About**
----
Demonstation .NET Core Web Api that is composed of the following functionality:

- Add Users:			POST /api/users
- List All Users:		GET /api/users
- List User By Id:		GET /api/users/{id} (Sampla dataset with users with Ids 1 - 12)
- Update Users:			PUT /api/users/{id} 	 
- Delete User By Id:	DELETE /api/users/{id}
- List All Products:	GET	/api/products
- List Product By Id:	GET	/api/products/{id} (Ids 1, 2 or 3)

**Issues:**

----

(1) Product 'Search' functionality still not working [WIP]
(2) Initialize sample user data on startup by navigating to 'Users' page
(3) No separate 'Add New User' page (as would be expected in a real-world scenario)
(4) No authentication/authorization functionality

**Installation:**

----

[Prerequisites] Microsoft .NET Core (Widows, Mac or Linux)

(1) Git clone https://github.com/MisterSK/PodCraft.git into your project folder
(2) Enter the PodCraft directory
(3) dotnet run
(4) Default application URL:PORT is https://localhost:5001 or https://localhost:5000
