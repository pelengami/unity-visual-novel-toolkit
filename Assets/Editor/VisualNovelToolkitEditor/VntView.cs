using System;
using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using Assets.Editor.ToolkitGui.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.VisualNovelToolkitEditor
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

		public float Width { get { return _window.position.width; } }
		public float Height { get { return _window.position.height; } }

		[MenuItem("VisualNovelToolkit/Open")]
		public static void ShowEdiorMenuItem()
		{
			_window = GetWindow<VntView>(true, LocalizationStrings.WindowTitle, true);
		}

		public void Awake()
		{
			_vntPresenter = new VntPresenter(this, new VntModel());
			_backgroundGrid = new BackgroundGrid(this);

			if (Awaked != null)
				Awaked.Invoke();
		}

		public void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems)
		{
			var genericMenu = ContextMenuBuilder.Build(contextMenuItems, mousePosition);
			genericMenu.ShowAsContext();
		}

		// ReSharper disable once InconsistentNaming
		private void OnGUI()
		{
			_backgroundGrid.Draw();

			// ReSharper disable once UseNullPropagation
			if (OnGui != null)
				OnGui.Invoke();

			// ReSharper disable once UseNullPropagation
			if (ProcessedEvents != null)
				ProcessedEvents.Invoke(Event.current);

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
						if (MouseClicked != null)
							MouseClicked.Invoke(e.mousePosition);
					break;

				case EventType.MouseDrag:
					if (e.button == 0)
					{
						if (Drag != null)
							Drag.Invoke(e.delta);
						GUI.changed = true;
					}
					break;
			}
		}
	}
}
