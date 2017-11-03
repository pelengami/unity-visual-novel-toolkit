using System;
using UnityEngine;

namespace Assets.Editor.System.ConnectionPoint
{
	sealed class ConnectionPointPresenter
	{
		private bool _isSelected;
		private readonly ConnectionPointView _connectionPointView;

		public ConnectionPointPresenter(ConnectionPointView connectionPointView)
		{
			_connectionPointView = connectionPointView;
			_connectionPointView.Clicked += OnClicked;
		}

		public event Action Selected;
		public event Action UnSelected;

		public bool IsSelected { get { return _isSelected; } }
		public Rect Rect { get { return _connectionPointView.Rect; } }

		public void Draw()
		{
			_connectionPointView.Draw();
		}

		public void ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.mouseUp:
					if (_isSelected)
					{
						_isSelected = false;
						if (UnSelected != null)
							UnSelected.Invoke();
					}
					break;
			}
		}

		private void OnClicked()
		{
			_isSelected = true;
			if (Selected != null)
				Selected.Invoke();
		}
	}
}
