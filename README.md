## VIST-477 Programming Assignment 2
Team 9 Members: 
Hao Yi Tan, HuiGang Qu, Jash Veragiwala
# Planet Golf 

This repository contains the Unity3D project file for the Texas A&M Spring 2024 VIST 477 Virtual Reality course's second programming assignment. In this assignment, students are required to develop a VR mini golf game designed to run on the Oculus Quest 2.

# Game Setup
-   Players should stand while playing the game.
-   Upon starting the game, a game setup containing a table, golf club, and golf ball will appear in front of the player.
-   The game will start after the user grabs the golf club using the right controller's trigger button. They can interact with the golf club using either the direct interactor or ray interactor.

## Game controls
|Controller|Function
|---------------------|-------------------------------------------------------|
|Right Joystick North|Teleporting|
|Right Joystick East/West|Player turning left/right|
|Right Trigger|Grabing golf club|
|Left Joystick|Player's locomotion|
|Left Button X|Teleporting to new course (only when gaze interactor is activated)|
## Game logic
    
- The game will end when the player finishes all three courses, and a dialog showing the total score will appear on the player's UI.
- The minimum score is 3, which means it takes one hit per course to hit the ball into the hole. The lower the score, the better the player is.
- Players can restart the game by looking up to the plane on other courses and pressing the X button on the left controller to jump to the new course.
- Players can reset their game by looking up at the plane located on top of the course they are currently on.


## Scene Generation
-   The game features three distinct islands, each representing Mars, Earth, and Saturn, respectively.
-   Each course has its own gravity settings and golf ball resistance settings, tabulated on the table below:

|Course|Gravity|Drag|
|--|--|--|
| Earth |-10|0.5|
|Mars| -5 |0.05|
|Neptune|-17|0.5|

## Implemented Function

|Functionality|Script|
|---------------------|-------------------------------------------------------|
|Increase score when club hit the ball|CollisionDetection|
|Check if ball dropped into the hole|CollisionDetection|
|Teleport player in front of the ball when ball slowed down|CollisionDetection |
|Keep track of scores and number of finished courses|GameLogistics|
|Play sound track upon teleporting to new course|GazeToTeleport|
|Make UI appeared upon looking at the plane|GazeToTeleport|
|Teleport to new course upon pressing button X|GazeToTeleport|
|Reset golf club's position after dropping it|GoldClubReset|
|Neptune's course dynamic obstacles|SwingingCube|
|Earth's course rotating windmill behaviour|WindmillBehaviour|


