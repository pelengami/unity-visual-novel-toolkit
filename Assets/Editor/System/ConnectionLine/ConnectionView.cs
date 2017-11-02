using System;
using Assets.Editor.System.ConnectionPoint;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.System.ConnectionLine
{
	sealed class ConnectionView
	{
		public ConnectionPointView InPointView;
		public ConnectionPointView OutPointView;
		public Action<ConnectionView> OnClickRemoveConnection;

		public void Draw(Rect rect, Vector2 mousePosition)
		{
			Handles.DrawBezier(
				rect.center,
				mousePosition,
				rect.center + Vector2.left * 50f,
				mousePosition - Vector2.left * 50f,
				Color.white,
				null,
				2f
			);

			GUI.changed = true;
		}
	}
}
