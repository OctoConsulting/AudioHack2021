# Seeing Eye Bats: 2021 Hack-A-Thon
 This hack submission consists of 2 parts, a Unity portion and the ML portion.
 
 ## Premise
 In order to test a model that could detect direction and distance a shot was fired, we created a 3d simulation which allowed for the generation of those specific gun shots.
 
 ## Unity 
 The unity app allows the user to rotate a 3d gun around our Ear Bat character by moving the mouse and by pressing the SPACE BAR, fire off a shot.  This shot is then recorded from inside the engine using spatialized sound into a 2 channel new audio clip.
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/UnityScreen.jpg)
 
 ## ML 
 Once an audio clip has been generated from Unity, the new file is dumped into a folder which is constantly monitored.  When trat new file is detected, the model is run, and a direction and distance is determined and output for the user.
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/UnityScreen.jpg)
 
 
