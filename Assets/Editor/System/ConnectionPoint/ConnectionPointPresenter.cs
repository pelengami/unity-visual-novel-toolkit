using Assets.Editor.System.ConnectionLine;
using UnityEngine;

namespace Assets.Editor.System.ConnectionPoint
{
	sealed class ConnectionPointPresenter
	{
		private bool _isSelected;
		private readonly ConnectionPointView _connectionPointView;
		private readonly ConnectionPresenter _connectionPresenter;

		public ConnectionPointPresenter(ConnectionPointView connectionPointView)
		{
			_connectionPointView = connectionPointView;

			_connectionPresenter = new ConnectionPresenter(new ConnectionView(), new ConnectionModel());

			_connectionPointView.Clicked += OnClicked;
		}

		public bool IsSelected { get { return _isSelected; } }

		public void Draw()
		{
			_connectionPointView.Draw();

			//draw connection to mouse position
			if (_isSelected)
				_connectionPresenter.Draw(_connectionPointView.Rect);
		}

		public void ProcessEvents(Event e)
		{
			_connectionPresenter.ProcessEvents(e);

			switch (e.type)
			{
				case EventType.mouseUp:
					_isSelected = false;
					break;
			}
		}

		private void OnClicked()
		{
			_isSelected = true;
		}
	}
}
