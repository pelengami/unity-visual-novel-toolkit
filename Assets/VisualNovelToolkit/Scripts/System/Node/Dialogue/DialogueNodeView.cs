using System;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Node.Dialogue
{
    internal sealed class DialogueNodeView : NodeView
    {
        public DialogueNodeView(string title, Vector2 position) : base(position)
        {
            DefaultNodeStyle = StylesCollection.GetStyle(VntStyles.DialogueNode);
            SelectedNodeStyle = StylesCollection.GetStyle(VntStyles.DialogueNode);

            Width = 130;
            Height = 75;
        }

        public event Action Clicked;

        public override void Draw()
        {
            base.Draw();

            GUILayout.BeginArea(BRect);
            GUILayout.BeginVertical();

            if (GUILayout.Button("Click"))
                Clicked?.Invoke();

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}