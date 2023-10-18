using UnityEngine;

namespace MyProject.Data
{
    public class GameSession : MonoBehaviour
    {
        public int _coins;
        public int _swords;

        public int _playerHP;
        public int _playerMaxHP;

        public bool _isArmed;

        public static GameSession CurrentSession => _currentSession;
        private static GameSession _currentSession;
        private void Awake()
        {
            if (IsCurrentSession())
            {
                DontDestroyOnLoad(this.gameObject);
                _currentSession = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private bool IsCurrentSession()
        {
            GameSession[] _sessions = FindObjectsOfType<GameSession>();
            foreach (GameSession _session in _sessions)
            {
                if (_session != this) return false;
            }
            return true;
        }
    }
}
