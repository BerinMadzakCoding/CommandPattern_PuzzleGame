using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform victoryScreen;

    private void OnEnable()
    {
        player.OnMoveCompleted += HandleMoveCompleted;
    }

    private void OnDisable()
    {
        player.OnMoveCompleted -= HandleMoveCompleted;
    }

    private void HandleMoveCompleted(Vector2Int coord)
    {
        if (coord == gridManager.EndCoord)
        {
            victoryScreen.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        victoryScreen.gameObject.SetActive(false);
        gridManager.GenerateMap();
        player.SetCoords();
    }
}