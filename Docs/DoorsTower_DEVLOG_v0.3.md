# Doors Tower — Dev Log v0.3

## Project Status

Doors Tower is now a playable prototype built with Unity and C#.

Core gameplay loop is fully functional:

```text
Choose a door
↓
Trigger an event
↓
Gain gold / lose HP / heal HP
↓
Advance to next floor
↓
Repeat until Game Over
↓
Restart run
```

---

## Current Features

* HP System
* Gold System
* Floor Progression
* Game Over
* Restart System
* Three Door Types
* Random Events
* Floor-based Damage Scaling
* HP Cap (100)
* Event Log Messages
* Door Disable On Death

---

## Door Design

### Door 1 — Safe

```text
50% Treasure
30% Heal
20% Monster
```

Rewards:

```text
Gold: 10-30
Heal: 15-35
Damage: 10-30 + Floor
```

### Door 2 — Balanced

```text
33% Treasure
33% Heal
34% Monster
```

Rewards:

```text
Gold: 15-35
Heal: 10-30
Damage: 15-35 + Floor
```

### Door 3 — Risky

```text
40% Treasure
10% Heal
50% Monster
```

Rewards:

```text
Gold: 25-60
Heal: 5-20
Damage: 20-45 + Floor
```

---

## Current Runtime State

```csharp
private int hp = 100;
private int gold = 0;
private int floor = 1;
private bool isGameOver = false;
```

HP is capped at:

```text
100
```

---

## Current Architecture

### Main Class

```text
GameManager
```

### Main Methods

```text
OpenDoor(...)
HandleRandomEvent(...)
ResolveDoorEvent(...)

ApplyTreasure(...)
ApplyHeal(...)
ApplyMonster(...)

UpdateUi(...)
SetDoorsInteractable(...)

RestartGame(...)
Start()
```

---

## Refactoring Completed

Previous duplicated event logic has been extracted into reusable methods:

```text
ApplyTreasure(...)
ApplyHeal(...)
ApplyMonster(...)
ResolveDoorEvent(...)
```

Benefits:

* Less duplicated code
* Easier balancing
* Easier future expansion
* Better readability

---

## Implemented Gameplay Rules

### Damage Scaling

```text
Damage = Random Damage + Floor
```

### HP Cap

Healing can never increase HP above:

```text
100
```

### Game Over

When HP reaches zero:

```text
Game Over
Door actions blocked
Door buttons disabled
Restart available
Door buttons restored on restart
```

---

## Feature Implemented — Door Disable On Death

Description:

```text
When the player reaches 0 HP,
all door buttons become non-interactable.

When the game is restarted,
all door buttons are restored automatically.
```

Implementation:

```text
Button[] doorButtons
SetDoorsInteractable(bool value)
Button.interactable
foreach loop
```

Benefits:

* Prevents invalid interactions after Game Over
* Improves game feedback
* Introduces scalable button management

---

## Known Improvements

* Add final score screen
* Add best run tracking
* Add win condition
* Add shop system
* Add audio feedback
* Improve UI polish

---

## Next Feature Candidates

```text
Final Score Screen
Best Run Tracking
Win Condition
Shop System
Audio System
Save System
```

---

## Learning Progress

Concepts practiced:

```text
MonoBehaviour
Canvas UI
TextMeshPro
Button Events
Button References
Button Arrays
Button.interactable

Game State Management
Method Parameters
Refactoring
foreach

Random.Range()
Debug.Log()

Git Basics
Feature Branch Workflow
Git Merge Workflow
GitHub Repository Management
```

---

## Current Status

```text
Playable Prototype

Core gameplay loop completed.

Gameplay polish feature completed:
- Door Disable On Death

Ready for Final Score Screen implementation.
```
