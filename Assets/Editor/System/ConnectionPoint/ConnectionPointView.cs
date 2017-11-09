using System;
using Assets.Editor.System.Node;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.ConnectionPoint
{
	sealed class ConnectionPointView
	{
		private const float Width = 25f;
		private const float Height = 25f;

		private readonly GUIStyle _style;
		private readonly ConnectionPointType _type;
		private Rect _rect;

		public ConnectionPointView(ConnectionPointType type)
		{
			_rect = new Rect(0, 0, Width, Height);
			_type = type;

			switch (type)
			{
				case ConnectionPointType.In:
					_style = StylesCollection.GetStyle(VntStyles.ConnectionIn);
					break;
				case ConnectionPointType.Out:
					_style = StylesCollection.GetStyle(VntStyles.ConnectionOut);
					break;
			}
		}

		public Action Clicked;

		public Rect Rect { get { return _rect; } }

		public void Draw(Rect rect)
		{
			_rect.y = rect.y + rect.height * 0.5f - _rect.height * 0.5f;

			switch (_type)
			{
				case ConnectionPointType.In:
					_rect.x = rect.x - _rect.width + 8f;
					break;

				case ConnectionPointType.Out:
					_rect.x = rect.x + rect.width - 8f;
					break;
			}

			if (!GUI.Button(_rect, "", _style))
				return;

			if (Clicked != null)
				Clicked();
		}
	}
}
