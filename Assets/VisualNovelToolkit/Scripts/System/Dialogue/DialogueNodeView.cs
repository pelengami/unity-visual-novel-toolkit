using Assets.Editor.System.Node;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.Dialogue
{
    internal sealed class DialogueNodeView : NodeView
    {
        public DialogueNodeView(string title, Vector2 position) : base(title, position)
        {
            DefaultNodeStyle = StylesCollection.GetStyle(VntStyles.DialogueNode);
            SelectedNodeStyle = StylesCollection.GetStyle(VntStyles.DialogueNode);

            Width = 130;
            Height = 75;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}