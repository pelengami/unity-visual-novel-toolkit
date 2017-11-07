using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using Assets.Editor.ToolkitGui.Controls.ParametersPanel;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	abstract class NodeView : INodeView
	{
		protected Rect BRect;
		protected GUIStyle DefaultNodeStyle;
		protected GUIStyle SelectedNodeStyle;

		private const float PaddingNameTop = 18;
		private const float Width = 70;
		private const float Height = 70;
		private const float ButtonArrowWidth = 35;
		private const float ButtonArrowHeight = 35;

		private readonly string _title;
		private readonly GUIStyle _labelStyle;
		private GUIStyle _buttonStyle;
		private readonly GUIStyle _arrowButtonStyle;
		private bool _isExpanded;
		private bool _isSelected;
		private bool _isDragged;

		protected NodeView(string title, Vector2 position)
		{
			_title = title;
			BRect = new Rect(position.x, position.y, Width, Height);

			_labelStyle = StylesCollection.GetStyle(VntStyles.Label);
			_buttonStyle = StylesCollection.GetStyle(VntStyles.Button);
			_arrowButtonStyle = StylesCollection.GetStyle(VntStyles.ButtonArrow);
		}

		public event Action<Vector2> MouseClicked;

		public Rect Rect { get { return BRect; } }
		public bool IsExpanded { get { return _isExpanded; } }

		public virtual void Draw()
		{
			var style = _isSelected ? SelectedNodeStyle : DefaultNodeStyle;
			GUI.Box(BRect, "", style);

			var labelRect = new Rect(BRect.x, BRect.y - PaddingNameTop, 100, 20);
			GUI.Box(labelRect, _title, _labelStyle);

			var buttonArrowRect = new Rect(BRect.x + Width / 2 + 15, BRect.y + 45, ButtonArrowWidth, ButtonArrowHeight);
			if (GUI.Button(buttonArrowRect, "", _arrowButtonStyle))
				_isExpanded = !_isExpanded;
		}

		public void DrawParameters(NodeParametersPanel nodeParametersPanel)
		{
			if (!_isExpanded)
				return;

			nodeParametersPanel.Draw(BRect);
		}

		public void Drag(Vector2 delta)
		{
			BRect.position += delta;
		}

		public void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems)
		{
			var genericMenu = ContextMenuBuilder.Build(contextMenuItems, mousePosition);
			genericMenu.ShowAsContext();
		}

		public bool ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 0)
					{
						if (BRect.Contains(e.mousePosition))
						{
							_isDragged = true;
							_isSelected = true;
							GUI.changed = true;
						}
						else
						{
							_isSelected = false;
							GUI.changed = true;
						}
					}

					if (e.button == 1 && _isSelected && BRect.Contains(e.mousePosition))
					{
						if (MouseClicked != null)
							MouseClicked.Invoke(e.mousePosition);

						e.Use();
					}

					break;

				case EventType.MouseUp:
					_isDragged = false;
					break;

				case EventType.MouseDrag:
					if (e.button == 0 && _isDragged)
					{
						Drag(e.delta);
						e.Use();
						return true;
					}
					break;
			}
			return false;
		}
	}
}
