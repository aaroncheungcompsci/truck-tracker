# Backend
This is the backend part of the application. It is separated into three different layers:

1) API Layer - receives the calls from the front end to access the databases.
2) Service (Core) Layer - responsible for making changes to the data of the database if needed.
3) Data Access Layer - responsible for accessing the data that is stored within the databases.

This structure is used to separate the business logic from the data access logic. This should improve clarity and <br>
overall code structure. Future improvements to this structure are welcome. <br>

Each of the layers have another README.md file in them that documents what should be done if changes need to be <br>
made during the database development process. It should NOT be followed once the application is operational on <br>
production. Eventually, these README.md files will have documentation in them that specifies what should be done <br>
when debugging.

To run the backend, use command "dotnet run --project trucktracker.api/trucktracker.api.csproj" when in <br> the "backend" directory.
