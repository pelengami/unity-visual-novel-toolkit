using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Controls.Dialog;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.Node.SetBackgroundNode
{
	sealed class SetBackgroundNodeView : NodeView
	{
		private const float MarginTop = 70;
		private const float MarginLeft = 15;

		private readonly NodeParametersPanel _nodeParametersPanel;

		public SetBackgroundNodeView(string title, Vector2 position) : base(title, position)
		{
			DefaultNodeStyle = StylesCollection.GetStyle(VntStyles.SetBackgroundNode);
			SelectedNodeStyle = StylesCollection.GetStyle(VntStyles.SetBackgroundNode);
		}
		
		public string TexturePath { get; set; }

		public override void Draw()
		{
			base.Draw();
		}
	}
}