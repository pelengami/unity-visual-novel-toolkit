using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	interface INodeView
	{
		event Action<Vector2> MouseClicked;

		Rect Rect { get; }

		void Draw();
		void Drag(Vector2 delta);
		bool ProcessEvents(Event e);

		void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems);
	}
}