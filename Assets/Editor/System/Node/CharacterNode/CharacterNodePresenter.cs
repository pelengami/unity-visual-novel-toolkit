using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node.SetBackgroundNode;
using Assets.Editor.ToolkitGui.Controls.Dialog;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using UnityEngine;

namespace Assets.Editor.System.Node.CharacterNode
{
	sealed class CharacterNodePresenter : NodePresenter
	{
		private readonly INodeView _nodeView;
		private readonly NodeParametersPanel _nodeParametersPanel;

		public CharacterNodePresenter(INodeView nodeView,
			NodeData nodeData,
			ConnectionPointPresenter connectionPointInPresenter,
			ConnectionPointPresenter connectionPointOutPresenter) :
			base(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter)
		{
			_nodeView = nodeView;

			var parameter = new NodeParameter("Texture Path")
			{
				IsClickable = true
			};

			_nodeParametersPanel = new NodeParametersPanel(new List<NodeParameter>
			{
				parameter
			});
		}

		public static NodePresenter Create(Vector2 position)
		{
			var nodeView = new CharacterNodeView(LocalizationStrings.CharacterNode, position);
			var connectionPointInPresenter = new ConnectionPointPresenter(new ConnectionPointView(nodeView, ConnectionPointType.In));
			var connectionPointOutPresenter = new ConnectionPointPresenter(new ConnectionPointView(nodeView, ConnectionPointType.Out));
			var nodeData = new CharacterNodeData();
			var nodePresenter = new CharacterNodePresenter(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter);
			return nodePresenter;
		}

		public override void Draw()
		{
			base.Draw();

			base.DrawParameters(_nodeParametersPanel);
		}
	}
}