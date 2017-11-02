using Assets.Editor.System.ConnectionPoint;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	sealed class NodePresenter
	{
		private readonly INodeView _nodeView;
		private readonly ConnectionPointPresenter _connectionPointInPresenter;
		private readonly ConnectionPointPresenter _connectionPointOutPresenter;

		public NodePresenter(INodeView nodeView, ConnectionPointPresenter connectionPointInPresenter, ConnectionPointPresenter connectionPointOutPresenter)
		{
			_nodeView = nodeView;
			_connectionPointInPresenter = connectionPointInPresenter;
			_connectionPointOutPresenter = connectionPointOutPresenter;

			_nodeView.MouseClicked += OnMouseClicked;
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
	}
}
