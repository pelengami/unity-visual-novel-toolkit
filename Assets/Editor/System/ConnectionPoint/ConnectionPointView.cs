using System;
using Assets.Editor.System.Node;
using UnityEngine;

namespace Assets.Editor.System.ConnectionPoint
{
	sealed class ConnectionPointView
	{
		private readonly INodeView _nodeView;
		private readonly GUIStyle _style;
		private readonly ConnectionPointType _type;
		private Rect _rect;

		public ConnectionPointView(INodeView nodeView, GUIStyle guiStyle, ConnectionPointType type)
		{
			_rect = new Rect(0, 0, 25f, 25f);
			_nodeView = nodeView;
			_style = guiStyle;
			_type = type;
		}

		public Action Clicked;

		public Rect Rect { get { return _rect; } }

		public void Draw()
		{
			_rect.y = _nodeView.Rect.y + _nodeView.Rect.height * 0.5f - _rect.height * 0.5f;

			switch (_type)
			{
				case ConnectionPointType.In:
					_rect.x = _nodeView.Rect.x - _rect.width + 8f;
					break;

				case ConnectionPointType.Out:
					_rect.x = _nodeView.Rect.x + _nodeView.Rect.width - 8f;
					break;
			}

			if (!GUI.Button(_rect, "", _style))
				return;

			if (Clicked != null)
				Clicked();
		}
	}
}
