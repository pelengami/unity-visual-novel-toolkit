using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.Node.CharacterNode.AnswerNode
{
	sealed class AnswerNodeView : NodeView
	{
		private const float MarginTop = 70;
		private const float MarginLeft = 15;

		private readonly NodeParametersPanel _nodeParametersPanel;

		public AnswerNodeView(string title, Vector2 position) : base(title, position)
		{
			DefaultNodeStyle = StylesCollection.GetStyle(VntStyles.AnswerNode);
			SelectedNodeStyle = StylesCollection.GetStyle(VntStyles.AnswerNode);
		}

		public string TexturePath { get; set; }

		public override void Draw()
		{
			base.Draw();
		}
	}
}