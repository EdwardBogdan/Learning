using UnityEditor;

namespace AnimatorController
{
    [CustomEditor(typeof(CreatureAnimatorController))]
    public class CreatureAnimatorControllerEditor : Editor
    {
        SerializedProperty _animator;
        SerializedProperty _setGround;
        SerializedProperty _setWall;
        SerializedProperty _setRunning;
        SerializedProperty _setVerticalVelocity;
        SerializedProperty _ground;
        SerializedProperty _wall;
        SerializedProperty _body;
        SerializedProperty _eventRefs;
        SerializedProperty _eventStringRefs;

        private void OnEnable()
        {
            _animator = serializedObject.FindProperty("animator");

            _setGround = serializedObject.FindProperty("_setGround");
            _setWall = serializedObject.FindProperty("_setWall");
            _setRunning = serializedObject.FindProperty("_setRunning");
            _setVerticalVelocity = serializedObject.FindProperty("_setVerticalVelocity");

            _ground = serializedObject.FindProperty("_groundTouch");
            _wall = serializedObject.FindProperty("_wallTouch");
            _body = serializedObject.FindProperty("_rigidbody");

            _eventRefs = serializedObject.FindProperty("_eventRefs");
            _eventStringRefs = serializedObject.FindProperty("_eventStringRefs");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_animator);

            EditorGUILayout.Space(3);

            EditorGUILayout.PropertyField(_setGround);
            EditorGUILayout.PropertyField(_setWall);

            EditorGUILayout.PropertyField(_setRunning);
            EditorGUILayout.PropertyField(_setVerticalVelocity);

            EditorGUILayout.Space(3);

            if (_setGround.boolValue)
            {
                EditorGUILayout.PropertyField(_ground);
            }

            if (_setWall.boolValue)
            {
                EditorGUILayout.PropertyField(_wall);
            }

            if (_setRunning.boolValue || _setVerticalVelocity.boolValue)
            {
                EditorGUILayout.PropertyField(_body);
                EditorGUILayout.Space(3);
            }

            EditorGUILayout.PropertyField(_eventRefs);

            EditorGUILayout.PropertyField(_eventStringRefs);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
