# API Layer
This is the API layer of the application. The frontend communicates directly <br>
with this layer to access the necessary resources from the database.

## Controllers
The Controllers directory is the main location of where the HTTP requests <br>
take place. The frontend calls these different methods throug the Axios <br>
library, and the appropriate HTTP requests are returned depending on the <br>
information that is passed into the functions. This is also one of the places <br>
where the debugging should take place. The other place is within the Services layer <br>
under the Services directory. Testing should be done via Unit Tests or through an
application named <a href=https://www.postman.com>Postman</a>.

## Models
The Models directory is where the API layer is able to create the necessary <br>
models to pass down into the subsequent layers, being the Service and Data <br>
layers. The API layer does not interact with the data layer directly, but <br>
it needs to set up the models in such a way where the data layer will understand <br>
the presented information when the information gets to it.

## Program.cs
This is the file that is the heart of the API layer. This is how the API layer <br>
recognizes the Service and Data layers to be able to communicate with them properly. <br>
This is also another location that may need to be added to as this application gets <br>
bigger.