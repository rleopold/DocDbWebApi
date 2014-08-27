DocumentDb with Web API
=======================

Created to test out the new Azure DocumentDb stuff. I used the standard Web Api template in VS 2013, 
swapping out the EF identity stuff for a DocumentDb backing store.

Also, a sample repository pattern that might work for such things.

To get start, go to the new Azure portal and create a DocumentDb instance.
Open up the web.config and put the endpoint, auth key, and the name of the auth db in the app settings

**NOTE:** I have an issue with DocumentDb seeming to hang up when first creating a db or document collection.
After the inital create is done it seems fine, but the request that created the db or collection never comes back.
I have no idea what is causing that, but this stuff is pretty new right now.