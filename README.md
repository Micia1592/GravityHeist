# Gravity Heist Control Version
*Play Gravity Heist:* https://mikethingsbetter.itch.io/gravity-heist

If playing from the Unity files, launch the MusicManager scene first to play with all features. Built in editor version 2019.4.13f. 

This version of Gravity Heist is designed as a control measure for research. It is set up for WebGL format so it can be embedded into a page or survey (e.g. Qualtrics).
After a set amount of time playing Gravity Heist, player controls will be disabled and the a UI overlay will direct the player to a specified survey. 
To edit the survey redirect link and the duration of the game. Go to the prefab folder, find the 'TimeController' prefab and edit the duration (e.g. 60 = 1 min, 600 = 10min).
You can also specify the survey link URL in the inspector.
The Open URL with Query script is also set up so that the WEBGL build can carry over query strings between pre and post test surveys or a default query string can be set. 

There is also a visual timer (anchored top right) that can be diplayed by toggling the visibility of the timer display (Canvas(1))/(Text1 in inspector of TimeController) in the timecontroller prefab (it is toggled off by default).

It should also be fairly easy to export the timecontroller prefab and the two related script, 'timer' and 'OpenURLWithQuery' to another project. Please be sure to check the references to player movement and player tag. 

Do let us know how and where the project is used via acknowledgements.  


*Michael Saiger, Oliver Withington and Louise Bryce*
