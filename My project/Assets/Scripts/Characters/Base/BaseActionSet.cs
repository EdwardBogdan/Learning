using MyProject.Components;
using MyProject.Components.Cast;
using UnityEngine;

namespace MyProject.Characters
{
    public class BaseActionSet : MonoBehaviour
    {
        [SerializeField] protected CastComponent _castAttack;
        [SerializeField] protected PlaySoundsComponent _speeker;

        public virtual void AttackAction()
        {
            _castAttack.Cast();
        }

        public void PlaySound(string id)
        {
            _speeker?.Play(id);
        }
    }
}

