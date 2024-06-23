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
