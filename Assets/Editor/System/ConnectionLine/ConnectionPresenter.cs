using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node;
using UnityEngine;

namespace Assets.Editor.System.ConnectionLine
{
	sealed class ConnectionPresenter
	{
		private readonly ConnectionView _connectionView;
		private readonly ConnectionModel _connectionModel;
		private Vector2 _mousePosition;

		public ConnectionPresenter(ConnectionView connectionView, ConnectionModel connectionModel)
		{
			_connectionView = connectionView;
			_connectionModel = connectionModel;
		}

		public ConnectionPointPresenter ConnectionPointFrom { get; set; }
		public ConnectionPointPresenter ConnectionPointTo { get; set; }

		public void Draw()
		{
			if (ConnectionPointFrom != null && ConnectionPointTo != null)
				_connectionView.Draw(ConnectionPointFrom.Rect, ConnectionPointTo.Rect);
		}

		public void Draw(Rect rect)
		{
			_connectionView.Draw(rect, _mousePosition);
		}

		public void ProcessEvents(Event e)
		{
			_mousePosition = e.mousePosition;
		}
	}
}
