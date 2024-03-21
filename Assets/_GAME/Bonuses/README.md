# Bonuses system
The system handles the processing of bonuses throughout the game. With this system, any game 
entities can provide bonuses (or modifiers, or debuffs) to other game entities. Moreover, 
this system operates independently of the other mechanics in the game.

You can start with: 
[Bonus](Scripts/Bonus.cs) и
[BonusesController](Scripts/BonusesController.cs).

You can create new bonuses via inheriting from 
[BonusType](Scripts/Configs/BonusType.cs) и
[BonusApplyerFor](Scripts/Configs/BonusApplierFor.cs) and then creating instances of ScriptableObjects 
in the Project.