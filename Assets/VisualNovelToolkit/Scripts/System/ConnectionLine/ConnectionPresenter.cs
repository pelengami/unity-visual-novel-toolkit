using System;
using Assets.Editor.System.ConnectionPoint;
using UnityEngine;

namespace Assets.Editor.System.ConnectionLine
{
	sealed class ConnectionPresenter
	{
		private readonly ConnectionView _connectionView;
		private readonly ConnectionData _connectionData;
		private Vector2 _mousePosition;

		public ConnectionPresenter(ConnectionView connectionView, ConnectionData connectionData)
		{
			_connectionView = connectionView;
			_connectionData = connectionData;
		}

		public ConnectionPointPresenter ConnectionPointFrom { get; private set; }
		public ConnectionPointPresenter ConnectionPointTo { get; private set; }

		public ConnectionData ConnectionData { get { return _connectionData; } }

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

		public void SetFrom(ConnectionPointPresenter connectionPointPresenter, Guid nodeId)
		{
			ConnectionPointFrom = connectionPointPresenter;
			_connectionData.From = nodeId;
		}

		public void SetTo(ConnectionPointPresenter connectionPointPresenter, Guid nodeId)
		{
			ConnectionPointTo = connectionPointPresenter;
			_connectionData.To = nodeId;
		}
	}
}
