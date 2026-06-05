# Doors Tower — Dev Log v0.1

## 1. Project Overview

**Doors Tower** is a simple mobile roguelite prototype built with Unity and C#.

The player opens one of three doors. Each door can trigger an event such as treasure, monster damage, or healing. The run progresses floor by floor until the player reaches Game Over. The player can then restart and try again.

### Core Loop

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

The goal is to create a simple but addictive mobile game based on fast decisions, risk/reward, progression, and replayability.

---

## 2. Current Tech Stack

- **Engine:** Unity 6
- **Language:** C#
- **UI System:** Unity Canvas
- **Text System:** TextMeshPro
- **Target Platform:** Mobile, initially Android
- **IDE:** Visual Studio Code

---

## 3. Current Scene Structure

Current scene contains:

```text
SampleScene
├── Main Camera
├── Canvas
│   ├── FloorText
│   ├── HpText
│   ├── GoldText
│   ├── EventText
│   ├── Door1Button
│   ├── Door2Button
│   ├── Door3Button
│   └── RestartButton
├── EventSystem
└── GameManager
```

---

## 4. Current UI Elements

The current UI displays:

```text
FLOOR: 1
HP: 100
GOLD: 0

Choose a door...

[ DOOR 1 ]
[ DOOR 2 ]
[ DOOR 3 ]

[ RESTART ]
```

### UI References in `GameManager`

The `GameManager` currently references these TextMeshPro UI components:

```csharp
public TextMeshProUGUI hpText;
public TextMeshProUGUI goldText;
public TextMeshProUGUI eventText;
public TextMeshProUGUI floorText;
```

These references are assigned manually through the Unity Inspector.

---

## 5. Current Game State

The main runtime state is stored in `GameManager`:

```csharp
private int hp = 100;
private int gold = 0;
private int floor = 1;
private bool isGameOver = false;
```

### State Meaning

| Variable | Meaning |
|---|---|
| `hp` | Player health points |
| `gold` | Current gold collected during the run |
| `floor` | Current tower floor |
| `isGameOver` | Blocks door logic after death |

---

## 6. Implemented Features

### 6.1 Door Interaction

Each door is a Unity UI Button.

The buttons call:

```csharp
OpenDoor(int doorType)
```

Configured in the Unity Inspector:

```text
Door1Button → OpenDoor(0)
Door2Button → OpenDoor(1)
Door3Button → OpenDoor(2)
```

The `doorType` parameter is used to distinguish between different door behaviors.

---

### 6.2 Random Events

Currently supported events:

```text
Treasure
Monster
Heal
```

#### Treasure

Adds gold to the player.

```text
You found X gold!
```

#### Monster

Deals damage to the player.

Damage currently scales with floor:

```text
damage = random base damage + floor
```

#### Heal

Restores HP.

```text
You healed X HP!
```

---

### 6.3 Floor System

The run starts at:

```text
FLOOR: 1
```

Every time the player opens a door and survives:

```csharp
floor++;
```

If the player dies, the floor does not increase.

---

### 6.4 Damage Scaling

Monster damage increases with floor.

Current logic:

```csharp
int damage = Random.Range(10, 30) + floor;
```

This makes later floors more dangerous and creates natural difficulty progression.

---

### 6.5 Game Over

When HP reaches zero or below:

```csharp
if (hp <= 0)
```

The game:

```text
Sets isGameOver = true
Sets hp = 0
Displays "Game Over!"
Updates UI
Blocks further door actions
```

Current behavior:

```csharp
if (isGameOver)
{
    return;
}
```

This means door buttons may still be clickable, but game logic does not execute after Game Over.

---

### 6.6 Restart

A Restart button calls:

```csharp
RestartGame()
```

Current restart behavior:

```text
isGameOver = false
hp = 100
gold = 0
floor = 1
eventText = "Choose a door..."
UpdateUi()
```

This resets the run and allows the player to play again.

---

## 7. Current Code Architecture

### Main Class

```text
GameManager
```

### Current Responsibilities

`GameManager` currently handles:

```text
Game state
Door click handling
Random event generation
HP / Gold / Floor updates
Game Over logic
Restart logic
UI synchronization
Debug logging
```

### Main Methods

| Method | Responsibility |
|---|---|
| `OpenDoor(int doorType)` | Entry point from door buttons |
| `HandleRandomEvent(int doorType)` | Executes event logic based on door type |
| `UpdateUi()` | Synchronizes UI with current game state |
| `RestartGame()` | Resets the run |
| `Start()` | Initializes UI at scene start |

---

## 8. Current Door Design

### Door 1 — Safe Door

Implemented / in progress.

Target probability distribution:

```text
50% Treasure
30% Heal
20% Monster
```

Purpose:

```text
Low risk
Good for survival
Lower tension
```

---

### Door 2 — Balanced Door

Not implemented yet.

Planned probability distribution:

```text
33% Treasure
33% Heal
34% Monster
```

Purpose:

```text
Default balanced choice
Medium risk
Predictable progression
```

---

### Door 3 — Risky Door

Not implemented yet.

Planned probability distribution:

```text
40% Treasure
10% Heal
50% Monster
```

Purpose:

```text
High risk
High reward
Fast progression but dangerous
```

Future improvement:

```text
Risky Door should probably give higher gold rewards than Safe Door.
```

---

## 9. Current Important Design Decisions

### 9.1 UI is driven by game state

The UI should not contain the real source of truth.

Correct flow:

```text
Game state changes
↓
UpdateUi()
↓
UI reflects state
```

Example:

```csharp
hpText.text = "HP: " + hp;
goldText.text = "GOLD: " + gold;
floorText.text = "FLOOR: " + floor;
```

---

### 9.2 `UpdateUi()` should not change game state

Important rule:

```text
UpdateUi() only displays state.
It should not modify hp, gold, floor, or game status.
```

Bad example:

```csharp
private void UpdateUi()
{
    floor++; // wrong responsibility
}
```

Good example:

```csharp
private void UpdateUi()
{
    floorText.text = "FLOOR: " + floor;
}
```

---

### 9.3 Door buttons use parameters

Instead of creating separate methods like:

```csharp
OpenDoor1()
OpenDoor2()
OpenDoor3()
```

The project uses:

```csharp
OpenDoor(int doorType)
```

This is more scalable.

---

### 9.4 Game Over is blocked by state, not by disabling buttons yet

Current solution:

```csharp
if (isGameOver)
{
    return;
}
```

Future improvement:

```text
Disable door buttons visually after Game Over.
Enable them again on Restart.
```

---

## 10. Known Issues / Improvements

### 10.1 GameManager is doing too much

Currently `GameManager` handles everything.

This is acceptable for the prototype, but later it should be split.

Possible future classes:

```text
PlayerState
DoorManager
EventResolver
UiManager
RunManager
```

---

### 10.2 Events are duplicated

Treasure, Monster, and Heal logic may become duplicated when Door 2 and Door 3 are implemented.

Future improvement:

Create separate methods:

```csharp
private void ApplyTreasure(int minReward, int maxReward)
private void ApplyMonster(int minDamage, int maxDamage)
private void ApplyHeal(int minHeal, int maxHeal)
```

---

### 10.3 Event types should become enum

Future improvement:

```csharp
public enum DoorEventType
{
    Treasure,
    Monster,
    Heal
}
```

This will make event logic cleaner and safer.

---

### 10.4 Door configuration should become data-driven

Later, door probabilities should not be hardcoded.

Potential future structure:

```text
DoorData
├── doorName
├── treasureChance
├── healChance
├── monsterChance
├── minReward
├── maxReward
├── minDamage
├── maxDamage
```

This could be implemented using:

```text
ScriptableObject
```

---

## 11. Suggested Next Steps

### Next Task 1 — Finish Door 1

Ensure Door 1 uses:

```text
50% Treasure
30% Heal
20% Monster
```

Use `probability = Random.Range(0, 100)`.

---

### Next Task 2 — Implement Door 2

Balanced Door:

```text
0-32   Treasure
33-65  Heal
66-99  Monster
```

---

### Next Task 3 — Implement Door 3

Risky Door:

```text
0-39   Treasure
40-49  Heal
50-99  Monster
```

Potentially increase Door 3 reward:

```text
Treasure reward = Random.Range(25, 60)
```

---

### Next Task 4 — Refactor repeated event logic

Create helper methods:

```csharp
ApplyTreasure(...)
ApplyMonster(...)
ApplyHeal(...)
```

This will reduce duplication and make doors easier to configure.

---

### Next Task 5 — Improve Game Over UX

Possible improvements:

```text
Disable door buttons
Show Restart button only after Game Over
Add final score display
Show highest floor reached
```

---

### Next Task 6 — Add Win Condition

Possible first win condition:

```text
If gold >= 500
→ YOU WIN
```

Alternative:

```text
Reach Floor 50
→ YOU ESCAPED THE TOWER
```

---

## 12. Current Learning Notes

Important Unity concepts learned so far:

```text
GameObject
Component
Canvas
TextMeshPro
Button OnClick
Inspector references
MonoBehaviour
Start()
Public methods callable from UI
Random.Range()
Debug.Log()
Game state vs UI state
```

Important C# concepts practiced:

```text
private fields
public methods
method parameters
if / else if / else
bool state flags
string concatenation
string interpolation
method extraction
basic refactoring
```

---

## 13. File Placement

Recommended documentation file path inside Unity project:

```text
DoorsTower/
└── Docs/
    └── DoorsTower_DEVLOG_v0.1.md
```

If the `Docs` folder does not exist, create it at the root of the Unity project, next to folders like:

```text
Assets/
Packages/
ProjectSettings/
```

Do **not** put this documentation inside `Library/`, because Unity regenerates that folder and it should not be tracked manually.

---

## 14. Recommended Git Tracking

This file should be committed to Git.

Suggested commit message:

```text
Add development log v0.1
```

Recommended `.gitignore` should exclude Unity-generated folders such as:

```text
Library/
Temp/
Obj/
Build/
Builds/
Logs/
UserSettings/
```

But it should keep:

```text
Assets/
Packages/
ProjectSettings/
Docs/
```

---

## 15. Resume Prompt for New Chat

Use this prompt to continue in a new chat:

```text
We are building a Unity mobile roguelite prototype called Doors Tower.
Current stack: Unity 6, C#, Canvas UI, TextMeshPro.
Current features: HP, Gold, Floor, EventText, three door buttons, restart button, Game Over, floor-based monster damage, and door buttons call OpenDoor(int doorType).
Door 1 is being implemented as Safe Door: 50% Treasure, 30% Heal, 20% Monster.
Next steps: finish Door 1, implement Door 2 Balanced, Door 3 Risky, then refactor repeated Treasure/Monster/Heal logic into helper methods.
Continue in learning-by-doing mode. Do not just give full solutions unless I ask. Guide me step by step.
```
