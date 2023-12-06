using SoundsAndUI.Components;
using UnityEngine;

namespace PlayerController
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] 
        private RuntimeAnimatorController animatorArmed;

        [SerializeField] 
        private RuntimeAnimatorController animatorDisArmed;

        [SerializeField] private PlaySoundsComponent _speeker;

        private PlayerActionSet _actionSet;

        
        #region Animator Keys
        protected static readonly int animatorKey_AttackIndex = Animator.StringToHash("Attack Index");
        
        #endregion

        private static PlayerAnimatorController _currentAnimator;
        public static PlayerAnimatorController I => _currentAnimator;

        protected virtual void Awake()
        {
            _currentAnimator = this;
            //_actionSet = (PlayerActionSet)_actions;
        }
        protected void FixedUpdate()
        {
           // base.FixedUpdate();
            //animator.SetBool(animatorKey_IsWalled, IsWalled);
        }

        public void ACTriggerAttackIndex(int index)
        {
            //animator.SetInteger(animatorKey_AttackIndex, index);
        }

        public void ACTriggerThrow()
        { 
        
        }
        public void PlaySound(string id)
        {
            _speeker.PlaySound(id);
        }
        public void AttackCast()
        {
            _actionSet.AttackCast();
        }
        internal void ChangeArmedMode(bool value)
        {
           //if (value) animator.runtimeAnimatorController = animatorArmed;
           //else animator.runtimeAnimatorController = animatorDisArmed;
        }
    }
}
