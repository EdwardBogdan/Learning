using UnityEditor;


namespace MyProject.Components.Triggers
{
    [CustomEditor(typeof(BaseTriggerComponent), true)]
    public class BaseTriggerComponentEditor : Editor
    {
        private SerializedProperty _checkByTagProp;
        private SerializedProperty _tagsProp;
        private SerializedProperty _checkByLayerProp;
        private SerializedProperty _layerMaskProp;

        private SerializedProperty _actionProp; // Добавляем это поле

        private void OnEnable()
        {
            _checkByTagProp = serializedObject.FindProperty("_checkByTag");
            _tagsProp = serializedObject.FindProperty("_tags");
            _checkByLayerProp = serializedObject.FindProperty("_checkByLayer");
            _layerMaskProp = serializedObject.FindProperty("_layerMask");

            _actionProp = serializedObject.FindProperty("_action"); // Находим поле _action
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_checkByTagProp);

            if (_checkByTagProp.boolValue)
            {
                EditorGUI.indentLevel++;
                _tagsProp.isExpanded = true;
                EditorGUILayout.PropertyField(_tagsProp, true);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_checkByLayerProp);

            if (_checkByLayerProp.boolValue)
            {
                EditorGUI.indentLevel++;
                _layerMaskProp.isExpanded = true;
                EditorGUILayout.PropertyField(_layerMaskProp);
                EditorGUI.indentLevel--;
            }

            // Отображаем поле _action стандартно
            EditorGUILayout.PropertyField(_actionProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}