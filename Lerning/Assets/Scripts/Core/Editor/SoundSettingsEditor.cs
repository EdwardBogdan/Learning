using Core.Data.PersistentProperty;
using UnityEditor;
using UnityEngine;

namespace Core.Settings.Editors
{
    [CustomEditor(typeof(SoundSettings))]
    public class SoundSettingsEditor : Editor
    {
        private SoundSettings _target;

        private FloatPersistentProperty music;
        private FloatPersistentProperty sfx;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Music");
            music.Value = (float)EditorGUILayout.IntSlider(Mathf.RoundToInt(music.Value * 100), 0, 100) / 100;

            EditorGUILayout.LabelField("SFX");
            sfx.Value = (float)EditorGUILayout.IntSlider(Mathf.RoundToInt(sfx.Value * 100), 0, 100) / 100;
        }

        private void OnEnable()
        {
            _target = (SoundSettings)target;

            music = _target.GetSoundProperty(SoundSettingsMode.music);
            sfx = _target.GetSoundProperty(SoundSettingsMode.sfx);
        }
    }
}
