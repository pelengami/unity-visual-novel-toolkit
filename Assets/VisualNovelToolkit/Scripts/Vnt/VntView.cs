using System;
using System.Collections.Generic;
using Assets.VisualNovelToolkit.Scripts.Localization;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ContextMenu;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ToolPanelButton;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.Vnt
{
	sealed class VntView : EditorWindow, IVntView
	{
		private static EditorWindow _window;
		private static BackgroundGrid _backgroundGrid;

		private VntPresenter _vntPresenter;

		public event Action<Vector2> MouseClicked;
		public event Action<Vector2> Drag;
		public event Action Awaked;
		public event Action<Event> ProcessedEvents;
		public event Action OnGui;

		public float Width => _window.position.width;
		public float Height => _window.position.height;

		[MenuItem("VisualNovelToolkit/Open")]
		public static void ShowEdiorMenuItem()
		{
			_window = GetWindow<VntView>(false, LocalizationStrings.WindowTitle, true);
		}

		public void Awake()
		{
			_vntPresenter = new VntPresenter(this, new VntData());
			_backgroundGrid = new BackgroundGrid(this);

			Awaked?.Invoke();
		}

		public void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems)
		{
			var genericMenu = ContextMenuBuilder.Build(contextMenuItems, mousePosition);
			genericMenu.ShowAsContext();
		}

		public void DrawToolPanel(IEnumerable<ToolPanelButton> toolPanelButtons)
		{
			GUILayout.BeginHorizontal(EditorStyles.toolbar);
			foreach (var toolPanelButton in toolPanelButtons)
				toolPanelButton.Draw();
			GUILayout.EndHorizontal();
		}

		private void OnGUI()
		{
			_backgroundGrid.Draw();

			OnGui?.Invoke();
			ProcessedEvents?.Invoke(Event.current);

			ProcessEvents(Event.current);

			if (GUI.changed)
				Repaint();
		}
		
		private void ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 1)
						MouseClicked?.Invoke(e.mousePosition);
					break;

				case EventType.MouseDrag:
					if (e.button == 0)
					{
						Drag?.Invoke(e.delta);
						GUI.changed = true;
					}
					break;
			}
		}
	}
}
