# Undoable Move System using the Command Pattern - Starting Project

This is the **starting branch** for the companion project to the tutorial on implementing undo in a grid puzzle game with the Command pattern ([**Link**](https://codeandarchitecture.hashnode.dev/implementing-the-undo-functionality-in-unity-using-the-command-pattern)). The core gameplay loop already works - the player can slide around a grid toward a goal tile - but **there's no way to undo a move yet**. You'll build that from scratch by following the tutorial, encapsulating each move as an `ICommand` object.

> Want to see the finished result first, or just grab the working code? Switch to the [`main`](https://github.com/BerinMadzakCoding/CommandPattern_PuzzleGame) branch instead.

## What's Already Here

* **`GridManager.cs`** - loads a `MapLayout` ScriptableObject and resolves tile collisions/obstacles
* **`PlayerController.cs`** - handles the player's sliding movement across the grid
* **`GameManager.cs`** - tracks turn count and overall game state
* **`MoveButton.cs`** - on-screen input for the four movement directions
* **`HistoryIcon.cs`** - a UI component for displaying move-history icons, already built but not yet wired up to anything
* **`CellType.cs`** / **`MapLayout.cs`** - grid data definitions
* Three sample maps (`Map1`–`Map3`) under `Assets/Maps`

## What You'll Build

Following the tutorial, you'll add:

* An `ICommand` interface (`Execute`, `Undo`, `IsValid`)
* A `MoveCommand` that captures origin/target coordinates and can reverse itself
* A `CommandManager` to execute commands and maintain a multi-step undo history stack
* A `CommandHistoryManager` that listens for command events and updates the move-history UI
* An `UndoButton` to trigger undo from the player

## Tech Stack & Version

* **Engine:** Unity 6000.3.17f1 (or newer)
* **Language:** C#

## Getting Started

```bash
git clone https://github.com/BerinMadzakCoding/CommandPattern_PuzzleGame
cd CommandPattern_PuzzleGame
git checkout StartingProject
```

Open the project with Unity Hub, open the `PuzzleGame` scene under `Assets/Scenes`, and follow along with the tutorial from the top.

## Repository Branches

* **`StartingProject`** *(you are here)* - grid movement only, no undo functionality yet.
* **[`main`](https://github.com/BerinMadzakCoding/CommandPattern_PuzzleGame)** - the fully finished project with complete command/undo logic and move-history UI.
