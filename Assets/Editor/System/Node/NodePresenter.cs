using System;
using System.Collections.Generic;
using Assets.Editor.System.ConnectionLine;
using Assets.Editor.System.ConnectionPoint;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	sealed class NodePresenter
	{
		private readonly INodeView _nodeView;
		private readonly ConnectionPointPresenter _connectionPointInPresenter;
		private readonly ConnectionPointPresenter _connectionPointOutPresenter;
		private readonly List<NodePresenter> _nextNodes = new List<NodePresenter>();

		private ConnectionPointPresenter _selectedConnectionPointPresenter;

		public NodePresenter(INodeView nodeView, ConnectionPointPresenter connectionPointInPresenter, ConnectionPointPresenter connectionPointOutPresenter)
		{
			_nodeView = nodeView;
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
		public Guid Id { get; }

		public void AddNextNode(NodePresenter nodePresenter)
		{
			_nextNodes.Add(nodePresenter);
		}

		public void Draw()
		{
			_nodeView.Draw();

			_connectionPointInPresenter.Draw();
			_connectionPointOutPresenter.Draw();
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
