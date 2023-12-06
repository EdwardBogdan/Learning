using UnityEditor;

namespace ColliderTouchSystem.Editors
{
    [CustomEditor(typeof(BaseTriggerComponent), true)]
    public class BaseTriggerComponentEditor : Editor
    {
        private SerializedProperty _checkByTagProp;
        private SerializedProperty _ignoreTagProp;
        private SerializedProperty _tagsProp;
        private SerializedProperty _ignoreTagsProp;
        private SerializedProperty _checkByLayerProp;
        private SerializedProperty _layerMaskProp;

        private SerializedProperty _actionProp;

        private void OnEnable()
        {
            _checkByTagProp = serializedObject.FindProperty("_checkByTag");
            _ignoreTagProp = serializedObject.FindProperty("_ignoreByTag");
            _tagsProp = serializedObject.FindProperty("_tags");
            _ignoreTagsProp = serializedObject.FindProperty("_ignoreTags");
            _checkByLayerProp = serializedObject.FindProperty("_checkByLayer");
            _layerMaskProp = serializedObject.FindProperty("_layerMask");

            _actionProp = serializedObject.FindProperty("_action");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_checkByTagProp);

            if (_checkByTagProp.boolValue)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(_tagsProp, true);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_ignoreTagProp);

            if (_ignoreTagProp.boolValue)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(_ignoreTagsProp, true);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_checkByLayerProp);

            if (_checkByLayerProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_layerMaskProp);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_actionProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}