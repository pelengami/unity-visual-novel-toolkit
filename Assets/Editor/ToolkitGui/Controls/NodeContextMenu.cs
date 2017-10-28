using System;
using Assets.Editor.Localization;
using Assets.Editor.ToolkitGui.Style;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls
{
	sealed class NodeContextMenu
	{
		public NodeContextMenu(Vector2 mousePosition)
		{
			var genericMenu = new GenericMenu();

			var guiContent = new GUIContent(LocalizationStrings.LoadSceneNodeTitle);
			genericMenu.AddItem(guiContent, @on: false, func: () => OnClickAddNode(StyleNames.LoadSceneNode, mousePosition));

			guiContent = new GUIContent(LocalizationStrings.ChangeBackgroundNoteTitle);
			genericMenu.AddItem(guiContent, @on: false, func: () => OnClickAddNode(StyleNames.ChangeBackgroundNode, mousePosition));

			genericMenu.ShowAsContext();
		}

		public Action<string> Clicked;

		private void OnClickAddNode(string nodeName, object mousePosition)
		{
			// ReSharper disable once UseNullPropagation
			if (Clicked != null)
				Clicked.Invoke(nodeName);
		}
	}
}
