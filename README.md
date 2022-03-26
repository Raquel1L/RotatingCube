# RotatingCube
Hi, i made a really simple Augmented Reality app about Image Tracking. 
It consist's in detecting a particulary image and spawn a rotating cube over it without using directly the prefab of ARTRackedImageManager. I did it because, with this code, i manipulate a prefab ( or more than one ) in runtime ( rotating a cube in this example ) allowing to do a lot of cool stuff easily! 

One Step ( kind of obvious ) just attach the following script to the AR Session Origin ( responsible for transformim the AR coordenates in "Unity" coordenates ) and dont forget to fill the ReferenceImageLibrary with the image (or images) you want to track and fill the Text (I print the name of the object, print wathever you want) and, finally, fullfill the list of cool prefabs you want to appear in the real world!
