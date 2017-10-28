using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.ToolkitGui.Controls;
using Assets.Editor.ToolkitGui.Node;
using Assets.Editor.ToolkitGui.Style;
using Assets.Editor.ToolkitGui.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    sealed class VisualNovelToolkitEditorWindow : EditorWindow
    {
        private static EditorWindow _window;
        private static PainterUtils _painterUtils;
        private readonly GuiStylesDictionary _guiStylesDictionary = new GuiStylesDictionary();

        private readonly List<Node> _nodes = new List<Node>();
        private readonly List<Connection> _connections = new List<Connection>();

        private ConnectionPoint _selectedInConnectionPoint;
        private ConnectionPoint _selectedOutConnectionPoint;

        private Vector2 _drag;

        [MenuItem("VisualNovelToolkit/Open")]
        public static void ShowWindow()
        {
            _window = GetWindow<VisualNovelToolkitEditorWindow>(true, LocalizationStrings.WindowTitle, true);
            _painterUtils = new PainterUtils(_window);
        }

        private void OnEnable()
        {
            _guiStylesDictionary.LoadStyles();
        }

        // ReSharper disable once InconsistentNaming
        private void OnGUI()
        {
            _painterUtils.DrawGrid();

            DrawNodes();
            DrawConnections();

            DrawConnectionLine(Event.current);

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);

            if (GUI.changed)
                Repaint();
        }

        private void DrawNodes()
        {
            foreach (var node in _nodes)
                node.Draw();
        }

        private void DrawConnections()
        {
            foreach (var connection in _connections)
                connection.Draw();
        }

        private void ProcessEvents(Event e)
        {
            _drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                        ProcessContextMenu(e.mousePosition);
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
                        OnDrag(e.delta);
                    break;
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            foreach (var node in _nodes)
            {
                bool guiChanged = node.ProcessEvents(e);
                if (guiChanged)
                    GUI.changed = true;
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            var contextMenu = new NodeContextMenu(mousePosition);

            contextMenu.Clicked += delegate (string nodeName)
            {
                var nodeStyle = _guiStylesDictionary.GetStyle(nodeName);
                var inConnectionStyle = _guiStylesDictionary.GetStyle(StyleNames.ConnectionIn);
                var outConnectionStyle = _guiStylesDictionary.GetStyle(StyleNames.ConnectionOut);
                _nodes.Add(new Node(mousePosition, 50, 50, nodeStyle, nodeStyle,
                    inConnectionStyle,
                    outConnectionStyle,
                    OnClickInPoint,
                    OnClickOutPoint));
            };
        }

        private void OnClickInPoint(ConnectionPoint inPoint)
        {
            _selectedInConnectionPoint = inPoint;

            if (_selectedOutConnectionPoint == null)
                return;

            if (_selectedOutConnectionPoint.Node != _selectedInConnectionPoint.Node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
                ClearConnectionSelection();
        }

        private void OnClickOutPoint(ConnectionPoint outPoint)
        {
            _selectedOutConnectionPoint = outPoint;

            if (_selectedOutConnectionPoint == null)
                return;

            if (_selectedOutConnectionPoint.Node != _selectedInConnectionPoint.Node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
                ClearConnectionSelection();
        }

        private void OnClickRemoveConnection(Connection connection)
        {
            _connections.Remove(connection);
        }

        private void CreateConnection()
        {
            _connections.Add(new Connection(_selectedInConnectionPoint, _selectedOutConnectionPoint, OnClickRemoveConnection));
        }

        private void OnDrag(Vector2 delta)
        {
            _drag = delta;

            foreach (var node in _nodes)
                node.Drag(delta);

            GUI.changed = true;
        }

        private void DrawConnectionLine(Event e)
        {
            if (_selectedInConnectionPoint != null && _selectedOutConnectionPoint == null)
            {
                Handles.DrawBezier(
                    _selectedInConnectionPoint.Rect.center,
                    e.mousePosition,
                    _selectedInConnectionPoint.Rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (_selectedOutConnectionPoint != null && _selectedInConnectionPoint == null)
            {
                Handles.DrawBezier(
                    _selectedOutConnectionPoint.Rect.center,
                    e.mousePosition,
                    _selectedOutConnectionPoint.Rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

        private void OnClickRemoveNode(Node node)
        {
            var connectionsToRemove = new List<Connection>();

            foreach (var connection in _connections)
                if (connection.InPoint == node.InPoint || connection.OutPoint == node.OutPoint)
                    connectionsToRemove.Add(connection);

            foreach (var connection in connectionsToRemove)
                _connections.Remove(connection);

            _nodes.Remove(node);
        }

        private void ClearConnectionSelection()
        {
            _selectedInConnectionPoint = null;
            _selectedOutConnectionPoint = null;
        }
    }
}
