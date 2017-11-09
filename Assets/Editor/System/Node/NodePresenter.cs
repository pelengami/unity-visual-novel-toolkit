using System;
using System.Collections.Generic;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	abstract class NodePresenter
	{
		private readonly INodeView _nodeView;
		private readonly NodeData _nodeData;
		private readonly ConnectionPointPresenter _connectionPointInPresenter;
		private readonly ConnectionPointPresenter _connectionPointOutPresenter;
		private readonly List<NodePresenter> _nextNodes = new List<NodePresenter>();

		private ConnectionPointPresenter _selectedConnectionPointPresenter;

		protected NodePresenter(INodeView nodeView, NodeData nodeData, ConnectionPointPresenter connectionPointInPresenter, ConnectionPointPresenter connectionPointOutPresenter)
		{
			_nodeView = nodeView;
			_nodeData = nodeData;
			_connectionPointInPresenter = connectionPointInPresenter;
			_connectionPointOutPresenter = connectionPointOutPresenter;

			_nodeView.MouseClicked += OnMouseClicked;

			_connectionPointInPresenter.Selected += ConnectionPointInPresenterOnSelected;
			_connectionPointOutPresenter.Selected += ConnectionPointOutPresenterOnSelected;
			_connectionPointInPresenter.UnSelected += ConnectionPointInPresenterOnUnSelected;
			_connectionPointOutPresenter.UnSelected += ConnectionPointOutPresenterOnUnSelected;

			Id = Guid.NewGuid();
		}

		public event Action<NodePresenter, ConnectionPointPresenter> ConnectionPointSelected;
		public event Action<NodePresenter, ConnectionPointPresenter> ConnectionPointUnSelected;

		public ConnectionPointPresenter ConnectionPointIn { get { return _connectionPointInPresenter; } }
		public ConnectionPointPresenter ConnectionPointOut { get { return _connectionPointOutPresenter; } }
		public ConnectionPointPresenter SelectedConnectionPointPresenter { get { return _selectedConnectionPointPresenter; } }
		public NodeData NodeData { get { return _nodeData; } }
		public Guid Id { get; private set; }

		public virtual void Draw()
		{
			_nodeView.Draw();

			_connectionPointInPresenter.Draw(_nodeView.Rect);
			_connectionPointOutPresenter.Draw(_nodeView.Rect);

			UpdateNodeData();
		}

		public void DrawParameters(NodeParametersPanel nodeParametersPanel)
		{
			_nodeView.DrawParameters(nodeParametersPanel);
		}

		public void AddNextNode(NodePresenter nodePresenter)
		{
			_nextNodes.Add(nodePresenter);
		}

		public void Drag(Vector2 mousePosition)
		{
			_nodeView.Drag(mousePosition);
		}

		public void ProcessEvents(Event e)
		{
			_nodeView.ProcessEvents(e);
			_connectionPointInPresenter.ProcessEvents(e);
			_connectionPointOutPresenter.ProcessEvents(e);
		}

		private void UpdateNodeData()
		{
			var rect = _nodeView.Rect;
			_nodeData.X = rect.x;
			_nodeData.Y = rect.y;
		}

		private void OnMouseClicked(Vector2 vector2)
		{
			//todo create context menu
		}

		private void ConnectionPointInPresenterOnSelected()
		{
			_selectedConnectionPointPresenter = _connectionPointInPresenter;

			if (ConnectionPointSelected != null)
				ConnectionPointSelected.Invoke(this, _connectionPointInPresenter);
		}

		private void ConnectionPointOutPresenterOnSelected()
		{
			_selectedConnectionPointPresenter = _connectionPointOutPresenter;

			if (ConnectionPointSelected != null)
				ConnectionPointSelected.Invoke(this, _connectionPointOutPresenter);
		}

		private void ConnectionPointInPresenterOnUnSelected()
		{
			_selectedConnectionPointPresenter = null;

			if (ConnectionPointUnSelected != null)
				ConnectionPointUnSelected.Invoke(this, _connectionPointInPresenter);
		}

		private void ConnectionPointOutPresenterOnUnSelected()
		{
			_selectedConnectionPointPresenter = null;

			if (ConnectionPointUnSelected != null)
				ConnectionPointUnSelected.Invoke(this, _connectionPointOutPresenter);
		}
	}
}
