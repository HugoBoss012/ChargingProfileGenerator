# ChargingProfileGenerator

------------------------------------SUMMARY----------------------------------------
The purpose of this application is that it generates the user's ideal schedule from a particular point in time (the StartingTime) to the (LeavingTime) As well as optimize for the user's lowest energy bill while taking into account all of the previously specified user options.
The application reads a .json file containing the specified user options as well as additional information regarding tariffs and car data.
The application then does the necessary calculations to determine the ideal schedule for the user that contains the lowest Energy price. It will then write the user schedule data to a specified .json file.


-----------------------------------FEATURES----------------------------------------

----Input Data----
The user of the application will need to go to the specified directory where they have saved the project folder called “JedlixCodingExercise”, they will be supplied with 2 folders and a read me file. 

Within the “JsonFiles” Folder contain’s 2 json files, the one called “inputJsonFile” contains the user input information.

Or the user may save a new json file in this directory and just make sure to change the directory pointed within the program.cs file on line 12 e.g
“public static string src = @"ChargingProfileGenerator\JsonFiles\inputJsonFile.json";”

----Output Data ----
The user of the application will need to go to the specified directory where they have saved the project folder called “JedlixCodingExercise”, they will be supplied with 2 folders and a read me file. 

Within the “JsonFiles” Folder contain’s 2 json files, the one called “OutputJsonFile” contains the calculated user schedule.

The user will need to change the directory to point to that specific file, this can be done in the program.cs file on line 59.

e.g. “string location = @"ChargingProfileGenerator\JsonFiles";

----Classes----
There is one classes file specified called “JSONModel.cs” this contains the structure of how the data will be consumed and produced.

----Math Functions----
Within the program.cs file contains mathimatical functions containing comments which explain what they do.

----Nuget packages-----
For the user to run the project successfully, the user will have to download/install the following Nuget Packages:

Microsoft.Extentions.Configuration.Json

Newtonsoft.Json

To do so, in Visual Studio -> project -> Manage NuGet Packages... -> and search for the 2 packages above.


----Software needed----
Visual Studio will need to be installed on your desired machine, if it is not install you are able to download and install a free version of Visual Studio from this Link: https://visualstudio.microsoft.com/downloads/

---------------------To Ensure the Application runs smoothly:----------------------
Download/Install:
•	Microsoft.Extentions.Configuration.Json
•	Newtonsoft.Json

Change line 12 to match the directory in your system for the inputJsonFile
e.g., public static string src = @"ChargingProfileGenerator\JsonFiles";

Change line 59 to match the directory in your system for the OutputJsonFile
e.g., public static string src = @"ChargingProfileGenerator\JsonFiles";

