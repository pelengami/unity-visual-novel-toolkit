using System;
using System.Collections.Generic;

namespace Editor.System.Dialogue
{
    sealed class DialogueNodeData
    {
        private Guid _id;
        private List<DialogueLink> _dialogueLinks = new List<DialogueLink>();

        public DialogueNodeData()
        {
        }

        public Guid Id
        {
            get { return _id; }
        }

        public void AddLink(DialogueLink dialogueLink)
        {
            _dialogueLinks.Add(dialogueLink);
        }
    }
}