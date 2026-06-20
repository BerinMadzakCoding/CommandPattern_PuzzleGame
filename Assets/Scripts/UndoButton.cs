using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(UndoLastCommand);
    }

    private void UndoLastCommand()
    {
        CommandManager.Instance.UndoCommand();
    }
}
