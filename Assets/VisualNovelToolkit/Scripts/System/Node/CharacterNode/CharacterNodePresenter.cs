using System.Collections.Generic;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node.SetBackgroundNode;
using Assets.Editor.ToolkitGui.Controls.Dialog;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using Editor.Localization;
using UnityEngine;

namespace Assets.Editor.System.Node.CharacterNode
{
	sealed class CharacterNodePresenter : NodePresenter
	{
		private readonly CharacterNodeView _nodeView;
		private readonly CharacterNodeData _nodeData;
		private readonly NodeParametersPanel _nodeParametersPanel;

		public CharacterNodePresenter(CharacterNodeView nodeView,
			CharacterNodeData nodeData,
			ConnectionPointPresenter connectionPointInPresenter,
			ConnectionPointPresenter connectionPointOutPresenter) :
			base(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter)
		{
			_nodeView = nodeView;
			_nodeData = nodeData;

			var textureParameter = new NodeParameter(LocalizationStrings.TexturePath)
			{
				IsClickable = true
			};

			var xParameter = new NodeParameter(LocalizationStrings.X)
			{
				IsEditable = true
			};
			var yParameter = new NodeParameter(LocalizationStrings.Y)
			{
				IsEditable = true
			};
			var widthParameter = new NodeParameter(LocalizationStrings.Width)
			{
				IsEditable = true
			};
			var heightParameter = new NodeParameter(LocalizationStrings.Height)
			{
				IsEditable = true
			};

			textureParameter.Clicked += OnTextureParameterClicked;

			_nodeParametersPanel = new NodeParametersPanel(new List<NodeParameter>
			{
				textureParameter,
				xParameter,
				yParameter,
				widthParameter,
				heightParameter
			});
		}

		public static NodePresenter Create(Vector2 position)
		{
			var nodeView = new CharacterNodeView(LocalizationStrings.CharacterNode, position);
			var connectionPointInPresenter = new ConnectionPointPresenter(new ConnectionPointView(ConnectionPointType.In));
			var connectionPointOutPresenter = new ConnectionPointPresenter(new ConnectionPointView(ConnectionPointType.Out));
			var nodeData = new CharacterNodeData();
			var nodePresenter = new CharacterNodePresenter(nodeView, nodeData, connectionPointInPresenter, connectionPointOutPresenter);
			return nodePresenter;
		}

		public override void Draw()
		{
			base.Draw();

			DrawParameters(_nodeParametersPanel);
		}

		private void OnTextureParameterClicked()
		{
			var dialog = new LoadTextureDialog();
			dialog.ShowDialog();

			if (dialog.Result)
			{
				_nodeView.TexturePath = dialog.Path;
				_nodeData.TexturePath = dialog.Path;
			}
		}
	}
}