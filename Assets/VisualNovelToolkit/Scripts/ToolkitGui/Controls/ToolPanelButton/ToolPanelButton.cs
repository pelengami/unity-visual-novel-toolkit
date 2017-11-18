using System;
using UnityEditor;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ToolPanelButton
{
	sealed class ToolPanelButton
	{
		private readonly string _text;

		public ToolPanelButton(string text)
		{
			_text = text;
		}

		public event Action Clicked;

		public void Draw()
		{
			if (!GUILayout.Button(_text, EditorStyles.toolbarButton))
				return;
			Clicked?.Invoke();
		}
	}
}
