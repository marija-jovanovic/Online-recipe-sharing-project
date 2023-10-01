<h1 align = center>Recipe project</h1>
<div align="center"><img src="https://png.pngtree.com/png-vector/20191129/ourmid/pngtree-hand-drawn-kitchen-tools-doodle-set-of-cooking-utensils-vector-illustration-png-image_2046732.jpg"></div>

 <b><h2 align = center>Introduction</h2></b>

This is an independent and obligatory project that I made for my university course - Development of server-based WEB applications.

More information about this university course can be found at the following <a href="https://feit.ukim.edu.mk/en/subject/development-of-server-based-web-applications/">link</a>.

The project is an MVC Web Application created using ASP.NET Core.

The Web Application is about sharing cooking recipes. It allows users to create their own recipes, view recipes from other users and write reviews.

<h2 align = center>How to run the project on your machine</h2>

First and foremost, you will need to download Microsoft .NET SDK. For this project, the 6.0 version is used.

Next, it is recommended to download Visual Studio. The Visual Studio version for this project is from 2022.

To make sure you have installed all the required . NET libraries in Visual Studio, use the NuGet package manager.

Once you are done with all the installations, go back to this github repository.

Click on the green Code button located at the top of this github repository and click Open with Visual Studio to start the cloning process.

After successfully cloning the repository, make sure to download the recepti_podatoci.dacpac file, which can be found at the bottom of this repository. This file contains all the important information (including the data) for building the database. 

Next, in Visual Studio open the SQL Server Object Explorer, select your MSSQLLocalDB and perform right-click on the Databases folder. Then click on Publish Data-tier Application.

In the file on disk field select the recepti_podatoci.dacpac file. Then, in the database name field type Recepti.Data and finally click Publish.

If after clicking the publish button a window pops up asking to proceed with the task make sure to click Yes.

Once the Data-tier application is published, make sure to click the refresh button located at the SQL Server Object Explorer.

Now you can finally run the application and you will notice that it will be opened in your preferred web-browser.

<h2 align = center>Goal and requirements</h2>

The goal was to create different identity roles where each role will be authorized to perform certain tasks tailored to its needs.

For the creation of identity roles, ASP.NET Core Identity was used. This project should distinguish two roles, 1) registered users and 2) administrator.

1) Registered users will be users who have previously created an account and should be authorized to create their own recipes, edit and delete their existing recipes and write reviews about recipes from other users.
  
2) There will be only one administrator who will be manually inserted into the database. The administator should be authorized to view all recipes and edit/delete them, but should not be authorized to create new recipes nor to write reviews.

Apart from these two roles, the Web application should be available in visitors mode, where such visitors will not have an account and will be only authorized to view the recipes.

<h2 align = center>Available features for each role</h2>

<b>*Visitors*</b> - As explained above, visitors do not have an account (they are neither registered nor logged in), thus they technically do not have a separate identity role.

At the top right corner the visitors have two buttons, *Register* and *Login*. The register button leads the visitor to a new page for creating a new account. The register operation is performed with email and password only. Once the visitor has created its account, it automatically becomes entered into the user database and it gets logged in. Once logged in, the user is no longer in visitor's mode. The login button takes the visitor to the login page where it will be required to give its email and password details so that it can login with its existing account. 

Additionally, visitors can filter the recipes by food type (Dessert, Main course, Salad or All), Vegan-friendliness (Yes, No, All), Kid-friendliness (Yes, No, All) and by a specific recipe creator. For every recipe, visitors can read its title, preparation time, picture, preparation text, suitability for vegans, suitability for children, type of food, who created it and the average grade calculated by taking the average of all reviews for that recipe. 

In addition, for every recipe visitors can click on *Details* link which opens a new page where additional information is displayed for the selected recipe including all of its reviews.

<b>*Registered and logged in users*</b> - At the top right corner, a Hello greeting is displayed following the user's email to indicate that the user is successfully logged in. Next to the greeting, the *Logout* button is placed. When the user wishes to log out, all it should do is click the Logout button and automatically the application will be back to visitors mode. 

Additionally, logged in users can click the *Kreiraj nov* link which displays the page for creating new recipes. To create a new recipe, the user is required to enter the title of the recipe, preparation time (in hh::mm time format), upload a picture which is already saved on its device, enter the preparation text, suitability for vegans, suitability for children, type of food and the name of the creator itself. Once the recipe is created, it will be shown at the list of all recipes. 

An interesting detail is that the recipes created by the current logged in user will be colored in green and the rest of the recipes which are not created by the current logged in user are colored in orange.

Additionally, the recipes created by the current logged in user can be edited and deleted by the user itself, whereas the rest of the recipes which are not created by the current logged in user cannot be edited/deleted by the same user and only their details can be viewed. In other words, only the creators of the recipes can edit/delete their own recipes (the admin can also edit/delete any recipe). 

The logged in users can also filter the recipes in the very same way as in visitors mode. 

The last thing that is exclusively for logged in users is the ability to write reviews. At the top left corner there is a *Recenzii* button which takes the user to a page where all of its reviews are shown. If the user wishes to create a new review, it should click the *Kreiraj nov* button which will display a new page for creating reviews. The user will be required to enter its email, comment, grade for the recipe in the range [1, 10] and the name of the recipe that the review is dedicated to.

To ensure valid reviews, the users cannot write reviews for recipes that they have personally created. Reviews can be written only for recipes created by other users.

Once the review is created, it will be added to the list of the user's reviews. The user is allowed to edit/delete its own reviews. Every created review plays part in calculating the average grade of the recipe and the same review is added to the details page of the chosen recipe.

<b>*Administrator role*</b> - The admin is also required to log in, in order to activate its identity role. The admin view of the application is identical to the visitors view, except for the fact that the admin can edit/delete every recipe.  

<h2 align = center>Personal summary</h2>

The process of creating this project was fun and challenging, yet rewarding. 

Apart from implementing the things I learned throughout the course, I decided to dive deeper by securing the application, as I believe no application is functional without protecting its users integrity.

The implementation of security and authorization parts was new to me as it was not in the main focus of the university course. I had to figure it out on my own by researching and having many trials and errors.

After finally finishing the project, I have learned many new things which I believe are basic necessity for any Web Application.    
