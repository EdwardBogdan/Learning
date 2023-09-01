using MyProject.Components;
using UnityEngine;

namespace MyProject.Characters
{
    public class CharacterBehaviour : MonoBehaviour
    {
        [SerializeField] protected CastComponent _castAttack;
        protected GeneralAnimationController _animatorController;
        protected void Awake()
        {
            _animatorController = GetComponent<GeneralAnimationController>();
        }
        #region Triggers
        public virtual void OnTriggerAttack()
        {
            _animatorController.SetTriggerAttack();
        }
        public virtual void OnTriggerSpecialAttack()
        {
            _animatorController.SetTriggerSpecialAttack();
        }
        public virtual void OnTriggerHit()
        {
            _animatorController.SetTriggerHit();
        }
        public virtual void OnTriggerDeath()
        {
            _animatorController.SetTriggerDeathHit();
        }
        #endregion
        public virtual void AttackAction()
        {
            _castAttack.Cast();
        }
    }
}

