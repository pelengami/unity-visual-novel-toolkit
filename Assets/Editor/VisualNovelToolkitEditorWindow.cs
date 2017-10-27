using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.ToolkitGui.Node;
using Assets.Editor.ToolkitGui.Primitives;
using Assets.Editor.ToolkitGui.Style;
using Assets.Editor.ToolkitGui.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	sealed class VisualNovelToolkitEditorWindow : EditorWindow
	{
		private static EditorWindow _window;
		private static PainterUtils _painterUtils;
		private readonly GuiStylesDictionary _guiStylesDictionary = new GuiStylesDictionary();

		private readonly List<Node> _nodes = new List<Node>();

		[MenuItem("VisualNovelToolkit/Open")]
		public static void ShowWindow()
		{
			_window = GetWindow<VisualNovelToolkitEditorWindow>(true, LocalizationStrings.WindowTitle, true);
			_painterUtils = new PainterUtils(_window);
		}

		private void OnEnable()
		{
			_guiStylesDictionary.LoadStyles();
		}

		// ReSharper disable once InconsistentNaming
		private void OnGUI()
		{
			_painterUtils.DrawGrid();

			DrawNodes();

			ProcessNodeEvents(Event.current);
			ProcessEvents(Event.current);

			if (GUI.changed)
				Repaint();
		}  

		private void DrawNodes()
		{
			foreach (var node in _nodes)
				node.Draw();
		}

		private void ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 1)
						ProcessContextMenu(e.mousePosition);
					break;
			}
		}

		private void ProcessNodeEvents(Event e)
		{
			foreach (var node in _nodes)
			{
				bool guiChanged = node.ProcessEvents(e);
				if (guiChanged)
					GUI.changed = true;
			}
		}

		private void ProcessContextMenu(Vector2 mousePosition)
		{
			var contextMenu = new NodeContextMenu(mousePosition);

			contextMenu.Clicked += delegate
			{
				var style = _guiStylesDictionary.GetStyle(NodeStyleNames.LoadScene);
				_nodes.Add(new Node(mousePosition, 50, 50, style));
			};
		}
	}
}
