# Services Layer
This is the Services layer of the application. This layer is responsible <br>
for linking the API layer with the data layer to retrieve data from the <br>
database. The Services layer exists to separate the data retrieval logic <br>
from the business logic. 

## Interfaces
This directory allows the API layer to call the different functions that are <br>
located in the Services directory. This allows for only calling the functions <br>
that are currently implemented.

## Models
This directory allows for the creation of new models, if necessary. This is <br>
mainly important for creating the models that are necessary for the data layer <br>
to process, since this is how the data layer will interact with SQL server.

## Services
This directory contains the heart of all the logic that is associated with <br>
this layer. This is one of the few places where debugging should take place <br>
if there is an issue. The other place is the Controllers directory in the <br>
API layer.