using UnityEngine;
using MyProject.Data;
using MyProject.Components;

namespace MyProject.Characters
{
    public class PlayerData : MonoBehaviour
    {
        private void Start()
        {
            GameSession _session = GameSession.CurrentSession;

            HealthComponent _health = GetComponent<HealthComponent>();
            _health.SetMaxHealth(_session._playerMaxHP);
            _health.SetHealth(_session._playerHP);

            PlayerAnimationController _animator = GetComponent<PlayerAnimationController>();
            _animator.SetArming(_session._isArmed);

            Destroy(this);
        }
    }
}
