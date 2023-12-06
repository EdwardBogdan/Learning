using CastSystem2D.Components;
using UnityEngine;

namespace CreatureController
{
    public class CreatureActionSet : MonoBehaviour
    {
        [SerializeField] private CastComponent _attackCast;

        public void AttackCast()
        {
            _attackCast.Cast();
        }
    }
}

