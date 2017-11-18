using System.Collections.Generic;
using Assets.VisualNovelToolkit.Scripts.Localization;
using Assets.VisualNovelToolkit.Scripts.Serialization;
using Assets.VisualNovelToolkit.Scripts.System;
using Assets.VisualNovelToolkit.Scripts.System.Link;
using Assets.VisualNovelToolkit.Scripts.System.Node;
using Assets.VisualNovelToolkit.Scripts.System.Node.Dialogue;
using Assets.VisualNovelToolkit.Scripts.System.Port;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ContextMenu;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.Dialog;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ToolPanelButton;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Styles;
using UnityEditor;
using UnityEngine;
using VisualNovelToolkit.Scripts.System.Filters;

namespace Assets.VisualNovelToolkit.Scripts.Vnt
{
    internal sealed class VntPresenter
    {
        private readonly IVntView _vntView;
        private readonly List<NodePresenter> _nodePresenters = new List<NodePresenter>();
        private readonly List<LinkPresenter> _connectionPresenters = new List<LinkPresenter>();
        private readonly LinkPresenter _linkToMousePresenter;
        private readonly VntData _vntData;

        private NodePresenter _selectedNodePresenter;
        private PortPresenter _selectedPointPresenter;

        public VntPresenter(IVntView vntView, VntData vntData)
        {
            _vntView = vntView;
            _vntData = vntData;
            _vntView.MouseClicked += VntViewOnMouseClicked;
            _vntView.Awaked += VntViewOnAwaked;
            _vntView.OnGui += VntViewOnGui;
            _vntView.Drag += VntViewOnDrag;
            _vntView.ProcessedEvents += VntViewOnProcessedEvents;

            _linkToMousePresenter = new LinkPresenter(new LinkView(), new LinkData());
        }

        private void VntViewOnAwaked()
        {
            StylesCollection.LoadStyles();
            _vntData.LoadNodes();
        }

        #region GUI Events

        private void VntViewOnGui()
        {
            DrawToolPanel();

            foreach (var nodePresenter in _nodePresenters)
                nodePresenter.Draw();

            foreach (var connectionPresenter in _connectionPresenters)
                connectionPresenter.Draw();

            if (_selectedNodePresenter != null && _selectedNodePresenter.SelectedPortPresenter != null)
                _linkToMousePresenter.Draw(_selectedNodePresenter.SelectedPortPresenter.Rect);
        }

        private void OnNodeSelected(NodePresenter nodePresenter)
        {
            Selection.activeObject = nodePresenter.NodeData;
        }

        private void DrawToolPanel()
        {
            var createNewButton = new ToolPanelButton("Create");
            var loadButton = new ToolPanelButton("Load");
            var saveButton = new ToolPanelButton("Save");

            createNewButton.Clicked += () =>
            {
                foreach (var nodePresenter in _nodePresenters)
                {
                    nodePresenter.ConnectionPointSelected -= OnConnectionPointSelected;
                    nodePresenter.ConnectionPointUnSelected -= OnConnectionPointUnSelected;
                }

                _nodePresenters.Clear();
                _connectionPresenters.Clear();
                _selectedNodePresenter = null;
                _selectedPointPresenter = null;
            };

            loadButton.Clicked += () => { };

            saveButton.Clicked += () =>
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.ShowDialog();

                if (saveFileDialog.Result)
                {
                    var path = saveFileDialog.Path;
                    XmlReadWriter.Write<VntData>(path, _vntData);
                }
            };

            var toolPanelButtons = new List<ToolPanelButton>()
            {
                createNewButton,
                loadButton,
                saveButton
            };

            _vntView.DrawToolPanel(toolPanelButtons);
        }

        private void VntViewOnProcessedEvents(Event e)
        {
            foreach (var nodePresenter in _nodePresenters)
                nodePresenter.ProcessEvents(e);

            foreach (var connectionPresenter in _connectionPresenters)
                connectionPresenter.ProcessEvents(e);

            _linkToMousePresenter.ProcessEvents(e);
        }

        private void VntViewOnDrag(Vector2 vector2)
        {
            foreach (var nodePresenter in _nodePresenters)
                nodePresenter.Drag(vector2);
        }

        private void VntViewOnMouseClicked(Vector2 mousePosition)
        {
            var backgroundMenuItem = new ContextMenuItem
            {
                Title = LocalizationStrings.SetBackgroundNode,
            };
            backgroundMenuItem.Clicked += OnSetBackgroundClicked;

            var characterMenuItem = new ContextMenuItem
            {
                Title = LocalizationStrings.CharacterNode
            };
            characterMenuItem.Clicked += OnCharacterMenuItemClicked;

            var dualogueMenuItem = new ContextMenuItem
            {
                Title = LocalizationStrings.DialogueNode
            };
            dualogueMenuItem.Clicked += OnDialogueMenuItemClicked;

            var questionMenuItem = new ContextMenuItem
            {
                Title = LocalizationStrings.QuestionNode
            };
            questionMenuItem.Clicked += OnQuestionMenuItemClicked;

            var answerMenuItem = new ContextMenuItem
            {
                Title = LocalizationStrings.AnswerNode
            };
            answerMenuItem.Clicked += OnAnswerMenuItemClicked;

            var contextMenuItems = new List<ContextMenuItem>
            {
                backgroundMenuItem,
                characterMenuItem,
                dualogueMenuItem,
                questionMenuItem,
                answerMenuItem
            };

            _vntView.ShowContextMenu(mousePosition, contextMenuItems);
        }

        #endregion

        #region Create Nodes

        //todo refact

        private void OnSetBackgroundClicked(Vector2 mousePosition)
        {
        }

        private void OnCharacterMenuItemClicked(Vector2 mousePosition)
        {
        }

        private void OnDialogueMenuItemClicked(Vector2 mousePosition)
        {
            var nodeView = new DialogueNodeView(LocalizationStrings.DialogueNode, mousePosition);
            var connectionPointInPresenter =
                new PortPresenter(new PortView(PortType.In));
            var connectionPointOutPresenter =
                new PortPresenter(new PortView(PortType.Out));
            var go = new GameObject(LocalizationStrings.DialogueNode);
            var nodeData = go.AddComponent<DialogueNodeData>();
            var nodePresenter = new DialogueNodePresenter(nodeView, nodeData, connectionPointInPresenter,
                connectionPointOutPresenter);

            nodePresenter.ConnectionPointSelected += OnConnectionPointSelected;
            nodePresenter.ConnectionPointUnSelected += OnConnectionPointUnSelected;
            _nodePresenters.Add(nodePresenter);
            _vntData.AddNodeData(nodePresenter.NodeData);
        }

        private void OnQuestionMenuItemClicked(Vector2 mousePosition)
        {
        }

        private void OnAnswerMenuItemClicked(Vector2 mousePosition)
        {
        }

        #endregion

        #region Create Connection Points

        private void OnConnectionPointSelected(NodePresenter nodePresenter,
            PortPresenter portPresenter)
        {
            if (_selectedNodePresenter != null && _selectedNodePresenter.Id != nodePresenter.Id)
            {
                var selectedConnectionPointPresenter = _selectedPointPresenter;

                var connectionBetweenNodes = new LinkPresenter(new LinkView(), new LinkData());

                connectionBetweenNodes.SetFrom(selectedConnectionPointPresenter, _selectedNodePresenter.Id);
                connectionBetweenNodes.SetTo(portPresenter, nodePresenter.Id);

                _selectedNodePresenter.AddNextNode(nodePresenter);

                _connectionPresenters.Add(connectionBetweenNodes);

                _vntData.AddConnectionData(connectionBetweenNodes.LinkData);

                _selectedNodePresenter = null;

                return;
            }

            _selectedNodePresenter = nodePresenter;
            _selectedPointPresenter = portPresenter;
        }

        private void OnConnectionPointUnSelected(NodePresenter nodePresenter,
            PortPresenter portPresenter)
        {
            _selectedNodePresenter = null;
            _selectedPointPresenter = null;
        }

        #endregion
    }
}