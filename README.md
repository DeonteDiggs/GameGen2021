# GameGen 2021
This is a *very quick/rough* prototype of the Wind-blowing mechanic.

Input is handled through Unity's new Input Management System, using Unity Events.

There are three main objects right now

 - A shipObject (a sphere)
 - The windObject (that's what we control directly)
 - targetObject

shipObject is a ship; it has a boat tag, and can be affected by the player's wind power. 
There is no associated code.

windObject is directly controlled by the player; it rotates around targetObject using the rotateAround function, and it use's raycasting to check if shipObject is in front of the player.

targetObject just exists as a point in space for windObject to rotate around.
# GameGen2021