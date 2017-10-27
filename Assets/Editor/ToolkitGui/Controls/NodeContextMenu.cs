using System;
using Assets.Editor.Localization;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls
{
	sealed class NodeContextMenu
	{
		public NodeContextMenu(Vector2 mousePosition)
		{
			var genericMenu = new GenericMenu();
			var guiContent = new GUIContent(LocalizationStrings.AddNodeButton);
			genericMenu.AddItem(guiContent, @on: false, func: () => OnClickAddNode(mousePosition));
			genericMenu.ShowAsContext();
		}

		public Action Clicked;

		private void OnClickAddNode(object mousePosition)
		{
			// ReSharper disable once UseNullPropagation
			if (Clicked != null)
				Clicked.Invoke();
		}
	}
}
