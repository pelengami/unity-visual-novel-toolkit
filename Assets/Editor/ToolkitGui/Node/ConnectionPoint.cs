using System;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Node
{
	sealed class ConnectionPoint
	{
		public Rect Rect;

		public ConnectionPointType Type;

		public Node Node;

		public GUIStyle Style;

		public Action<ConnectionPoint> OnClickConnectionPoint;

		public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> onClickConnectionPoint)
		{
			Node = node;
			Type = type;
			Style = style;
			OnClickConnectionPoint = onClickConnectionPoint;
			Rect = new Rect(0, 0, 10f, 20f);
		}

		public void Draw()
		{
			Rect.y = Node.Rect.y + (Node.Rect.height * 0.5f) - Rect.height * 0.5f;

			switch (Type)
			{
				case ConnectionPointType.In:
					Rect.x = Node.Rect.x - Rect.width + 8f;
					break;

				case ConnectionPointType.Out:
					Rect.x = Node.Rect.x + Node.Rect.width - 8f;
					break;
			}

			if (!GUI.Button(Rect, "", Style))
				return;

			// ReSharper disable once UseNullPropagation
			if (OnClickConnectionPoint != null)
				OnClickConnectionPoint(this);
		}
	}
}
