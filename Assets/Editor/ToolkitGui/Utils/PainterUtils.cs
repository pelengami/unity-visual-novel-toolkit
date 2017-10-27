using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Utils
{
	sealed class PainterUtils
	{
		private readonly EditorWindow _editorWindow;
		private Vector2 _offset;
		private Vector2 _drag;

		public PainterUtils(EditorWindow editorWindow)
		{
			_editorWindow = editorWindow;
		}

		public void DrawGrid()
		{
			DrawGrid(gridSpacing: 20, gridOpacity: 0.2f, gridColor: Color.black);
			DrawGrid(gridSpacing: 100, gridOpacity: 0.4f, gridColor: Color.black);
		}

		private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
		{
			int widthDivs = Mathf.CeilToInt(_editorWindow.position.width / gridSpacing);
			int heightDivs = Mathf.CeilToInt(_editorWindow.position.height / gridSpacing);

			Handles.BeginGUI();
			Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

			_offset += _drag * 0.5f;
			var newOffset = new Vector3(_offset.x % gridSpacing, _offset.y % gridSpacing, 0);

			for (int i = 0; i < widthDivs; i++)
				Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, _editorWindow.position.height, 0f) + newOffset);

			for (int j = 0; j < heightDivs; j++)
				Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(_editorWindow.position.width, gridSpacing * j, 0f) + newOffset);

			Handles.color = Color.black;
			Handles.EndGUI();
		}
	}
}
