using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node;
using Editor.Localization;
using UnityEngine;

namespace Assets.Editor.System.Dialogue
{
    sealed class DialogueNodePresenter : NodePresenter
    {
        public DialogueNodePresenter(INodeView nodeView, DialogueNodeData nodeData, 
            ConnectionPointPresenter connectionPointInPresenter, 
            ConnectionPointPresenter connectionPointOutPresenter) : 
            base(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter)
        {
            
        }
        
        public static NodePresenter Create(Vector2 position)
        {
            var nodeView = new DialogueNodeView(LocalizationStrings.DialogueNode, position);
            var connectionPointInPresenter = new ConnectionPointPresenter(new ConnectionPointView(ConnectionPointType.In));
            var connectionPointOutPresenter = new ConnectionPointPresenter(new ConnectionPointView(ConnectionPointType.Out));
            var go = new GameObject(LocalizationStrings.DialogueNode);
            var nodeData = go.AddComponent<DialogueNodeData>();
            var nodePresenter = new DialogueNodePresenter(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter);
            return nodePresenter;
        }
    }
}