# Seeing Eye Bats: 2021 Hack-A-Thon
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/TitleShot.jpg)
 
 This hack submission consists of 2 parts, a Unity portion and the ML portion.
 
 ## Premise
 Test a model that can detect direction and distance of a shot fired within a 3d simulation using game engine technology.
 
 ## Unity 
 The unity app allows the user to rotate a 3d gun around our Ear Bat character by moving the mouse and by pressing the SPACE BAR, fire off a shot.  This shot is then recorded from inside the engine using spatialized sound into a 2 channel new audio clip.
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/UnityScreen.jpg)
 
 
 In order to train the model, we setup an automation procedure that allowed us to generate as many samples as we needed.  The final training consisted of almost 2400 samples generated with a click of a button.
 
 
 ### Variables
 
 The engine provides the ability for the user to determine which direction to fire a shot as well as as distance
 
 
 In addition, the user can change the environmental weather conditions to add in realitic ambient noise conditions to further test the model.
 
 ![img](https://github.com/OctoConsulting/AudioHack2021/blob/master/Assets/Screenshots/EarBatsWeatherChange.gif)
 
 
 ## ML 
 Once an audio clip has been generated from Unity, the new file is dumped into a folder which is constantly monitored.  When that new file is detected, the model is run, and a direction and distance is determined and output for the user.
 
 
 
 
 # Running The Demo
 
 There are 2 apps that you need to run.
 
 The Unity app which is a compiled exe found in the Builds folder [here](https://github.com/OctoConsulting/AudioHack2021/blob/master/Builds/SeeingEyeBats.zip)
 
 Extract and run the `Hack-a-thon2021.exe`.

Using your mouse, you can move the gun around the Ear Bat in the center of the world. 
Space Bar will fire a shot.
Hit Escape to shot the weather dialog and change the environment conditions as you see fit.

Use the slider to adjust the distance of the gun from the Ear Bat, and the other slider to increase or decrease the level of ambient noise.

The TensorFlow models can be trained in notebooks/hackathon_train_model.ipynb and can be run from notebooks/hackathon_app.ipynb. Saved models are in Assets/models/ in h5 format and loaded by the notebooks. Running all cells in hackathon_app.ipynb will bring up the compass and monitor a directory, but the directory path must be manually set first. It is best to first start the unity app and then the Tensorflow app so as to avoid errors from non-audio file creation.

All sound files will be dumped into the following folder
`C:\Users\<your user name>\AppData\LocalLow\Seeing Ear Bats\Hack-a-thon_2021`


# Final Results

Our application generates shot azimuth and shot distance detections with around 90% accuracy.

>>>>>>> ff25842e53acd5a3cafed4bedda6015b8f412ff4
