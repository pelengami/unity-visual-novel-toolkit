using System;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Port
{
	sealed class PortView
	{
		private const float Width = 25f;
		private const float Height = 25f;

		private readonly GUIStyle _style;
		private readonly PortType _type;
		private Rect _rect;

		public PortView(PortType type)
		{
			_rect = new Rect(0, 0, Width, Height);
			_type = type;

			switch (type)
			{
				case PortType.In:
					_style = StylesCollection.GetStyle(VntStyles.PortIn);
					break;
				case PortType.Out:
					_style = StylesCollection.GetStyle(VntStyles.PortOut);
					break;
			}
		}

		public Action Clicked;

		public Rect Rect => _rect;

		public void Draw(Rect rect)
		{
			_rect.y = rect.y + rect.height * 0.5f - _rect.height * 0.5f;

			switch (_type)
			{
				case PortType.In:
					_rect.x = rect.x - _rect.width + 8f;
					break;

				case PortType.Out:
					_rect.x = rect.x + rect.width - 8f;
					break;
			}

			if (!GUI.Button(_rect, "", _style))
				return;

			Clicked?.Invoke();
		}
	}
}
