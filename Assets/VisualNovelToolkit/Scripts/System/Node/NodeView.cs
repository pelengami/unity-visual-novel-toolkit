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
		protected float Width = 90;
		protected float Height = 130;
		protected GUIStyle DefaultNodeStyle;
		protected GUIStyle SelectedNodeStyle;

		private const float ButtonArrowWidth = 28;
		private const float ButtonArrowHeight = 28;

		private readonly GUIStyle _labelStyle;
		private readonly GUIStyle _arrowButtonStyle;
		private string _title;
		private bool _isExpanded;
		private bool _isSelected;
		private bool _isDragged;

		protected NodeView(string title, Vector2 position)
		{
			_title = title;
			BRect = new Rect(position.x, position.y, Width, Height);
			
			_arrowButtonStyle = StylesCollection.GetStyle(VntStyles.ButtonArrow);
			_arrowButtonStyle.margin = new RectOffset(60, 0, 0, 0);
		}

		public event Action<Vector2> MouseClicked;
		public event Action Selected;

		public Rect Rect { get { return BRect; } }
		public bool IsExpanded { get { return _isExpanded; } }

		public virtual void Draw()
		{
			//todo change
			BRect.width = Width;
			BRect.height = Height;
			
			var style = _isSelected ? SelectedNodeStyle : DefaultNodeStyle;
			style.fixedWidth = Width;
			style.fixedHeight = Height;

			var screenRect = BRect;
			screenRect.height += 3;
			GUILayout.BeginArea(screenRect);
			GUILayout.BeginVertical();

//			_title = GUILayout.TextField(_title, GUILayout.MaxWidth(screenRect.width));
			GUILayout.Box("", style);

//			GUILayout.Space(-25);
//			if (GUILayout.Button("", _arrowButtonStyle, GUILayout.Width(ButtonArrowWidth), GUILayout.Height(ButtonArrowHeight)))
//				_isExpanded = !_isExpanded;

			GUILayout.EndVertical();
			GUILayout.EndArea();
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
							
							if (Selected!=null)
								Selected.Invoke();
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
