# Seeing Eye Bats: 2021 Hack-A-Thon
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/TitleShot.jpg)
 
 This hack submission consists of 2 parts, a Unity portion and the ML portion.
 
 ## Premise
 In order to test a model that could detect direction and distance a shot was fired, we created a 3d simulation which allowed for the generation of those specific gun shots.
 
 ## Unity 
 The unity app allows the user to rotate a 3d gun around our Ear Bat character by moving the mouse and by pressing the SPACE BAR, fire off a shot.  This shot is then recorded from inside the engine using spatialized sound into a 2 channel new audio clip.
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/UnityScreen.jpg)
 
 
 In order to train the model, we setup an automation mode that allowed us to generate as many samples as we needed.  The final training consisted of about 1100 samples generated with a click of a button.
 
 
 ### Variables
 
 The engine provides the ability for the user to determine which direction to fire a shot as well as as distance
 
 
 In addition, the user can change the environmental weather conditions to add in realitic ambient noise conditions to further test the model.
 
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/EarBatsWeatherChange.gif)
 
 
 ## ML 
 Once an audio clip has been generated from Unity, the new file is dumped into a folder which is constantly monitored.  When trat new file is detected, the model is run, and a direction and distance is determined and output for the user.
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/UnityScreen.jpg)
 
 # Running The Demo
 
 There are 2 apps that you need to run.
 
 The Unity app which is a compiled exe found in the Builds folder [here](https://github.com/OctoConsulting/AudioHack2021/blob/master/Builds/SeeingEyeBats.zip)
 
 Extract and run the `Hack-a-thon2021.exe`.

Using your mouse, you can move the gun around the Ear Bat in the center of the world. 
Space Bar will fire a shot.
Hit Escape to shot the weather dialog and change the environment conditions as you see fit.

Use the slider to adjust the distance of the gun from the Ear Bat, and the other slider to increase or decrease the level of ambient noise.
