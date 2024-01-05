REST-API for Persons and Interests

Description:
 The following is a Web API Project that showcases people and their interests. 
 The project is built on a REST-structure to enable standard HTTP-commands and CRUD Operations. 
 The API is written in C# using ASP.NET and Entity Framework core to enable database access.

Configuration:
 1. Clone project
 2. Create a database and add the connection string in 'ApplicationContext' within 'appsettings.json'.
 3. Use the 'run' button to start the API

Endpoints:
(GET)
 *MapGet/persons: View all persons in database
 *MapGet/interests: View all interests in database
 *MapGet/persons/{id}: View a specific person
 *MapGet/interests/{id}: View a specific interest and all persons tied to it
 *MapGet/persons/{personId}/interests: View a specific person's interests
 *MapGet/persons/{personId}/interestLinks: View all links tied to specific person and interest

(POST)
 *MapPost/persons/add: Add new person
 *MapPost/interests/add: Add a new interest
 *MapPost/persons/{personId}/links/{interestId}/add: Add new link tied to specific person and interest
 *MapPost/persons/{personId}/interests/{interestId}/add: Add existing interest to existing person in database

Project structure:
 Program.cs defines API routes and configuration. Project is built on three models: interests, persons and links.
 To handle various operations such as viewing a person's interests and adding a new one - Handlers.cs is used.
 The API in turn, offers different endpoints to manage individuals and their interests.

Notes:
For program to work correctly, all calls require correct parameters and data to be provided as guided.
