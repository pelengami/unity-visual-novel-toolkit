using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node;

namespace Editor.System.Dialogue
{
    sealed class DialogueNodePresenter : NodePresenter
    {
        public DialogueNodePresenter(INodeView nodeView, NodeData nodeData, 
            ConnectionPointPresenter connectionPointInPresenter, 
            ConnectionPointPresenter connectionPointOutPresenter) : 
            base(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter)
        {
            
        }
    }
}