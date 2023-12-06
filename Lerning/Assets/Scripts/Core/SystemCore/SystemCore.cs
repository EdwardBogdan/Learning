using UnityEngine;
using Core.Settings;

namespace Core
{
    public class SystemCore : MonoBehaviour
    {
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private DialogUIManager _dialogPrefab;

        [SerializeField] private Canvas _overlayCanvas;

        private static SystemCore _currentCore;

        public static AudioSource MusicSource => I._musicSource;
        public static AudioSource SFXSource => I._sfxSource;
        public static Canvas OverlayCanvas => I._overlayCanvas;
        public static SoundSettings SoundSettings => SoundSettings.I;
        public static HudSettings HudSettings => HudSettings.I;
        public static DialogUIManager DialogPrefab => I._dialogPrefab;
        private static SystemCore I => _currentCore;


        private void Awake()
        {
            if (IsCurrentSession())
            {
                DontDestroyOnLoad(gameObject);
                _currentCore = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private bool IsCurrentSession()
        {
            SystemCore[] _sessions = FindObjectsOfType<SystemCore>();
            foreach (SystemCore _session in _sessions)
            {
                if (_session != this)
                {
                    return false;
                } 
            }
            return true;
        }

    }

    public enum SoundSettingsMode
    {
        music,
        sfx,
    }
}
