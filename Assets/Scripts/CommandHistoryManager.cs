using UnityEngine;

public class CommandHistoryManager : MonoBehaviour
{
    [SerializeField] private HistoryIcon historyIconPrefab;

    private void Start()
    {
        GameManager.Instance.OnGameRestarted += ClearHistory;
        CommandManager.Instance.OnCommandExecuted += HandleCommandExecuted;
        CommandManager.Instance.OnCommandUndone += HandleCommandUndone;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameRestarted -= ClearHistory;
        CommandManager.Instance.OnCommandExecuted -= HandleCommandExecuted;
        CommandManager.Instance.OnCommandUndone -= HandleCommandUndone;
    }

    private void HandleCommandExecuted(ICommand command)
    {
        HistoryIcon icon = Instantiate(historyIconPrefab, transform);
        if(command is MoveCommand moveCommand)
        {
            icon.SetValue(moveCommand.ArrowDir);
        }
    }

    private void HandleCommandUndone()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }

    public void ClearHistory()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
