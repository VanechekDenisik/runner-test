# runner-test

## General
This is a project for a test assignment for the position of "Gameplay Engineer (Unity3D)".
Approximately 8 hours were spent on the assignment including refactoring and documentation.

## Task
Your task will be to create a simple platformer endless runner with an emphasis on code extensibility. Whether it's 2D or 3D doesn't matter. You can use any available assets. The main character of this runner is a character who automatically runs along the platform. The runner should be enjoyable to play.

During the run, the character may encounter various coins/objects, each of which has an effect on its behavior for a certain period of time. In the basic version of the game, a minimum of three types of coins with such effects is proposed: one coin slows down the character for 10 seconds, another speeds it up for 10 seconds, and the third allows the character to take off and fly for 10 seconds. After the effect of the coin ends, the character's behavior returns to normal.

An important part of the task is to demonstrate your understanding of code extensibility principles. You should organize the code in such a way that in the future, new types of coins and new effects can be easily added without the need to edit existing character class(es). Please limit the use of Unity components such as MonoBehaviour for the demonstration of your design skills and knowledge of basic patterns. Also, refrain from using ECS.

The project should be well-documented, with each class provided with comments explaining its functionality and operation principles. It is also necessary to provide a brief description of the architectural decisions you made during the task and justify the choice of specific patterns and principles.

This task will help assess your understanding of SOLID principles, your ability to develop extensible and easily maintainable code, as well as your experience working with C# and Unity3D.

The project code should be published in your GitHub repository. In the Build folder of the repository, there should be a compiled project intended for the Android platform.

## Used assets
[Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)\
[DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)\
[Addressables](https://docs.unity3d.com/Manual/com.unity.addressables.html)\
[HYPER Casual Stickmans](https://assetstore.unity.com/packages/3d/characters/humanoids/humans/hypercasual-stickman-pack-2-169861)
[ProBuilder](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/manual/index.html)
[Simple Gems](https://assetstore.unity.com/packages/3d/props/simple-gems-ultimate-animated-customizable-pack-73764)

## How is extensibility achieved
Probably the most interesting system in the project is 
[Bonuses system](Assets/_GAME/Bonuses/README.md). The system helps to create new entities with new 
effects (bonuses, debuffs) without changing logic of other systems. This is my own system that I created 
previously for action RPG game. Here I simplified it and used with 
[Collectables system](Assets/_GAME/Collectables/README.md) to achieve modular system that can be easily
extended - even if it is unknown how the project will evolve in future.

You can see simple examples of new bonuses implementation in scripts 
[FlyBonus](Assets/_GAME/Characters/Scripts/Bonuses/FlyBonus.cs) and
[MovementSpeedBonus](Assets/_GAME/Characters/Scripts/Bonuses/MovementSpeedBonus.cs).

In the system I am using all SOLID principles: 
1) SRP for simple code
2) OCP for extendable and modular bonuses. Instead of changing existing scripts you can inherit 
from specific classes or use existing classes and configs.
3) LSP for correct inheritance. You can see that in the system there are some inheritance and 
polymorphism used to remove duplication and to extend existing logic.
4) ISP: there are not much interfaces in the project because of rather easy logic of the game.
5) DIP: BonusesSystem is not dependant of any other systems except of Core systems (containing basic 
functionality). Instead of this, systems like Collectables, Characters depend on it. This is the example
of dependency inversion in practice. I used such architecture because implementation of 
specific bonuses logic of in-game mechanics is not the responsibility of Bonuses system. Also I want to change 
the code of the system as rarely as possible.



[Core](Assets/_GAME/Core/README.md)\
