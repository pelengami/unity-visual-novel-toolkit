namespace Editor.System.Dialogue
{
    interface IDialogueCondition
    {
        bool Evaluate(string value);
    }
}