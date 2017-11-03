using UnityEditor;
using UnityEngine;

namespace Assets.Editor.System.ConnectionLine
{
	sealed class ConnectionView
	{
		public void Draw(Rect fromRect, Rect toRect)
		{
			Handles.DrawBezier(
				fromRect.center,
				toRect.center,
				fromRect.center + Vector2.left * 50f,
				toRect.center - Vector2.left * 50f,
				Color.white,
				null,
				2f
			);
		}

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
