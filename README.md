# SafetyInc

## Contents
- [The Task](#task)
- [Primary Technologies](#primarytech)
- [Libraries and Tools](#libsandtools)
- [Assumptions](#assumptions)
- [Design](#design)
- [Alternatives Considered](#alternatives)
- [Limitations/Potential Improvements](#limitationsimprovements)
- [How To](#howto)
- [Appendix](#appendix)

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
This solution primarily uses Kentico integration packages and doesn't leverage any other major packages in a significant way (other than asp.net core packages and what Kentico itself depends on). I did use a selection of JavaScript libraries to provide UI components to the front-end users which is detailed below.

JavaScript Libraries:
- Bootstrap 4 (CSS and JS)
- JQuery and JQuery validation (Used by .net core for front-end validation of field before submission)
- Bootstrap select
- Popper.js (required by Bootstrap select)
- TinyMC rich text edtior
- platpickr DateTime picker

All of the above that weren't bundled with the solution are included via CDNs.

## <a name="assumptions"></a>Assumptions
I made a number of assumptions about how the mock client and their staff would use the system. Primarly I assumed managers/admins would work with the system mostly through the admin interface, and staff (those mostly logging the discussions) would only ever access the system through the front end. I also assumed it would be appropriate to allow users to submit discussions on behalf of others, and for them to be able to edit and delete discussion in addition to viewing them (for discussion they created, or had created for them). It may be more appropriate for users with certain roles only be allowed to delete, but for now I have gone with the assumption anyone can delete a discussion they created or observed.

Additional assumptions:
- Managers would want a way to filter, view, edit, and delete submitted discussions
- Managers may want a simple way to provide a "message of the day" type instruction to users (achieved through page builder)
- Locations can only be added by managers through the backend (locations being perhaps analogous to offices)
- New users would be registered by managers in the backend, no front-end registration is available
- Discussions can have one or more colleagues

## <a name="design"></a>Design
After considering some alternatives (described below), I decided to implement the solution using Kentico 13 with an ASP.Net Core MVC live site. The Xperience application (admin interface) allowed me to define the necessary data structures and develop a custom administration application for the management of discussions and locations. Kentico lends itself well to quickly prototyping solutions and building a clean admin interface for CRUD operations on the application data.

Developing the live site was also simplified as both the solutions make use of the same database. Kentico provides integrations through services and data providers that simplify a lot of the work around tasks such as authentication and retrieving objects from the database. Authentication is handled through integration with ASP.Net core Identity. Data is selected, updated, inserted and deleted through providers that are generated when defining class/table structure through the Xperience admin interface.

The rest of the front end is implemented as a custom MVC application that leverages the providers mentioned above to perform the various CRUD operations on the Discussions. The UI is scaffolded from various Bootstrap components, with JavaScript libraries integrated for date time picking, rich text editing, and user multiselection. The entire front-end requires authentication as I felt this was most appropriate given the scope of the tool. To me it made sense that the whole system would be locked out to the public (except the login screen). I have also leveraged caching on the live site, the user and location selection lists draw their values from a cache. The user list cache is cleared (and subsequently repopulated on the next call) whenever a user is added, deleted, or has a role modfied. Similarly, the location list cache is cleared whenever a location is added, deleted, or modified.

## <a name="alternatives"></a>Alternatives Considered
- ASP.Net Core MVC web app
- ASP.Net Core API drive Angular web app
- Identity through Azure Active Directory

I considered doing an ASP.Net core MVC standalone solution with Azure Active Directory, but found that setting up the user listings among other things would be a slower process (having to use the graph API to get a list of users, and having to provision access keys and set application permissions). Another alternative I considered was a Web API driven Angular app, but ultimately I felt the mock client would be better served by leveraging Kentico's considerable suite of built in functionality. It was also an excellent opportunity for me to work with the newly released Kentico 13, and get a feel for the new development paradigm it offers (compared to previous versions of Kentico). Using a CMS removes a lot of the boiler plate around defining models and databased table structure, as the admin interface is able to handle most of the necessary operations to create a table and operate on the data.

## <a name="limitationsimprovements"></a>Limitations/Potential Improvements
- Let users add new locations themselves (could be configurable via settings key)
- Include additional roles to allow certain users to view all submissions through the front-end (e.g. managers, all done through backend at the moment)
- List of submitted discussions on the front end could be improved to make more responsive for mobile
- Settings keys could be used to enable/disable certain features, such as non-managers being able to edit their own discussions after submission, users being able to add locations themselves, etc
- LESS or SaSS stylesheets with a gulp compile/minify task, similar for JavaScript
- Filtering, sorting, and paging on front-end listings
- Possible Google maps integration for selecting locations
- Restraints on deleting locations if they are referenced by an existing discussion - possible something similar for users who are referenced by a discussion)

Various pieces of built-in Kentico functionality could be leveraged in a myriad of ways to improve the experience:
- Emails could be leverages to inform relevant managers when a user submits a discussion
- Workflows could be applied on home/landing page changes
- Workflows could potentially be applied to Discussion/Location changes as well
- More roles could be defined to allow greated flexibility in terms of who gets access to what
- Custom reports could be configured using the Reporting app to see which users submitted reports within a certain period etc
- And much more

## <a name="howto"></a>How To
The front-end of the site is fairly intuitive so shouldn't require much explanation but I will give a brief rundown. For the purpose of this guide please consider administrator to be a manager. If you log in with the administrator accout on the front-end you will be able to see all discussions regardless of if they are yours or not.

#### Using the Live Site
1. Navigate to [dylanavo.me](http://dylanavo.me) in your prefered browser
2. Sign in with one of the accounts listed below in the [Appendix section](#appendix) 
3. You will be directed to the home/landing page
4. From here click Manage in the navbar, or Manage your discussions in the banner
5. If you have logged in with a user who has any discussions submitted, you should see them listed
6. Click Create New and fill out the details on the next page, click Create when you are done. You should now see this discussion in the listing
7. You will notice even if you create a discussion with someone else as the observer you can still see it in this listing - you can see all discussions you either created and/or observed
8. Edit the discussion you just created and verify you are able to change the details for that discussion, save your changes
9. View the discussion you just created and verify you are able to see the details for the discussion, go back to the listing (or edit if you want to make further changes)
10. Delete the discussion you just created, from the confirmation screen click Confirm, verify the discussion has been removed.

The back-end is a little more involved as it has all of the built-in Kentico apps included. I will give a brief guide below on the critical processes around managing discussions, locations, users, and changing the home page data.

#### Managing Discussions
1. From your previous session, click the Admin link or navigate to [dylanavo.me/admin](http://dylanavo.me/admin)
2. Log in as the administrator user (due to free license limitation this is the only user who has access to the backend)
3. From the admin dashboard you should see the three apps necessary to interact with the system - Safety Discussions, Users, and Pages
4. Click on the Safety Discussion app
5. From this page you can filter and sort the discussions, you can also export the table data using the 3 horizontal lines on the top left of the grid
6. Click the New Discussion button and fill in the form, clicking save when you are done
7. Edit one of the discussions in the grid and verify you are able to modify its values
8. Verify the delete function is working by deleting one of the discussions
9. You can also clone one of the discussions if there is a new discussion with similar details

#### Managing Locations
1. Click on the Locations tab
2. This grid is very similar to the Discussions grid, you can filter, sort and export items
3. Click the New Location button
4. Fill in the form and click Save. You can leave the Code name field empty, the system will generate one for you
5. Edit one of the locations in the grid and verify you are able to modify its values
6. Verify the delete function is working by deleting one of the locations
7. As with the discussions, you can also clone a location

#### Adding a Front-End User
1. Click on the Home icon to go back to dashboard
2. Click on the Users app, and click on the New user button
3. Add a username, full name, and password. Set their Privilege level to none (Editor and above have access to the admin UI)
4. Once the user is saved click on the Roles tab (if you were taken back to the user listing, you can edit the user to get back to their management screen)
5. Click Add roles and give them the Safety Discussion User role, this will allow them to appear in the user selections on the live site

#### Changing the Home Page Content
1. Click on the Home icon again, then click on the Pages app
2. Click on the Home node in the tree and click on the Content tab
3. Here you can change the content that appears on the home/landing page when a user logs in
4. Make some changes and click save, then switch to the Page tab
5. You should be able to see your changes in the page builder
6. You can also modify the banner body text directly in-line on the page builder.
7. You can also add new sections and widgets (at the moment there is only default Kentico widgets - Form and Rich Text)
8. Make some changes to the text and click save
9. You should be able to immediately see your changes reflected on the live site, to verify click the Open Application list button at the top left of the screen, then click the Live site button at the bottom of the slide out menu

## <a name="appendix"></a>Appendix
u: administrator
p: Password!23

u: john.citizen@email.com
p: JohnCitizen

u: jane.citizen@email.com
P: JaneCitizen

u: hank.smith@email.com
p: HankSmith

u: kaitlyn.king@email.com
p: KaitlynKing

u: dylan.vanoss@email.com
p: DylanVanOss

u: anduin.wrynn@stormwind.az.ek
p: ForTheAlliance

u: magni.bronzebeard@ironforge.az.ek
p: ForKhazModan

u: tyrande.whisperwind@darnassus.az.kal
p: EluneGiveMeStrength

u: thrall@orgrimmar.az.kal
p: ForTheHorde

u: baine.bloodhoof@thunderbluff.az.kal
p: EarthmotherGuideUs

u: sylvanas.windrunner@themaw.sl
p: LetNoneSurvive
