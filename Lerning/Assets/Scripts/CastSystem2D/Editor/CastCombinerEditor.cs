using CastSystem2D.Components;
using UnityEditor;
using UnityEngine;

namespace CastSystem2D.Editors
{
    [CustomEditor(typeof(CastCombinerComponent))]
    public class CastCombinerEditor : Editor
    {
        public Texture2D icon;

        SerializedProperty _actionByNull;
        SerializedProperty _onlyFirstCollected;
        SerializedProperty _action;

        private void OnEnable()
        {
            _actionByNull = serializedObject.FindProperty("_actionByNull");
            _onlyFirstCollected = serializedObject.FindProperty("_onlyFirstCollected");
            _action = serializedObject.FindProperty("_action");

            CastCombinerComponent component = (CastCombinerComponent)target;

            if (icon != null)
            {
                EditorGUIUtility.SetIconForObject(component, icon);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_actionByNull);

            EditorGUILayout.PropertyField(_onlyFirstCollected);

            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_action);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
