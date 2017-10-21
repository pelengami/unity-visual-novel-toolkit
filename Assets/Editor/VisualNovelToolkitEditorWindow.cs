using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	internal sealed class VisualNovelToolkitEditorWindow : EditorWindow
	{
		private static EditorWindow _window;

		private Vector2 _offset;
		private Vector2 _drag;

		[MenuItem("VisualNovelToolkit/Open")]
		public static void ShowWindow()
		{
			_window = GetWindow<VisualNovelToolkitEditorWindow>(true, LocalizationStrings.WindowTitle, true);
		}

		// ReSharper disable once InconsistentNaming
		private void OnGUI()
		{
			DrawGrid(gridSpacing: 20, gridOpacity: 0.2f, gridColor: Color.black);
			DrawGrid(gridSpacing: 100, gridOpacity: 0.4f, gridColor: Color.black);

			DrawNodes();

			ProcessEvents(Event.current);

			if (GUI.changed) Repaint();
		}

		private void DrawNodes()
		{
		}

		private void ProcessEvents(Event e)
		{
		}

		private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
		{
			int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
			int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

			Handles.BeginGUI();
			Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

			_offset += _drag * 0.5f;
			var newOffset = new Vector3(_offset.x % gridSpacing, _offset.y % gridSpacing, 0);

			for (int i = 0; i < widthDivs; i++)
				Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);

			for (int j = 0; j < heightDivs; j++)
				Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);

			Handles.color = Color.black;
			Handles.EndGUI();
		}
	}
}
