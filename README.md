# Estoty Test Survivors
# (2024-06-22 12:00 - 2024-06-24 03:00)

## Initial Challenges and Solutions
One of the first issues I faced was getting the joystick to work for player movement. The problem was that the Canvas was disabled for interactions. After an hour of troubleshooting, I fixed this by creating a new Canvas. I ended up using two Canvases, which worked well.

## Dependency Injection and Code Management
I’m familiar with Singletons, but for this project I've must used Dependency Injection. I considered using Zenject but found it too complex for a small project. Instead, I found a simpler solution on GitHub. I used DI for managing the player, camera, sound manager, and joystick.

## Efficient Development Practices
Despite time constraints, I focused on efficient coding practices. I frequently used GetComponent for its simplicity and speed, even though it has some drawbacks. The game runs smoothly on my mobile device, despite the costs of GetComponent.

## Highlights and Achievements
I’m particularly proud of implementing Object Pooling. I combined four separate pools into a single pool, which I hadn’t done in a while. This optimized the game's performance and simplified the codebase, making it easier to manage and extend.

## Conclusion
Overall, I’m satisfied with the project’s outcome. And I've played the game testing it and resting after writing the code all the day.


# TASK:

Develop a Survivor's-Like Shooter mobile game.

## Player:
● Can move with an on-screen joystick

● Camera always focuses on the player

● Player holds a gun

● Automatically aims and shoots at the closest enemy within a specific radius

● Has limited ammo

● Attracts loot when near it (see below)

## Enemies:
● Enemies spawn in random positions outside of the camera view

● Spawned enemies chase the player

● When an enemy reaches the player, it deals periodic damage

● Killed enemies spawn experience gems and random loot (see below)

● There should be several enemy types, each with its own stats (health and damage)

● Enemy spawn rate should increase over time

## Loot:
● Enemies should drop experience gem and health potion/ammo box (randomly) on death

● Experience gem - increases player experience

● Health potion - recovers player health

● Ammo box - recovers player ammo

Make sure to provide scriptable object configs for each gameplay element so they can be
configured by game designers
(For example: enemy spawn rate; health, damage, speed of each enemy; player shooting range
etc.)

## UI:
● Add experience bar

● Add player health bar

● Add kills counter

● Add death screen

# IMPORTANT
● Please use the project template that we provided

● You can use Animancer, TextMeshPro, DOTween, UniTask, Simple Input System

But you can’t use any other third party solutions for anything else.

● It will be a plus if you use Dependency Injection, especially the Zenject Framework.

If you don’t have experience with Dependency Injection, please use the Service Locator
pattern instead.
The Singleton pattern is not allowed.
