using System;
using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.ToolkitGui.Controls.Dialog;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using UnityEngine;

namespace Assets.Editor.System.Node.SetBackgroundNode
{
	sealed class SetBackgroundNodePresenter : NodePresenter
	{
		private readonly SetBackgroundNodeView _nodeView;
		private readonly NodeParametersPanel _nodeParametersPanel;

		public SetBackgroundNodePresenter(SetBackgroundNodeView nodeView,
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

			parameter.Clicked += OnTextureParameterClicked;

			_nodeParametersPanel = new NodeParametersPanel(new List<NodeParameter>
			{
				parameter
			});
		}

		public static NodePresenter Create(Vector2 position)
		{
			var nodeView = new SetBackgroundNodeView(LocalizationStrings.SetBackgroundNode, position);
			var connectionPointInPresenter = new ConnectionPointPresenter(new ConnectionPointView(nodeView, ConnectionPointType.In));
			var connectionPointOutPresenter = new ConnectionPointPresenter(new ConnectionPointView(nodeView, ConnectionPointType.Out));
			var nodeData = new SetBackgroundNodeData();
			var nodePresenter = new SetBackgroundNodePresenter(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter);
			return nodePresenter;
		}

		public override void Draw()
		{
			base.Draw();

			base.DrawParameters(_nodeParametersPanel);
		}

		private void OnTextureParameterClicked()
		{
			var dialog = new LoadTextureDialog();
			dialog.ShowDialog();

			if (dialog.Result)
				_nodeView.TexturePath = dialog.Path;
		}
	}
}