using System;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ParametersPanel
{
	sealed class NodeParameter
	{
		private readonly string _text;
		private readonly GUIStyle _labelStyle;
		private readonly GUIStyle _buttonStyle;

		public NodeParameter(string text)
		{
			_text = text;
			_labelStyle = StylesCollection.GetStyle(VntStyles.Label);
			_buttonStyle = StylesCollection.GetStyle(VntStyles.Button);
			_labelStyle.margin = new RectOffset(5, 5, 5, 5);
			_labelStyle.fixedHeight = 20;
			_labelStyle.fixedWidth = 20;
			_buttonStyle.margin = new RectOffset(5, 5, 5, 5);
			_buttonStyle.fixedHeight = 20;
			_buttonStyle.fixedWidth = 20;
			Text = "0";
			Width = 20;
			Height = 20;
		}

		public event Action Clicked;

		public bool IsClickable { get; set; }

		public bool IsEditable { get; set; }

		public string Text { get; set; }

		public float Height { get; set; }

		public float Width { get; set; }

		public void Draw(Rect rect)
		{
			GUILayout.BeginHorizontal();

			GUILayout.Label(_text, _labelStyle);

			GUILayout.FlexibleSpace();

			if (IsClickable)
				if (GUILayout.Button("", _buttonStyle))
					Clicked?.Invoke();

			if (IsEditable)
				Text = GUILayout.TextArea(Text, GUILayout.Height(Height), GUILayout.Width(Width));

			GUILayout.EndHorizontal();
		}
	}
}