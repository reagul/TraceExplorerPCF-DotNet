README

NOTE: If these hosts are taken on PCF, you might need to modify the dotnet app { to point to the right REST URL }, which you will get once you are successful in pushing the BootREST app. 


First Push the Apps . 

Directly Push Dotnet app from inside the Push-to-CF folder. This is a compiled / publish output ready to use. 

1> cf push xamarin  -b hwc_buildpack -s windows2012R2

If you know how to modify the Dotnet app and Publish, you know wha to do to get to a PCF ready artifct . The Push-to-CF folder is meant for ready test and points to hard coded Backing Boot REST app. 


Build and Push Spring Boot app : Do this from inside the  BootREST folder 

2> maven package or just modify the manifest and add the right path to the REST jar and cf push 

3> cf push 


Curl Command 


4>  curl https://xamarin.cfapps.pez.pivotal.io

5> To build the dotnet App from groundup, get into the Dotnet/AllenXamarinTest folder, which forms the source for the Dotnet Publish artifact.

https://github.com/reagul/TraceExplorerPCF-DotNet/tree/master/DotnetApp/AllenXamarinTest

Credits : Allen Duet and Richard Seroter @ Pivotal

@@@@@@@@@@@@

![alt tag](https://github.com/reagul/TraceExplorerPCF-DotNet/edit/master/DotNetTrace-Sample.png)
