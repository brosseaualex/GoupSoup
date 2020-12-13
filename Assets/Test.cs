using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Bug Fix: (30%)
[FIXED] (DEBUG_Ingredients_Always_Similar was set to true) (15%)The potion always works regardless of what color interacts with what color. 
It used to work up until some recent dev push, investigate what happened and fix it properly while maintaining any feature that broke it.

[FIXED] (Layer was not assigned when creating potion) (10%) When potions are on the floor, they act like a landmine when monsters touch them, they explode, however it seems to be broken, fix that.

Research: (30%)
Where in the code does an enemy detect that it sees a player?
    - The code for the enemy player detection is in the MonsterAI.cs script, the distance is calculated by checking the distance between the monster and the player and
      if the distance is lower than the aggroRange (defined in Monster.cs, on the prefab), it will start chasing the player.

I dont see animation clips for all the monsters, how are the animations done?
    - The animations are created procedurally when an enemy is created. The enemies are created by the MonsterFactory.cs script by pooling them in the "monsterPrefabDict" that gets all the
      enemies in the "Prefabs/Monsters/" folder and upon the enemy creation, the "SetupAnimationForMonster" from the AnimationFactory script is called and the monster
      is passed in a parameter.
      The SetupAnimationForMonster function will instantiate a CustomAnim (CustomAnim.cs) using the Sprite Name that can be specified in the Monster.cs script attached to an enemy prefab.

Do similar plants breed, if yes, describe how, if not, describe what they do instead
    - Similar plants (plants that have the same sprites) do breed.
      All the code for the breeding/spreading can be found in Plant.cs
      When a plant attempts to spread (AttemptSpread function, called by UpdatePlant), it will try to find an available random spot according to a range defined by "Plants_Breed_Distance_Range"
      A Ray will be cast to check the bounds of the random place that was decided, if no plant is found (ray does not hit), it will spawn a random plant at the decided random place.
      If there is already a plant, the AttemptSpread function will check if the existing plant IsBreedable (returns a bool)
      If IsBreedable (the plants are similar, same sprite sheet), the plant colors will be merged to change the color of the existing plant instead of spawning a new one.

What does the flow manager do? What is a Flow, give examples.
    - The Flow Manager is used to control the "behavior" (flow) of a scene, depending on the scene itself.
      For example, in the "Scripts/Flow/Flows" folder, we will find a script for the "GameFlow" and the "MainMenuFlow"
      Both those classes inherit from Flow.cs which contains virtual methods that are redifined by the GameFlow and MainMenuFlow scripts
      For the GameFlow.cs script, this is the script that will initialize all the Managers required for the game to work and will Update/FixedUpdate those flows accordingly.
      The order of initialization is also important as some components need to be loaded before others.
      Example:
      - There is a GameObject in the scene called EntryCreator, this script will initialize the correct FlowManager according to the Scene Name of the scene that the GameObject is in.
      - The FlowManager will then initialize the Flow according to the scene name that was defined when the MainEntry script initialized the FlowManager.

When the potion is thrown, it has a 3D effect of going higher into the air and following an arc, but this is a 2D game, how is that achieved?
    - The coroutine used for Throwing an object can be found in Player.cs and is called ThrowCarriedObject
    - In order to give the feel of being thrown in an arc, the function "Lerps" the position and the local scale
    - The scale of the object is then doubled and when the lerp is done and comes back to its normal scale when falling back on the ground
    - The same applies to its position. The position is slightly altered to go up a bit and the combination of both effect gives the feeling of the item being thrown in an "arc"

Feature :
Feature 1 (30%)) COMPLETED
Add one new type of Monster (Dog), and in the Test.txt file, write the procedure for future devs to create new monsters.
(ex, Step 1, Download art, Step 2, Change art in editor to 4D hybercube mode, ...)
    - Step 1, download a sprite sheet for the animal you want (will use Dog sprite sheet found in "Resources/Sprites/Monsters"
    - Step 2, set the "Sprite Mode" to "Multiple", Click Apply
    - Step 3, if the Sprite Sheet is not already sliced, open it in the Sprite Editor and click on the "Slice" menu
    - If Step 3 was necessary, select Grid by Cell Size and enter the appropriate height and width of the sprites (W: 64 and H: 16 for Dog Sprites included)
    - Step 4, In the "Scripts/Monsters/Monsters" folder, create a script called "Dog" and open it. 
    - Step 5, Remove Start and Update function and have the script inherit the "Monster" class
    - Step 6, create an empty GameObject, name it "Dog" and set its Tag and Layer to "Monster"
    - Step 7, add the "Dog" script to the GameObject, RB2D, Sprite Renderer will be added automatically
    - Step 8, set the Sprite to "Dog_0"
    - Step 9, add a "Box Collider 2D" to the Dog
    - Step 10, in the "Dog Script", in the Anim Info section, set Sprite Name to "Dog"
    - Step 11, in the "Dog Script", set the various info to your liking in "Body Info" and "Ai Info"
    - Step 12, create a prefab in the "Resources/Prefabs/Monsters" folder and delete the GameObject used to create the prefab.


Extra hard Feature(10%)) COMPLETED - Not sure it's super great but its working lol

Add another monster AI state called "Enraged". A monster from Any State can enter into Enraged.A monster
beomes enranged if its spawner dies

- Enemy turns RED when destroying their monolith
- aggroRange and attentionSpan increased
- Max Speed increased
- Starts chasing player immediately

 If you cannot complete it, say the steps you think need to be done.

*/