# VRTestPattern
Simple Unity Project for Providing a Test Pattern using SteamVR

This application is used to display a fixed test pattern for the purposes of capturing the image
through the VR lenses for calibrating purposes.

This was developed using Unity 2017.3.1f1 Personal (64-bit) and requires the SteamVR plugin to
be imported into the project in order to work.

The pattern is a canvas in Worldspace that can be moved forward or backward along the Z-axis
one unit at a time using the arrow keys.  Using the Up arrow key moves the pattern closer into
view, the Down arrow key moves the pattern further away.  It is limited to nearest of 10 units
and furthest of 900.  The 'Q' key will exist the application.
