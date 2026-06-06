# Doors Tower — Dev Log v0.5

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
View Final Score
↓
Track Best Run
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
* Final Score Screen
* Best Run Tracking

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

private int bestFloor = 1;
private int bestGold = 0;

private bool isGameOver = false;
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

UpdateBestRun(...)

UpdateUi(...)
SetDoorsInteractable(...)

RestartGame(...)
Start()
```

---

## Feature Implemented — Best Run Tracking

Description:

```text
The game tracks the best run achieved
during the current session.

Best Run is determined by:

1. Highest Floor reached
2. If Floor is tied:
   Highest Gold collected
```

Displayed on the Final Score Screen:

```text
Best Floor
Best Gold
```

Current Scope:

```text
Runtime only
```

Best Run resets when the application is closed.

---

## Known Improvements

* Add win condition
* Add shop system
* Add audio feedback
* Add save system
* Improve UI polish

---

## Next Feature Candidates

```text
Win Condition
Shop System
Audio System
Save System
UI Polish
```

---

## Current Status

```text
Playable Prototype

Core gameplay loop completed.

Gameplay polish features completed:

- Door Disable On Death
- Final Score Screen
- Best Run Tracking

Ready for Win Condition implementation.
```
