using System;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Node.Dialogue
{
    [Serializable]
    public sealed class DialogueNodeData : NodeData
    {
        [SerializeField] 
        internal string Data;
    }
}