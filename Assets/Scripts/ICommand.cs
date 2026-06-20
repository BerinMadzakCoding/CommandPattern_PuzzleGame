public interface ICommand
{
    bool IsValid { get; }
    void Execute();
    void Undo();
}
