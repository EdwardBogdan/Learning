using UnityEditor;
using UnityEngine;

namespace CustomUnityUtils.Editors
{
    [CustomEditor(typeof(SORefer))]
    public class SOReferEditor : Editor
    {
        private delegate void OnDrawDelegate();

        private OnDrawDelegate OnDraw;

        private SORefer obj;

        private Editor editor;

        private SerializedProperty property;
        public override void OnInspectorGUI()
        {
            OnDraw?.Invoke();
        }

        private void DrawDefault()
        {
            DrawDefaultInspector();

            if (obj.Target != null)
            {
                ChangeMode();
            }
        }
        private void DrawEditor()
        {
            if (GUILayout.Button("Remove SO"))
            {
                obj.ChangeTarget(null);

                ChangeMode();
                return;
            }

            EditorGUILayout.PropertyField(property);

            EditorGUILayout.Space(5);

            editor.OnInspectorGUI();
        }

        private void ChangeMode()
        {

            if (obj.Target == null)
            {
                OnDraw = DrawDefault;
            }
            else
            {
                editor = CreateEditor(obj.Target);

                OnDraw = DrawEditor;
            }
        }


        private void OnEnable()
        {
            obj = (SORefer)target;

            property = serializedObject.FindProperty("_target");

            ChangeMode();
        }
    }
}
