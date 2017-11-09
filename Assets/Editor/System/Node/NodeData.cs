using System;
using System.Xml.Serialization;
using Assets.Editor.System.Node.CharacterNode;
using Assets.Editor.System.Node.CharacterNode.AnswerNode;
using Assets.Editor.System.Node.CharacterNode.DialogueNode;
using Assets.Editor.System.Node.CharacterNode.QuestionNode;
using Assets.Editor.System.Node.SetBackgroundNode;
using UnityEngine;

namespace Assets.Editor.System.Node
{
	[XmlInclude(typeof(AnswerNodeData))]
	[XmlInclude(typeof(DialogueNodeData))]
	[XmlInclude(typeof(QuestionNodeData))]
	[XmlInclude(typeof(CharacterNodeData))]
	[XmlInclude(typeof(SetBackgroundNodeData))]
	public abstract class NodeData : IEquatable<NodeData>
	{
		private readonly Guid _id;
		private float _x;
		private float _y;

		protected NodeData()
		{
			_id = Guid.NewGuid();
		}

		public Guid Id
		{
			get { return _id; }
		}

		public float X
		{
			get { return _x; }
			set { _x = value; }
		}

		public float Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public bool Equals(NodeData other)
		{
			return other != null && other.Id == Id;
		}
	}
}
