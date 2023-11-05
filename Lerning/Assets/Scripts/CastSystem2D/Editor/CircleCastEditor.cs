using CastSystem2D.Components;
using UnityEditor;
using UnityEngine;

namespace CastSystem2D.Editors
{
    [CustomEditor(typeof(CastCircleComponent))]
    public class CircleCastEditor : Editor
    {
        public Texture2D icon;

        SerializedProperty _actionByNull;
        SerializedProperty _useByCombinator;
        SerializedProperty _tagFilter;
        SerializedProperty _tag;
        SerializedProperty _layer;
        SerializedProperty _color;
        SerializedProperty _checkRadius;
        SerializedProperty _position;
        SerializedProperty _action;
        SerializedProperty _onlyFirstCollected;

        private void OnEnable()
        {
            _actionByNull = serializedObject.FindProperty("_actionByNull");
            _onlyFirstCollected = serializedObject.FindProperty("_onlyFirstCollected");
            _useByCombinator = serializedObject.FindProperty("_useByCombinator");
            _tagFilter = serializedObject.FindProperty("_tagFilter");
            _tag = serializedObject.FindProperty("_tag");
            _layer = serializedObject.FindProperty("_layer");
            _color = serializedObject.FindProperty("_color");
            _checkRadius = serializedObject.FindProperty("_checkRadius");
            _position = serializedObject.FindProperty("_position");
            _action = serializedObject.FindProperty("_action");

            CastCircleComponent component = (CastCircleComponent)target;

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
            EditorGUILayout.PropertyField(_useByCombinator);
            EditorGUILayout.PropertyField(_tagFilter);

            EditorGUILayout.Space(5);

            if (_tagFilter.boolValue)
            {
                EditorGUILayout.PropertyField(_tag);
            }

            EditorGUILayout.PropertyField(_layer);
            EditorGUILayout.PropertyField(_color);

            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_checkRadius);
            EditorGUILayout.PropertyField(_position);

            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_action);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
