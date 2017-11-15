using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.System.Dialogue
{
    sealed class DialogueLink
    {
        private DialogueNodeData _nextNodeData;
        private List<IDialogueCondition> _dialogueConditions = new List<IDialogueCondition>();

        public DialogueLink()
        {
        }

        public void AddCondition(IDialogueCondition dialogueCondition)
        {
            _dialogueConditions.Add(dialogueCondition);
        }

        public void SetNextNode(DialogueNodeData nodeData)
        {
            _nextNodeData = nodeData;
        }

        public DialogueNodeData GetNextNode()
        {
            return _nextNodeData;
        }

        public bool IsAvailable()
        {
            return _dialogueConditions.All(c => c.Evaluate());
        }
    }
}