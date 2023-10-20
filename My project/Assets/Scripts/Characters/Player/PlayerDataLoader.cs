using UnityEngine;
using MyProject.Data;
using MyProject.Components.Health;
using MyProject.Physic.PAController;

namespace MyProject.Characters
{
    public class PlayerDataLoader : MonoBehaviour
    {
        public GameObject _player;

        private void Start()
        {
            HealthComponent _health = _player.GetComponent<HealthComponent>();
            var data = GameSession.CurrentSession.PersonData;
            _health.SetMaxHealth(data.MaxHP);
            _health.SetHealth(data.HP);

            PlayerAnimatorController _animator = _player.GetComponent<PlayerAnimatorController>();
            _animator.SetArming(data.IsArmed());

            Destroy(this);
        }
    }
}
