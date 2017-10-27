using UnityEngine;

namespace Assets.Editor.ToolkitGui.Node
{
	class Node
	{
		private bool _isExpanded;

		public Rect Rect;
		public string Title;
		public GUIStyle Style;
		public bool IsDragged;

		public Node(Vector2 position, float width, float height, GUIStyle nodeStyle)
		{
			Rect = new Rect(position.x, position.y, width, height);
			Style = nodeStyle;
		}

		public void Drag(Vector2 delta)
		{
			Rect.position += delta;
		}

		public void Draw()
		{
			GUI.Box(Rect, Title, Style);
		}

		public bool ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 0)
					{
						if (Rect.Contains(e.mousePosition))
						{
							IsDragged = true;
							GUI.changed = true;
						}
						else
							GUI.changed = true;
					}
					break;

				case EventType.MouseUp:
					IsDragged = false;
					break;

				case EventType.MouseDrag:
					if (e.button == 0 && IsDragged)
					{
						Drag(e.delta);
						e.Use();
						return true;
					}
					break;
			}
			return false;
		}
	}
}
