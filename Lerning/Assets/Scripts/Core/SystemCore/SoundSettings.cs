using Core.Data.PersistentProperty;
using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(menuName = "Data/Settings/SoundSettings", fileName = "SoundSettings")]
    public class SoundSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty music;
        [SerializeField] private FloatPersistentProperty sfx;


        private static SoundSettings _instance;
        internal static SoundSettings I => _instance == null ? LoadSingleton() : _instance;

        public static void SetMusic(AudioClip clip)
        {
            SystemCore.MusicSource.clip = clip;
            SystemCore.MusicSource.Play();
        }
        public FloatPersistentProperty GetSoundProperty(SoundSettingsMode mode)
        {
            switch (mode)
            {
                case SoundSettingsMode.music:
                    return I.music;

                case SoundSettingsMode.sfx:
                    return I.sfx;
                default:
                    Debug.Log("Sound mode not identified");
                    return null;
            }
        }

        private static SoundSettings LoadSingleton()
        {
            return _instance = Resources.Load<SoundSettings>("Settings/SoundSettings");
        }
        private void OnEnable()
        {
            music = new FloatPersistentProperty(0.5f, SoundSettingsMode.music.ToString());
            sfx = new FloatPersistentProperty(0.5f, SoundSettingsMode.sfx.ToString());
        }
        private void OnValidate()
        {
            music.Validate();
            sfx.Validate();
        }
    }
}
