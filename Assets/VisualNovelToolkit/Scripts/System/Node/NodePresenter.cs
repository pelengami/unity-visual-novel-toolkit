using System;
using System.Collections.Generic;
using Assets.VisualNovelToolkit.Scripts.System.Port;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ParametersPanel;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Node
{
    internal abstract class NodePresenter
    {
        private readonly INodeView _nodeView;
        private readonly List<NodePresenter> _nextNodes = new List<NodePresenter>();

        protected NodePresenter(INodeView nodeView, NodeData nodeData,
            PortPresenter portPresenter, PortPresenter portOutPresenter)
        {
            _nodeView = nodeView;
            NodeData = nodeData;
            Port = portPresenter;
            PortOut = portOutPresenter;

            _nodeView.MouseClicked += OnMouseClicked;
            _nodeView.Selected += OnSelected;

            Port.Selected += ConnectionPointInPresenterOnSelected;
            PortOut.Selected += ConnectionPointOutPresenterOnSelected;
            Port.UnSelected += ConnectionPointInPresenterOnUnSelected;
            PortOut.UnSelected += ConnectionPointOutPresenterOnUnSelected;

            Id = Guid.NewGuid();
        }

        public event Action<NodePresenter, PortPresenter> ConnectionPointSelected;
        public event Action<NodePresenter, PortPresenter> ConnectionPointUnSelected;
        public event Action<NodePresenter> Selected;

        public PortPresenter Port { get; }
        public PortPresenter PortOut { get; }
        public PortPresenter SelectedPortPresenter { get; private set; }
        public NodeData NodeData { get; }
        public Guid Id { get; }

        public void AddNextNode(NodePresenter nodePresenter)
        {
            _nextNodes.Add(nodePresenter);
        }

        protected void DrawParameters(NodeParametersPanel nodeParametersPanel)
        {
            _nodeView.DrawParameters(nodeParametersPanel);
        }

        #region GUI

        public virtual void Draw()
        {
            _nodeView.Draw();

            Port.Draw(_nodeView.Rect);
            PortOut.Draw(_nodeView.Rect);

            UpdateNodeData();
        }
        
        public void Drag(Vector2 mousePosition)
        {
            _nodeView.Drag(mousePosition);
        }

        public void ProcessEvents(Event e)
        {
            _nodeView.ProcessEvents(e);
            Port.ProcessEvents(e);
            PortOut.ProcessEvents(e);
        }

        private void UpdateNodeData()
        {
            var rect = _nodeView.Rect;
            NodeData.X = rect.x;
            NodeData.Y = rect.y;
        }

        private void OnMouseClicked(Vector2 vector2)
        {
            //todo create context menu
        }

        private void OnSelected()
        {
            Selected?.Invoke(this);
        }

        private void ConnectionPointInPresenterOnSelected()
        {
            SelectedPortPresenter = Port;
            ConnectionPointSelected?.Invoke(this, Port);
        }

        private void ConnectionPointOutPresenterOnSelected()
        {
            SelectedPortPresenter = PortOut;
            ConnectionPointSelected?.Invoke(this, PortOut);
        }

        private void ConnectionPointInPresenterOnUnSelected()
        {
            SelectedPortPresenter = null;
            ConnectionPointUnSelected?.Invoke(this, Port);
        }

        private void ConnectionPointOutPresenterOnUnSelected()
        {
            SelectedPortPresenter = null;
            ConnectionPointUnSelected?.Invoke(this, PortOut);
        }

        #endregion
    }
}