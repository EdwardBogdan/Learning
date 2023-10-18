using MyProject.Components.Spawn;
using MyProject.Utils;
using UnityEngine;

namespace MyProject.Characters.Totem
{
    public class TotemMain : TotemBase
    {
        [SerializeField] SpawnComponent spawn;
        [SerializeField] Cooldown _attackRate;
        public override void Action()
        {
            _animator.SetClip("Attack");
            spawn.Spawn();
        }
        private void FixedUpdate()
        {
            if (_attackRate.IsReady)
            {
                _attackRate.Reset();
                Action();
            }
        }
    }
}
