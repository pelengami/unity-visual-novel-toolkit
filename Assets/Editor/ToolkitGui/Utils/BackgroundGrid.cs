using Assets.Editor.Vnt;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Utils
{
	sealed class BackgroundGrid
	{
		private readonly IWindow _window;
		private Vector2 _offset;
		private Vector2 _drag;

		public BackgroundGrid(IWindow window)
		{
			_window = window;
		}

		public void Draw()
		{
			Draw(gridSpacing: 20, gridOpacity: 0.2f, gridColor: Color.black);
			Draw(gridSpacing: 100, gridOpacity: 0.4f, gridColor: Color.black);
		}

		private void Draw(float gridSpacing, float gridOpacity, Color gridColor)
		{
			int widthDivs = Mathf.CeilToInt(_window.Width / gridSpacing);
			int heightDivs = Mathf.CeilToInt(_window.Height / gridSpacing);

			Handles.BeginGUI();
			Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

			_offset += _drag * 0.5f;
			var newOffset = new Vector3(_offset.x % gridSpacing, _offset.y % gridSpacing, 0);

			for (int i = 0; i < widthDivs; i++)
				Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, _window.Height, 0f) + newOffset);

			for (int j = 0; j < heightDivs; j++)
				Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(_window.Width, gridSpacing * j, 0f) + newOffset);

			Handles.color = Color.black;
			Handles.EndGUI();
		}
	}
}
