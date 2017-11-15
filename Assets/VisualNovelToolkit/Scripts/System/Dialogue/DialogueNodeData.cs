using System;
using System.Collections.Generic;
using Assets.Editor.System.Node;
using UnityEngine;

namespace Assets.Editor.System.Dialogue
{
    [Serializable]
    public sealed class DialogueNodeData : NodeData
    {
        [SerializeField]
        public string Data;
        
        private List<DialogueLink> _dialogueLinks = new List<DialogueLink>();
    }
}