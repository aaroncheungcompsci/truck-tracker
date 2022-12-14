## Data Layer
This is the data access layer. Entities, as well as repositories and interfaces for said entities, <br>
are made here. This is also where the backend communicates to the specified database that it is <br>
connected to in the `TruckTrackerContextFactory` file. 

Migrations are added if the database structure needs to be changed. It is advisable to add additional <br>
migrations instead of removing and readding the initial migration once live data is stored in the database <br>
to prevent data loss. 

Foreign key relationships and constraints were the most troublesome to figure out in the context of applying <br>
the knowledge to Microsoft EF Core. I recommend studying <a href=https://learn.microsoft.com/en-us/ef/core/modeling/relationships>this link</a> to get familiar with how relationships <br>
are formed in Microsoft EF Core.
