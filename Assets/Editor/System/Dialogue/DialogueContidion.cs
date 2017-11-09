namespace Editor.System.Dialogue
{
    sealed class DialogueContidion : IDialogueCondition
    {
        public DialogueContidion(string condition)
        {
            Condition = condition;
        }

        public string Condition { get; set; }

        public bool Evaluate(string value)
        {
            return Condition == value;
        }
    }
}