# RotatingCube
Hi, i made a really simple Augmented Reality app about Image Tracking. It consist's in  detecting a particulary image spawns a prefab rotating , in this case, a cube!

I made this because i find really hard to understand how can i manipulate in the runtime the prefab that i use in the ARTrackedImageManager! 
So, i set to null that parameter and create a simple code that spawned the prefab too but it is more easily updated/changed in the runtime ( in this case i made im rotate ).

Versions: 
Unity : 2020.3.30f1
ARFoundation : 4.2.2
ARCoreKit XR Plugin : 4.2.2

Print of workspace:
![image](https://user-images.githubusercontent.com/102438221/160258832-06cd6a7f-671b-4d9d-b53a-21f51b2ffaa4.png)

Dont forget to fullfill everything: 
The ReferenceImageLibrary to Unity know what you want to track
The List of GameObjects that you want to be spawned
The scale if you donw want the deafult and finally the Text ( i just print the name of my image )

Hope you enjoy it!
