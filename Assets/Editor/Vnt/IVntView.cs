using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using Assets.Editor.ToolkitGui.Controls.ToolPanelButton;
using UnityEngine;

namespace Assets.Editor.Vnt
{
	interface IVntView : IWindow
	{
		event Action<Vector2> MouseClicked;
		event Action<Vector2> Drag;
		event Action Awaked;
		event Action OnGui;
		event Action<Event> ProcessedEvents;

		void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems);
		void DrawToolPanel(List<ToolPanelButton> buttons);
	}
}
