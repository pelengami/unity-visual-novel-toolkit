using System;
using Assets.Editor.ToolkitGui.Styles;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls.ParametersPanel
{
	sealed class NodeParameter
	{
		private const float LabelWidth = 80;
		private const float LabelHeight = 20;

		private readonly string _text;
		private readonly GUIStyle _labelStyle;
		private readonly GUIStyle _buttonStyle;

		public NodeParameter(string text)
		{
			_text = text;
			_labelStyle = StylesCollection.GetStyle(VntStyles.Label);
			_buttonStyle = StylesCollection.GetStyle(VntStyles.Button);
		}

		public event Action Clicked;

		public bool IsClickable { get; set; }

		public void Draw(Rect rect)
		{
			var labelRect = new Rect(rect)
			{
				width = LabelWidth,
				height = LabelHeight,
			};

			labelRect.y += 20;
			labelRect.x += 7;

			GUI.Label(labelRect, _text, _labelStyle);

			if (!IsClickable)
				return;

			var buttonRect = new Rect(labelRect);
			buttonRect.x += labelRect.width + 15;
			buttonRect.width = 15;
			buttonRect.height = 15;

			if (!GUI.Button(buttonRect, "", _buttonStyle))
				return;

			if (Clicked != null)
				Clicked.Invoke();
		}
	}
}