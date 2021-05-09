Copyright: 2021 Rokas Marcinkevicius <rokas.marcinkevicius@ktu.edu>

Prerequisites:
Visual Studio Code - https://code.visualstudio.com/download

C# for Visual Studio Code (latest version) - https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

.NET Core 3.1 SDK or later - https://dotnet.microsoft.com/download/dotnet-core/3.1

Github Repo - Download the Repo from https://github.com/RokasMarcinkevicius/AdForm.git

Postman - Download postman from https://www.postman.com/downloads/

Unzip the repo, Open Visual Studio Code, select file -> Open Folder, select the unzipped repo.

Select any .cs file, When a dialog box asks if you want to add required assets to the project, select Yes.

Write "dotnet run" in the terminal or press CTRL + F5
then open Postman.

Copy the Postman requests as described below.
To create Points create such a request:
![CreatePoints](https://github.com/RokasMarcinkevicius/AdForm/blob/master/Images/CreatePoints.jpg)
To get all the active points input the request as so:
![GetAllPoints](https://github.com/RokasMarcinkevicius/AdForm/blob/master/Images/GetAllPoints.jpg)
To get squares and/or the number of them in your submitted list of points create the request as so:
![GetSquares](https://github.com/RokasMarcinkevicius/AdForm/blob/master/Images/GetSquares.jpg)

If you cannot install postman due to company policy you can also open a generic terminal and input cURL requests as so to replicate the same interactions:

To create Points create such a request:

curl -k -i --location --request POST 'https://localhost:5001/api/Points' \
--header 'Content-Type: application/json' \
--data-raw '[{
  "X": "-1",
  "Y": "1"
},
{
  "X": "1",
  "Y": "1"
},
{
  "X": "1",
  "Y": "-1"
},
{
  "X": "-1",
  "Y": "-1"
}]'

To get all the active points input the request as so:

curl -k -i --location --request GET 'https://localhost:5001/api/Points'

To get squares and/or the number of them in your submitted list of points create the request as so:

curl -k -i --location --request GET 'https://localhost:5001/api/Squares'