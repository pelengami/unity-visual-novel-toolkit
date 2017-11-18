using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ContextMenu
{
	static class ContextMenuBuilder
	{
		public static GenericMenu Build(List<ContextMenuItem> contextMenuItems, Vector2 mousePosition)
		{
			var genericMenu = new GenericMenu();

			foreach (var contextMenuItem in contextMenuItems)
			{
				var guiContent = new GUIContent(contextMenuItem.Title);
				genericMenu.AddItem(guiContent, @on: false, func: () => contextMenuItem.RaiseClicked(mousePosition));
			}

			return genericMenu;
		}
	}
}
