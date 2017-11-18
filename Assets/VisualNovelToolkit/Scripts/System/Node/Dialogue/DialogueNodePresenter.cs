using Assets.VisualNovelToolkit.Scripts.System.Port;

namespace Assets.VisualNovelToolkit.Scripts.System.Node.Dialogue
{
    internal sealed class DialogueNodePresenter : NodePresenter
    {
        private readonly DialogueNodeView _nodeView;
        private readonly DialogueNodeData _nodeData;

        public DialogueNodePresenter(DialogueNodeView nodeView, DialogueNodeData nodeData,
            PortPresenter portPresenter,
            PortPresenter portOutPresenter) :
            base(nodeView, nodeData, portPresenter, portOutPresenter)
        {
            _nodeView = nodeView;
            _nodeData = nodeData;

            _nodeView.Clicked += NodeViewOnClicked;
        }

        private void NodeViewOnClicked()
        {
        }
    }
}