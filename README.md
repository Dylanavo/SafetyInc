# SafetyInc

## Contents
- [The Task](#task)
- [Primary Technologies](#primarytech)
- [Libraries and Tools](#libsandtools)
- [Assumptions](#assumptions)
- [Design](#design)
- [alternatives](#alternatives)
- [limitationsimprovements](#limitationsimprovements)

## <a name="task"></a>The Task
Our customer has requested a proof-of-concept to allow employees to record safety discussions they have had with their colleagues, through a responsive web application. They have identified desktop and tablets as the target platforms.

The app will let employees record that a safety discussion occurred and to enter the following information related to it:
- The Observer (user entering the safety discussion)
- Date of discussion
- Location of discussion
- Colleague the discussion was with
- Subject of discussion
- Outcomes

It should also allow the employee to view a list of previous discussions they have recorded.

Build the proof-of-concept for recording safety discussions, and viewing previously entered discussions.

## <a name="primarytech"></a>Primary Technologies
Kentico 13 CMS (Recently rebranded Xperience) for the content repository and administration interface, with a seperate ASP.Net Core MVC live site that serves as the front-end of the application.

## <a name="libsandtools"></a>Libraries and Tools
This solution primarily uses Kentico integration packages and doesn't leverage any other major packages in a significant way (other than what Kentico itself depends on). I did use a selection of JavaScript libraries to provide UI components to the front-end users which is detailed below.

JavaScript Libraries:
- Bootstrap 4 (CSS and JS)
- JQuery and JQuery validation (Used by .net core for front-end validation of field before submission)
- Bootstrap select
- Popper.js (required by Bootstrap select)
- TinyMC rich text edtior
- platpickr DateTime picker

All of the above that weren't bundled with the solution are included via CDNs.

## <a name="assumptions"></a>Assumptions
I made a number of assumptions about how the virtual client and their staff would use the system. Primarly I assumed managers/admins would work with the system mostly through the admin interface, and staff (those mostly logging the discussions) would only ever access the system through the front end. I also assumed it would be appropriate to allow users to submit discussions on behalf of others, and for them to be able to edit and delete discussion in addition to viewing them (for discussion they created, or had created for them).

Additional assumptions:
- Managers would want a way to filter, view, edit, and delete submitted discussions
- Managers may want a simple way to provide a "message of the day" type instruction to users (achieved through page builder)
- Locations can only be added by managers through the backend (locations being perhaps analogous to offices)
- New users would be registered by managers in the backend, no front-end registration is available
- Discussions could have one or more colleagues

## <a name="design"></a>Design
After considering some alternatives (described below), I decided to implement the solution using Kentico 13 with an ASP.Net Core MVC live site. The Xperience application (admin interface) allowed me to define the necessary data structures and develop a custom administration application for the management of discussions and locations. Kentico lends itself well to quickly prototyping solutions and building a clean admin interface for CRUD operations on the application data.

Developing the live site was also simplified as both the solutions make use of the same database. Kentico provides integrations through services and data providers that simplify a lot of the work around tasks such as authentication and retrieving objects from the database. Authentication is handled through integration with ASP.Net core Identity. Data is selected, updated, inserted and deleted through providers that are generated when defining class/table structure through the Xperience admin interface.

The rest of the front end is implemented as a custom MVC application that leverages the providers mentioned above to perform the various CRUD operations on the Discussions. The UI is scaffolded from various Bootstrap components, with JavaScript libraries integrated for date time picking, rich text editing, and user mutliselection. The entire front-end requires authentication as I felt this was most appropriate given the scope of the tool.

## <a name="alternatives"></a>Alternatives considered
- ASP.Net Core MVC web app
- ASP.Net Core API drive Angular web app

I considered doing an ASP.Net core MVC standalone solution or an API drive Angular app, but felt the virtual client would be better served by leveraging Kentico's considerable suite of built in functionality. It was also an excellent opportunity for me to work with the newly released Kentico 13, and get a feel for the new development paradigm it offers (compared to previous versions of Kentico). Using a CMS removes a lot of the boiler plate around defining models and databased table structure, as the admin interface is able to handle all the necessary operations to create a table and operate on the data.

## <a name="limitationsimprovements"></a>Limitations/Potential Improvements
- Let users add new locations themselves (could be configurable via settings key)
- Include additional roles to allow certain users to view all submissions through the front-end (e.g. managers, all done through backend at the moment)
- List of submitted discussions on the front end could be improved to make more responsive for mobile
- Settings keys could be used to enable/disable certain features, such as non-managers being able to edit their own discussions after submission, users being able to add locations themselves, etc
- LESS or SaSS stylesheets with a gulp compile/minify task, similar for JavaScript
- Filtering, sorting, and paging on front-end listings
- Possible Google maps integration for selecting locations
