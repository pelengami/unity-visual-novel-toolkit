using System;
using System.Collections.Generic;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ContextMenu;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ToolPanelButton;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.Vnt
{
	internal interface IVntView : IWindow
	{
		event Action<Vector2> MouseClicked;
		event Action<Vector2> Drag;
		event Action Awaked;
		event Action OnGui;
		event Action<Event> ProcessedEvents;

		void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems);
		void DrawToolPanel(IEnumerable<ToolPanelButton> buttons);
	}
}
