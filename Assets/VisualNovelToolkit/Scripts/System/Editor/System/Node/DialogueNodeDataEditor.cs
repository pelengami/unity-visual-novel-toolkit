using Assets.VisualNovelToolkit.Scripts.System.Node.Dialogue;
using UnityEditor;
using UnityEngine;

namespace VisualNovelToolkit.Scripts.System.Editor.System.Node
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DialogueNodeData), true)]
    internal sealed class DialogueNodeDataEditor : NodeEditor
    {
        private const string DataPropertyName = "Data";
        private const string DataFieldName = "Data";
        
        private static readonly GUIContent DataInitialState = new GUIContent(DataFieldName);
        private SerializedProperty _data;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_data, DataInitialState);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _data = serializedObject.FindProperty(DataPropertyName);
        }
    }
}