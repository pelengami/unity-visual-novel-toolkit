using System;

namespace Assets.Editor.System.Dialogue
{
    sealed class DialogueContidion : IDialogueCondition
    {
        private readonly Func<bool> _action;

        public DialogueContidion(Func<bool> action)
        {
            _action = action;
        }

        public bool Evaluate()
        {
            return _action();
        }
    }
}