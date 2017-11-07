using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.Node.CharacterNode
{
	sealed class CharacterNodeView : NodeView
	{
		private const float MarginTop = 70;
		private const float MarginLeft = 15;

		private readonly NodeParametersPanel _nodeParametersPanel;

		public CharacterNodeView(string title, Vector2 position) : base(title, position)
		{
			DefaultNodeStyle = StylesCollection.GetStyle(VntStyles.CharacterNode);
			SelectedNodeStyle = StylesCollection.GetStyle(VntStyles.CharacterNode);
		}

		public string TexturePath { get; set; }

		public override void Draw()
		{
			base.Draw();
		}
	}
}