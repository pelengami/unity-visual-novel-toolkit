using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.System.ConnectionLine;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.VisualNovelToolkitEditor
{
	sealed class VntPresenter
	{
		private readonly IVntView _vntView;
		private readonly StylesCollection _stylesCollection = new StylesCollection();
		private readonly List<NodePresenter> _nodePresenters = new List<NodePresenter>();
		private readonly List<ConnectionPresenter> _connectionPresenters = new List<ConnectionPresenter>();
		private readonly ConnectionPresenter _connectionToMousePresenter;
		private readonly VntModel _vntModel;

		private NodePresenter _selectedNode;

		private NodePresenter _selectedNodePresenter;
		private ConnectionPointPresenter _selectedPointPresenter;

		public VntPresenter(IVntView vntView, VntModel vntModel)
		{
			_vntView = vntView;
			_vntModel = vntModel;
			_vntView.MouseClicked += VntViewOnMouseClicked;
			_vntView.Awaked += VntViewOnAwaked;
			_vntView.OnGui += VntViewOnGui;
			_vntView.Drag += VntViewOnDrag;
			_vntView.ProcessedEvents += VntViewOnProcessedEvents;

			_connectionToMousePresenter = new ConnectionPresenter(new ConnectionView(), new ConnectionModel());
		}

		private void VntViewOnAwaked()
		{
			_stylesCollection.LoadStyles();
			_vntModel.LoadNodes();
		}

		private void VntViewOnGui()
		{
			foreach (var nodePresenter in _nodePresenters)
				nodePresenter.Draw();

			foreach (var connectionPresenter in _connectionPresenters)
				connectionPresenter.Draw();

			if (_selectedNodePresenter != null && _selectedNodePresenter.SelectedConnectionPointPresenter != null)
				_connectionToMousePresenter.Draw(_selectedNodePresenter.SelectedConnectionPointPresenter.Rect);
		}

		private void VntViewOnProcessedEvents(Event e)
		{
			foreach (var nodePresenter in _nodePresenters)
				nodePresenter.ProcessEvents(e);

			foreach (var connectionPresenter in _connectionPresenters)
				connectionPresenter.ProcessEvents(e);

			_connectionToMousePresenter.ProcessEvents(e);
		}

		private void VntViewOnDrag(Vector2 vector2)
		{
			foreach (var nodePresenter in _nodePresenters)
				nodePresenter.Drag(vector2);
		}

		private void VntViewOnMouseClicked(Vector2 mousePosition)
		{
			var setBackgroundContextMenuItem = new ContextMenuItem
			{
				Title = LocalizationStrings.SetBackgroundNode,
			};

			setBackgroundContextMenuItem.Clicked += OnSetBackgroundClicked;

			var contextMenuItems = new List<ContextMenuItem>
			{
				setBackgroundContextMenuItem
			};

			_vntView.ShowContextMenu(mousePosition, contextMenuItems);
		}

		private void OnSetBackgroundClicked(Vector2 mousePosition)
		{
			CreateNode(mousePosition);
		}

		private void CreateNode(Vector2 mousePosition)
		{
			var defaultNodeStyle = _stylesCollection.GetStyle(StyleNames.SetBackgroundNode);
			var connectionPointInStyle = _stylesCollection.GetStyle(StyleNames.ConnectionIn);
			var connectionPointOutStyle = _stylesCollection.GetStyle(StyleNames.ConnectionOut);

			var nodeView = new NodeView(defaultNodeStyle, defaultNodeStyle, LocalizationStrings.SetBackgroundNode, mousePosition);

			var connectionPointInPresenter = new ConnectionPointPresenter(new ConnectionPointView(nodeView, connectionPointInStyle, ConnectionPointType.In));
			var connectionPointOutPresenter = new ConnectionPointPresenter(new ConnectionPointView(nodeView, connectionPointOutStyle, ConnectionPointType.Out));

			var nodePresenter = new NodePresenter(nodeView, connectionPointInPresenter, connectionPointOutPresenter);
			nodePresenter.ConnectionPointSelected += NodePresenterOnConnectionPointSelected;
			nodePresenter.ConnectionPointUnSelected += NodePresenterOnConnectionPointUnSelected;

			_nodePresenters.Add(nodePresenter);
		}

		private void NodePresenterOnConnectionPointSelected(NodePresenter nodePresenter, ConnectionPointPresenter connectionPointPresenter)
		{
			if (_selectedNodePresenter != null && _selectedNodePresenter.Id != nodePresenter.Id)
			{
				var selectedConnectionPointPresenter = _selectedPointPresenter;

				var connectionBetweenNodes = new ConnectionPresenter(new ConnectionView(), new ConnectionModel())
				{
					ConnectionPointFrom = selectedConnectionPointPresenter,
					ConnectionPointTo = connectionPointPresenter
				};

				_selectedNodePresenter.AddNextNode(nodePresenter);

				_connectionPresenters.Add(connectionBetweenNodes);

				_selectedNodePresenter = null;

				return;
			}

			_selectedNodePresenter = nodePresenter;
			_selectedPointPresenter = connectionPointPresenter;
		}

		private void NodePresenterOnConnectionPointUnSelected(NodePresenter nodePresenter, ConnectionPointPresenter connectionPointPresenter)
		{
			_selectedNodePresenter = null;
			_selectedPointPresenter = null;
		}
	}
}
