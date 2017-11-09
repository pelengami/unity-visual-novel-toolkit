using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using UnityEngine;

namespace Assets.Editor.System.Node.CharacterNode.DialogueNode
{
	sealed class DialogueNodePresenter : NodePresenter
	{
		private readonly INodeView _nodeView;
		private readonly NodeParametersPanel _nodeParametersPanel;

		public DialogueNodePresenter(INodeView nodeView,
			NodeData nodeData,
			ConnectionPointPresenter connectionPointInPresenter,
			ConnectionPointPresenter connectionPointOutPresenter) :
			base(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter)
		{
			_nodeView = nodeView;

			var dialogueParam = new NodeParameter(LocalizationStrings.Dialogue)
			{
				IsEditable = true,
				Height = 80,
				Width = 200
			};

			_nodeParametersPanel = new NodeParametersPanel(new List<NodeParameter>
			{
				dialogueParam
			})
			{
				Width = 320
			};
		}

		public static NodePresenter Create(Vector2 position)
		{
			var nodeView = new DialogueNodeView(LocalizationStrings.DialogueNode, position);
			var connectionPointInPresenter = new ConnectionPointPresenter(new ConnectionPointView(ConnectionPointType.In));
			var connectionPointOutPresenter = new ConnectionPointPresenter(new ConnectionPointView(ConnectionPointType.Out));
			var nodeData = new DialogueNodeData();
			var nodePresenter = new DialogueNodePresenter(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter);
			return nodePresenter;
		}

		public override void Draw()
		{
			base.Draw();

			DrawParameters(_nodeParametersPanel);
		}
	}
}