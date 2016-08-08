# _A Seattle Health Clinic Web Application with C#, Nancy, Razor, and SQL Databases_

#### By _**Andrew Niekamp, Sid Benavente, Steven Tran, and Yi Chiang**_

## Description/Specifications

A web application which is used for managing a health clinic in Seattle.

#### User Scenarios
1. A program administrator add a new employee to the database.
2. A program administrator schedule employee work days and hours.

#### Data Model
The data model consists of the following:

![ERD](/Content/Images/model.PNG)

#### Program Routing
// TODO
| Method       | Route           | Action  | Model/Comments |
| ------------- |:-------------:| -----:| -----:|
|GET| /| Returns index.cshtml | Informational page with links to view Seattle Venues and Bands |
|GET|/venues | Returns venues.cshtml, Model | Model is a list of all instances of the Venue class; all venues are displayed with the ability to link to Edit/Delete views for each venue |
|GET| /venues/new | Returns venues_form.cshtml| View returned allows addition of a new Venue |
|POST| /venues/new | Returns success.cshtml| Adds a new Venue, routes to simple confirmation page with link to home page |
|GET| /venues/{id}| Returns venue.cshml, Model | Model contains a specific Venue instance, a list of all bands which have played that venue, and a list of all bands in the database |
|POST| /venue/add_band| Returns success.cshtml |  Adds a new association between an instance of a Band and an instance of a Venue, routes to simple confirmation page with link to home page |
|GET| /venue/edit/{id}| Returns venue_edit.cshtml, Model| Model contains a single selected Venue, routes to a page with a form to edit the Venue name|
|PATCH| /venue/edit/{id}| Returns success.cshtml | Updates Venue instance name and routes to a simple confirmation page with a link to home page |
|GET|/venue/delete/{id} | Returns venue_delete.cshtml, Model | Model is a selected instance of the Venue class; view allows administrator to choose to delete instance |
|DELETE|/venue/delete/{id} | Returns success.cshtml| Deletes a select instance of a Venue, routes to simple confirmation page with link to home page|
|GET| /bands | Returns bands.cshtml, Model| Model contains all Band records in database, which are displayed in the view
|GET| /bands/new | Returns bands_form.cshtml, Model| Model contains a list of all Bands in the database; view displays a form allowing user to add a new Band information |
|POST| /bands/new|Returns success.cshtml | Adds a new Band to the database, routes to a simple confirmationpage with a link to home page |
|GET| /bands/{id}| Returns band.cshtml, Model |  Returns a Model containing a single selected Band, a list of all Venue instances associated with that Band, and a list of all existing Venue instances. |
|POST| /band/add_venue| Returns success.cshtml| Adds a new association between a Band instance and an instance of a Venue; routes to a simple confirmation pae with link to home page|

#### Use this program
Clone this repository.

_*Part 1. Prepare the database.*_
The root folder of this project contains two database scripting files (seattle_health_clinic_script.sql for testing, seattle_health_clinic_test_script.sql for production). To import these databases, do the following:
* Open SSMS
* Select File > Open > File and select the appropriate .sql file.
* Click !Execute.
* The databases should appear in your database listing.

_*Part 2. Run server and access the web app.*_
Prepare your machine to run the Kestrel server by following the [instructions here.](https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c)
To start the local server, type in "DNX Kestrel" at a command prompt in the root directory of your project. Navigate in your browser to "LocalHost:5004" to view the homepage.

#### Known Bugs / Unimplemented Features
TBD

#### Support and contact details
Please contact the authors if you have any questions or comments.

#### Technologies Used
This web application was created using C#, Nancy, Razor, SQL, CSS, and Bootstrap.

#### License
Copyright (c) 2016 _**Andrew Niekamp, Sid Benavente, Steven Tran, and Yi Chiang**_

This software is licensed under the MIT license.
