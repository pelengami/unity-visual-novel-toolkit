using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.Node.CharacterNode.DialogueNode
{
	sealed class DialogueNodeView : NodeView
	{
		private const float MarginTop = 70;
		private const float MarginLeft = 15;

		private readonly NodeParametersPanel _nodeParametersPanel;

		public DialogueNodeView(string title, Vector2 position) : base(title, position)
		{
			DefaultNodeStyle = StylesCollection.GetStyle(VntStyles.DialogueNode);
			SelectedNodeStyle = StylesCollection.GetStyle(VntStyles.DialogueNode);
		}

		public string TexturePath { get; set; }

		public override void Draw()
		{
			base.Draw();
		}
	}
}