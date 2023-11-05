using UnityEditor;
using UnityEngine;

namespace CustomUnityUtils.GUI
{
    public static class CustomGUILayout
    {
        public static void DrawHorizontalLine(float height)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            rect.height = height;
            GUILayout.Space(3);
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            GUILayout.Space(3);
        }
        public static void DrawHorizontalLine()
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            rect.height = 1;
            GUILayout.Space(3);
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            GUILayout.Space(3);
        }
    }
}