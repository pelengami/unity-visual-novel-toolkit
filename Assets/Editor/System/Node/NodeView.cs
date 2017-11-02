using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	sealed class NodeView : INodeView
	{
		private readonly string _title;
		private readonly GUIStyle _defaultNodeStyle;
		private readonly GUIStyle _selectedNodeStyle;

		private bool _isExpanded;
		private bool _isSelected;
		private bool _isDragged;
		private Rect _rect;

		public NodeView(GUIStyle defaultStyleName, GUIStyle selectedStyleName, string title, Vector2 position)
		{
			_defaultNodeStyle = defaultStyleName;
			_selectedNodeStyle = selectedStyleName;
			_title = title;

			_rect = new Rect(position.x, position.y, 100, 100);
		}

		public event Action<Vector2> MouseClicked;

		public Rect Rect { get { return _rect; } }

		public void Drag(Vector2 delta)
		{
			_rect.position += delta;
		}

		public void ShowContextMenu(Vector2 mousePosition, List<ContextMenuItem> contextMenuItems)
		{
			var genericMenu = ContextMenuBuilder.Build(contextMenuItems, mousePosition);
			genericMenu.ShowAsContext();
		}

		public void Draw()
		{
			var style = _isSelected ? _selectedNodeStyle : _defaultNodeStyle;
			GUI.Box(_rect, _title, style);
		}

		public bool ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 0)
					{
						if (_rect.Contains(e.mousePosition))
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

					if (e.button == 1 && _isSelected && _rect.Contains(e.mousePosition))
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
