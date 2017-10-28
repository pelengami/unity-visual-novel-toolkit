using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Node
{
    class Node
    {
        private bool _isExpanded;

        public bool IsSelected;
        public Rect Rect;
        public string Title;
        public GUIStyle Style;
        public GUIStyle DefaultNodeStyle;
        public GUIStyle SelectedNodeStyle;
        public bool IsDragged;

        public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedNodeStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint)
        {
            Rect = new Rect(position.x, position.y, width, height);
            Style = nodeStyle;
            DefaultNodeStyle = nodeStyle;
            SelectedNodeStyle = selectedNodeStyle;

            InPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, onClickInPoint);
            OutPoint = new ConnectionPoint(this, ConnectionPointType.Out, inPointStyle, onClickOutPoint);
        }

        public ConnectionPoint InPoint { get; }
        public ConnectionPoint OutPoint { get; }

        public void Drag(Vector2 delta)
        {
            Rect.position += delta;
        }

        public void Draw()
        {
            InPoint.Draw();
            OutPoint.Draw();
            GUI.Box(Rect, Title, Style);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (Rect.Contains(e.mousePosition))
                        {
                            IsDragged = true;
                            IsSelected = true;
                            Style = SelectedNodeStyle;
                            GUI.changed = true;
                        }
                        else
                        {
                            IsSelected = false;
                            Style = DefaultNodeStyle;
                            GUI.changed = true;
                        }
                    }

                    if (e.button == 1 && IsSelected && Rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }

                    break;

                case EventType.MouseUp:
                    IsDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && IsDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void ProcessContextMenu()
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, delegate { });
            genericMenu.ShowAsContext();
        }
    }
}
