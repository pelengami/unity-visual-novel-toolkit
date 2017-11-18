using System;
using System.Collections.Generic;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ContextMenu;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ParametersPanel;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Node
{
	internal interface INodeView
	{
		event Action<Vector2> MouseClicked;
		event Action Selected;

		Rect Rect { get; }

		void Draw();
		void DrawParameters(NodeParametersPanel nodeParametersPanel);
		void Drag(Vector2 delta);
		bool ProcessEvents(Event e);

		void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems);
	}
}