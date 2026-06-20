using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event System.Action OnGameRestarted;

    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform victoryScreen;
    [SerializeField] private Transform defeatScreen;
    [SerializeField] private TMP_Text turnCountText;

    public int TurnsToComplete { get; set; }
    private int turnCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerController.Instance.OnMoveCompleted += HandleMoveCompleted;
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnMoveCompleted -= HandleMoveCompleted;
    }

    private void HandleMoveCompleted(Vector2Int coord)
    {
        IncrementTurnCount();
        UpdateUI();
        if (coord == GridManager.Instance.EndCoord)
        {
            victoryScreen.gameObject.SetActive(true);
        }
        else if (turnCount >= TurnsToComplete)
        {
            defeatScreen.gameObject.SetActive(true);
        }
    }

    public void IncrementTurnCount()
    {
        turnCount++;
        UpdateUI();
    }

    public void DecrementTurnCount()
    {
        turnCount--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        turnCountText.text = "Turns: " + turnCount.ToString() + "/" + TurnsToComplete.ToString();
    }

    public void RestartGame()
    {
        OnGameRestarted?.Invoke();
        CommandManager.Instance.ClearHistory();
        turnCount = 0;
        victoryScreen.gameObject.SetActive(false);
        defeatScreen.gameObject.SetActive(false);
        GridManager.Instance.GenerateMap();
        PlayerController.Instance.SetCoords();
        UpdateUI();
    }
}