using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector2Int direction;
    private Vector2Int originCoord;
    private Vector2Int targetCoord;
    private ArrowDirection arrowDirection;

    public ArrowDirection ArrowDir => arrowDirection;
    public bool IsValid => targetCoord != originCoord;

    public MoveCommand(Vector2Int direction)
    {
        this.direction = direction;
        arrowDirection = VectorToArrowDirection(direction);
    }

    private ArrowDirection VectorToArrowDirection(Vector2Int dir)
    {
        if (dir == Vector2Int.up) return ArrowDirection.Up;
        if (dir == Vector2Int.down) return ArrowDirection.Down;
        if (dir == Vector2Int.left) return ArrowDirection.Left;
        return ArrowDirection.Right;
    }

    public void Execute()
    {
        // Record where the player started before moving
        originCoord = PlayerController.Instance.CurrentCoord;

        // Calculate the destination tile using the existing sliding logic
        Vector2Int finalCoord = originCoord;
        Vector2Int nextCoord = originCoord + direction;

        while (!GridManager.Instance.IsBlocked(nextCoord))
        {
            finalCoord = nextCoord;
            nextCoord += direction;
        }

        // If no movement is possible, don't execute
        if (finalCoord == originCoord) return;

        targetCoord = finalCoord;

        // Command the controller to slide to the target destination
        PlayerController.Instance.Move(targetCoord);
    }

    public void Undo()
    {
        // To reverse, command the controller to move back to the cached origin
        PlayerController.Instance.Move(originCoord, true);
        
        // Revert the turn count in the GameManager
        GameManager.Instance.DecrementTurnCount();
    }
}
