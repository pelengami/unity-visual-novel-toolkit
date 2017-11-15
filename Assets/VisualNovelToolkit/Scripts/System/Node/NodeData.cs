using System;
using System.Xml.Serialization;
using Assets.Editor.System.Node.CharacterNode;
using Assets.Editor.System.Node.SetBackgroundNode;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Editor.System.Node
{
    [XmlInclude(typeof(CharacterNodeData))]
    [XmlInclude(typeof(SetBackgroundNodeData))]
    [Serializable]
    public abstract class NodeData : MonoBehaviour, IEquatable<NodeData>
    {
        public Guid Id;
        public float X;
        public float Y;

        public Object RuntimeInstance
        {
            get { return Selection.activeObject; }
        }

        public bool IsValid { get { return true; } }

        protected NodeData()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(NodeData other)
        {
            return other != null && other.Id == Id;
        }
    }
}