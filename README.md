Dot Tracking Game
=================

This Game was implemented in WPF. Sample Images are in the Sample-Output/ folder.

Running
-------

This solution requires .Net Framework 4.6.

Timer for the game is configurable in the executable's configuration file.

Replay option is initially disabled till game play happens. Once the ball movement is completed based on
the timer value game ends and summary is calculated. Thereafter the screen gets redirected to Main
screen for replay operation to be performed.

Yellow ball is for Game play movement at random and Green ball is the recorded game movement
as per the mouse hovering. Both are shown in tandem to capture 
original random movement vs user movement though mouse.

Dependencies
-------------

log4net is used for application's logging. This can be downloaded from Nuget package.
