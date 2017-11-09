using System.Collections.Generic;
using Assets.Editor.Localization;
using Assets.Editor.Serialization;
using Assets.Editor.System.ConnectionLine;
using Assets.Editor.System.ConnectionPoint;
using Assets.Editor.System.Node;
using Assets.Editor.System.Node.CharacterNode;
using Assets.Editor.System.Node.CharacterNode.AnswerNode;
using Assets.Editor.System.Node.CharacterNode.DialogueNode;
using Assets.Editor.System.Node.CharacterNode.QuestionNode;
using Assets.Editor.System.Node.SetBackgroundNode;
using Assets.Editor.ToolkitGui.Controls.ContextMenu;
using Assets.Editor.ToolkitGui.Controls.Dialog;
using Assets.Editor.ToolkitGui.Controls.ToolPanelButton;
using Assets.Editor.ToolkitGui.Styles;
using UnityEngine;

namespace Assets.Editor.Vnt
{
	sealed class VntPresenter
	{
		private readonly IVntView _vntView;
		private readonly List<NodePresenter> _nodePresenters = new List<NodePresenter>();
		private readonly List<ConnectionPresenter> _connectionPresenters = new List<ConnectionPresenter>();
		private readonly ConnectionPresenter _connectionToMousePresenter;
		private readonly VntData _vntData;

		private NodePresenter _selectedNodePresenter;
		private ConnectionPointPresenter _selectedPointPresenter;

		public VntPresenter(IVntView vntView, VntData vntData)
		{
			_vntView = vntView;
			_vntData = vntData;
			_vntView.MouseClicked += VntViewOnMouseClicked;
			_vntView.Awaked += VntViewOnAwaked;
			_vntView.OnGui += VntViewOnGui;
			_vntView.Drag += VntViewOnDrag;
			_vntView.ProcessedEvents += VntViewOnProcessedEvents;

			_connectionToMousePresenter = new ConnectionPresenter(new ConnectionView(), new ConnectionData());
		}

		private void VntViewOnAwaked()
		{
			StylesCollection.LoadStyles();
			_vntData.LoadNodes();

			OnSetBackgroundClicked(new Vector2(150, 150));
		}

		private void VntViewOnGui()
		{
			DrawToolPanel();

			foreach (var nodePresenter in _nodePresenters)
				nodePresenter.Draw();

			foreach (var connectionPresenter in _connectionPresenters)
				connectionPresenter.Draw();

			if (_selectedNodePresenter != null && _selectedNodePresenter.SelectedConnectionPointPresenter != null)
				_connectionToMousePresenter.Draw(_selectedNodePresenter.SelectedConnectionPointPresenter.Rect);
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

			_connectionToMousePresenter.ProcessEvents(e);
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

		#region Create Nodes

		//todo refact

		private void OnSetBackgroundClicked(Vector2 mousePosition)
		{
			var nodePresenter = SetBackgroundNodePresenter.Create(mousePosition);
			nodePresenter.ConnectionPointSelected += OnConnectionPointSelected;
			nodePresenter.ConnectionPointUnSelected += OnConnectionPointUnSelected;
			_nodePresenters.Add(nodePresenter);
			_vntData.AddNodeData(nodePresenter.NodeData);
		}

		private void OnCharacterMenuItemClicked(Vector2 mousePosition)
		{
			var nodePresenter = CharacterNodePresenter.Create(mousePosition);
			nodePresenter.ConnectionPointSelected += OnConnectionPointSelected;
			nodePresenter.ConnectionPointUnSelected += OnConnectionPointUnSelected;
			_nodePresenters.Add(nodePresenter);
			_vntData.AddNodeData(nodePresenter.NodeData);
		}

		private void OnDialogueMenuItemClicked(Vector2 mousePosition)
		{
			var nodePresenter = DialogueNodePresenter.Create(mousePosition);
			nodePresenter.ConnectionPointSelected += OnConnectionPointSelected;
			nodePresenter.ConnectionPointUnSelected += OnConnectionPointUnSelected;
			_nodePresenters.Add(nodePresenter);
			_vntData.AddNodeData(nodePresenter.NodeData);
		}

		private void OnQuestionMenuItemClicked(Vector2 mousePosition)
		{
			var nodePresenter = QuestionNodePresenter.Create(mousePosition);
			nodePresenter.ConnectionPointSelected += OnConnectionPointSelected;
			nodePresenter.ConnectionPointUnSelected += OnConnectionPointUnSelected;
			_nodePresenters.Add(nodePresenter);
			_vntData.AddNodeData(nodePresenter.NodeData);
		}

		private void OnAnswerMenuItemClicked(Vector2 mousePosition)
		{
			var nodePresenter = AnswerNodePresenter.Create(mousePosition);
			nodePresenter.ConnectionPointSelected += OnConnectionPointSelected;
			nodePresenter.ConnectionPointUnSelected += OnConnectionPointUnSelected;
			_nodePresenters.Add(nodePresenter);
			_vntData.AddNodeData(nodePresenter.NodeData);
		}

		#endregion

		#region ConnectionPoints

		private void OnConnectionPointSelected(NodePresenter nodePresenter, ConnectionPointPresenter connectionPointPresenter)
		{
			if (_selectedNodePresenter != null && _selectedNodePresenter.Id != nodePresenter.Id)
			{
				var selectedConnectionPointPresenter = _selectedPointPresenter;

				var connectionBetweenNodes = new ConnectionPresenter(new ConnectionView(), new ConnectionData());

				connectionBetweenNodes.SetFrom(selectedConnectionPointPresenter, _selectedNodePresenter.Id);
				connectionBetweenNodes.SetTo(connectionPointPresenter, nodePresenter.Id);

				_selectedNodePresenter.AddNextNode(nodePresenter);

				_connectionPresenters.Add(connectionBetweenNodes);

				_vntData.AddConnectionData(connectionBetweenNodes.ConnectionData);

				_selectedNodePresenter = null;

				return;
			}

			_selectedNodePresenter = nodePresenter;
			_selectedPointPresenter = connectionPointPresenter;
		}

		private void OnConnectionPointUnSelected(NodePresenter nodePresenter, ConnectionPointPresenter connectionPointPresenter)
		{
			_selectedNodePresenter = null;
			_selectedPointPresenter = null;
		}

		#endregion
	}
}
