# Survival Arena Prototype — 2D Survival Game

A 2D survival game prototype inspired by the Vampire Survivors-style survival-arena genre, developed with **Unity and C#**.

The project focuses on gameplay programming and reusable gameplay systems, including component-based entities, modular weapons, ScriptableObject-driven upgrades, object pooling, runtime stats, and event-based communication.

## Gameplay

The core gameplay loop is:

**Move → Fight enemies → Collect XP → Level up → Choose upgrades → Survive stronger waves**

The player automatically attacks nearby enemies while surviving increasingly difficult enemy waves.

## Features

- Player movement using Unity's Input System
- Multiple enemy types
- Multiple weapons with different attack behaviours
- Enemy spawning with configurable waves
- Weighted enemy selection
- XP collection and level progression
- Level-up upgrade choices
- Health, damage, and stat systems
- Runtime stat buffs
- Item pickups and enemy drops
- Object pooling for frequently spawned objects
- Basic NavMesh-based enemy movement
- Event-based communication between gameplay systems

## Technical Highlights

### Component-Based Gameplay

Gameplay entities are built from reusable components rather than relying entirely on large inheritance-based classes.

Examples include:

- `MovementComponent`
- `LifeComponent`
- `StatComponent`
- `AttackComponent`
- `HitboxComponent`
- `HurtboxComponent`
- `PathfindingComponent`
- `PickUpItemComponent`
- `DropItemUponDeathComponent`

This allows different gameplay entities to be assembled from the components they require.

### Modular Weapon System

Weapons use an abstract `Weapon` base class with concrete implementations for different weapon behaviours.

Examples include:

- `SwordWeapon`
- `StaffWeapon`
- `LightningOrbWeapon`
- `AttackWhenCloseWeapon`

An `AttackContext` is used to pass attack-related data to different attack behaviours.

### ScriptableObject-Driven Upgrades

Upgrade definitions are stored as `ScriptableObject` assets.

Different upgrade types implement their own application logic for:

- Health
- Maximum health
- Stats
- Weapons

This separates upgrade configuration from the runtime systems that apply the upgrades.

### Object Pooling

Frequently created and destroyed objects are reused through Unity's `ObjectPool<GameObject>`.

Pooling is used for:

- Enemies
- Projectiles
- Pickups

Pooled objects implement an `IPoolable` lifecycle so they can reset their runtime state when spawned and despawned.

### Runtime Stats and Buffs

The project contains a runtime stat system supporting:

- Base stat values
- Additive modifiers
- Multiplicative modifiers
- Runtime stat buffs

Stats are represented using a common `StatType` system and managed by `StatComponent`.

### Event-Based Communication

C# events and `Action` delegates are used for communication between gameplay systems.

Examples include:

- Health changes
- XP changes
- Level-ups
- Wave progression
- Upgrade application
- UI updates

This allows systems such as the gameplay logic and UI to react to state changes without requiring every system to directly control the others.

### Configurable Enemy Waves

Enemy waves are configured through `WaveData` ScriptableObjects.

Each wave can define:

- Wave duration
- Spawn interval
- Enemy pool
- Enemy selection weights
- Minimum wave weight
- Enemy population limits

The `WaveManager` uses these values to determine which enemies to spawn as the game progresses.