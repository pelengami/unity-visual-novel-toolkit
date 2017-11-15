using Assets.Editor.System.Node;
using UnityEditor;
using UnityEngine;

namespace VisualNovelToolkit.Scripts.System.Editor.System.Node
{
    [CustomEditor(typeof(NodeData))]
    abstract class NodeEditor : UnityEditor.Editor
    {
        private const string NameFieldName = "Name";
        
        protected override void OnHeaderGUI()
        {
            var node = (NodeData)target;

            if (!node.IsValid) return;

            var title = ObjectNames.NicifyVariableName(node.GetType().Name);

            GUILayout.BeginHorizontal();
            GUILayout.Space(14);
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        public override void OnInspectorGUI()
        {
            var node = (NodeData)target;

            if (!node.IsValid) return;

            node.name = EditorGUILayout.TextField(NameFieldName, node.name);

            EditorGUILayout.Space();
        }
    }
}