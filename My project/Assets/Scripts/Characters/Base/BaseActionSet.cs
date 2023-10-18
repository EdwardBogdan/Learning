using MyProject.Components.Cast;
using UnityEngine;

namespace MyProject.Characters
{
    public class BaseActionSet : MonoBehaviour
    {
        [SerializeField] protected CastComponent _castAttack;

        public virtual void AttackAction()
        {
            _castAttack.Cast();
        }
    }
}

