using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.VisualNovelToolkit.Scripts.System.Node
{
    [Serializable]
    public abstract class NodeData : MonoBehaviour, IEquatable<NodeData>
    {
        public Guid Id;
        public float X;
        public float Y;

        public Object RuntimeInstance => Selection.activeObject;

        public bool IsValid => true;

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