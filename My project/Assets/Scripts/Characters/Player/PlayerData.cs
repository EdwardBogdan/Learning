using UnityEngine;
using MyProject.Data;
using MyProject.Components.Health;
using MyProject.Physic.PAController;

namespace MyProject.Characters
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private void Start()
        {
            GameSession _session = GameSession.CurrentSession;

            HealthComponent _health = _player.GetComponent<HealthComponent>();
            _health.SetMaxHealth(_session._playerMaxHP);
            _health.SetHealth(_session._playerHP);

            PlayerAnimatorController _animator = _player.GetComponent<PlayerAnimatorController>();
            _animator.SetArming(_session._isArmed);

            Destroy(this);
        }
    }
}
