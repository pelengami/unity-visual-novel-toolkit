using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Node
{
	sealed class Connection
	{
		public ConnectionPoint InPoint;
		public ConnectionPoint OutPoint;
		public Action<Connection> OnClickRemoveConnection;

		public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> onClickRemoveConnection)
		{
			InPoint = inPoint;
			OutPoint = outPoint;
			OnClickRemoveConnection = onClickRemoveConnection;
		}

		public void Draw()
		{
			Handles.DrawBezier(
				InPoint.Rect.center,
				OutPoint.Rect.center,
				InPoint.Rect.center + Vector2.left * 50f,
				OutPoint.Rect.center - Vector2.left * 50f,
				Color.white,
				null,
				2f
			);

			if (!Handles.Button((InPoint.Rect.center + OutPoint.Rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleCap))
				return;

			// ReSharper disable once UseNullPropagation
			if (OnClickRemoveConnection != null)
				OnClickRemoveConnection(this);
		}
	}
}
