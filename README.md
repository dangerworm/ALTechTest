# AL Tech Test
This is a web application which, when given the name of an artist, will calculate and display statistics about the lyrics in the artist's works.

The code base forms the basis of a submission for a job interview.

## Setup and Run
The solution is a .Net 3.1 Core web app, and so will require at least Visual Studio 2019 (or similar). Setup should only take a few minutes.

To run the app:
* Open the solution.
* Restore NuGet packages.
* Select Debug -> Start Debugging from the top menu

Once the app has loaded:
* Type in the name of a performer or artist.
    * If it is unclear exactly which artist you mean, the app will ask you to choose.
* The app will then crunch through lyrics data and produce a page with stats about the artist's work.
* If you wish, you can search for the artist's work on Spotify using the button provided.
* To return to the search page, click the logo at the top.
