# Undoable Move System using the Command Pattern

A grid-based puzzle game built in **Unity 6** that uses the **Command Design Pattern** to give the player full, multi-step undo over their moves. This repository is the complete companion project for the technical tutorial published on [**Code & Architecture**](https://codeandarchitecture.hashnode.dev/implementing-the-undo-functionality-in-unity-using-the-command-pattern).

The architecture turns every player move into a self-contained `ICommand` object that knows how to execute and reverse itself, decoupling input handling and UI from the actual movement logic — and making "what happened, in what order" trivial to track and roll back.

> You are looking at the **finished, complete project**. Want to build it yourself step by step? Check out the [`StartingProject`](https://github.com/BerinMadzakCoding/CommandPattern_PuzzleGame/tree/StartingProject) branch and follow along with the tutorial.

## Key Features

* **Encapsulated Actions** — An `ICommand` interface (`Execute` / `Undo`) turns each grid move into a self-contained `MoveCommand` object, so the invoker never needs to know how a move is performed or reversed.
* **Multi-Step Undo Stack** — `CommandManager` keeps a history stack of executed commands, letting the player undo several moves in sequence, not just the most recent one.
* **Self-Validating Commands** — Every command exposes an `IsValid` flag (a `MoveCommand` is invalid if the target tile equals the origin tile), so no-op moves like walking into a wall are automatically filtered out before they're recorded.
* **Event-Driven History UI** — `CommandManager` raises `OnCommandExecuted` / `OnCommandUndone` events, which a separate `CommandHistoryManager` listens to in order to render move-history icons — no direct reference between gameplay and UI.
* **Silent Undo Replay** — `PlayerController.Move()` accepts an optional `undo` flag, so reversing a move doesn't re-fire the same completion events a normal move would.

## Tech Stack & Version

* **Engine:** Unity 6000.3.17f1 (or newer)
* **Language:** C#

## Getting Started

1. Clone the repository and open it with Unity Hub.
2. Open the `PuzzleGame` scene under `Assets/Scenes`.
3. Press Play. Use the directional buttons to slide across the grid toward the goal tile, and use the undo button to step back through your move history.

```bash
git clone https://github.com/BerinMadzakCoding/CommandPattern_PuzzleGame
cd CommandPattern_PuzzleGame
```

## Repository Branches

* **`main`** *(you are here)* — the finished project: full command/undo logic and move-history UI.
* **[`StartingProject`](https://github.com/BerinMadzakCoding/CommandPattern_PuzzleGame/tree/StartingProject)** — the baseline sandbox with grid movement only and no undo functionality. Use this if you want to implement the Command pattern yourself by following the tutorial.
