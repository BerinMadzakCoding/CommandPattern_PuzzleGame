using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance { get; private set; }

    public event Action<ICommand> OnCommandExecuted;
    public event Action OnCommandUndone;

    private Stack<ICommand> commandHistory = new Stack<ICommand>();

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

    public void ExecuteCommand(ICommand command)
    {
        if (PlayerController.Instance.IsMoving) return;

        command.Execute();

        if (command.IsValid) 
        {
            commandHistory.Push(command);
            OnCommandExecuted?.Invoke(command);
        }
    }

    public void UndoCommand()
    {
        if (PlayerController.Instance.IsMoving || commandHistory.Count == 0) return;

        ICommand lastCommand = commandHistory.Pop();
        lastCommand.Undo();

        OnCommandUndone?.Invoke();
    }

    public void ClearHistory() => commandHistory.Clear();
}
