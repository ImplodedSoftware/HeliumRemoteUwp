# HeliumRemoteUwp

This repo contains the code for Helium Remote for the Universal Windows Platform (UWP) for Windows 10. The application requires Visual Studio 2015 Update 2.

## Using the code

To be able to run this application you will need to have Neon installed and its web service active.
Neon can be downloaded from: https://implodedsoftware.wordpress.com/

To setup the web service, follow these steps:
* Open TOOLS > Options
* Select Web service
* Checkmark "Enable web service"
* Select a port (make sure that it open in your router/Windows firewall)
* Click OK

When you open the solution for the first time, be sure to restore NuGet packages.

## Folders

* HeliumRemote - The UWP application folder
* NeonShared.Pcl - A portable class library containing basic types, interfaces and view models
* Uwp.SharedResources - A UWP class library containing shared view models, styles and more
* lib - Contains the Neon.Api.Pcl.Models.dll file which is a PCL file containing basic entities

## See also

* For additional information about Neon, see: https://implodedsoftware.wordpress.com/
* A reference to the Neon web service can be found here: http://support.imploded.com/solution/articles/9000063810-neon-webservices-api
