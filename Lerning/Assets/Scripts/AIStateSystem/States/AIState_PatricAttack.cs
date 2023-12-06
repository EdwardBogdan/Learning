using AnimatorController;
using CustomUnityUtils.TimeUtils;
using PhysicModuleSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_PatricAttack : AIState
    {
        [SerializeField] private string _attackEventKey;
        [SerializeField] private string _immunityOnEventKey;
        [SerializeField] private string _immunityOffEventKey;

        [AIComponentRef, SerializeField] 
        private string _physicControllerName;

        [AIComponentRef, SerializeField] 
        private string _animatorControllerName;

        [Min(0.1f), SerializeField] 
        private float _attakcDuration;

        [Min(0.1f), SerializeField]
        private float _restDuration;

        [SerializeField] private Cooldown _attackCD;

        [AIStateChoose, SerializeField] 
        private AIState _attackFinished;

        private UnityEvent _attackEvent;
        private PhysicModuleController _physic;
        private CreatureAnimatorController _animator;
        public override string StateLogicName => "Patric attacks";
        public override IEnumerator StateLogic()
        {
            _animator.GetEvent(_immunityOnEventKey).Invoke();

            float time = Time.time + _attakcDuration;

            while (Time.time <= (time))
            {
                if (_attackCD.IsReady)
                {
                    _attackCD.Reset();
                    _attackEvent.Invoke();
                }

                _physic.SetDirection(machine.DirectionToTarget);
                yield return new WaitForEndOfFrame();
            }
            _animator.GetEvent(_immunityOffEventKey).Invoke();

            _physic.SetDirection(Vector2.zero);

            yield return new WaitForSeconds(_restDuration);

            machine.StartState(_attackFinished);
        }
        public override void AwakeState(AIStateMachine machine)
        {
            _attackCD.Reset();
            base.AwakeState(machine);
            _animator = machine.GetComponentReference<CreatureAnimatorController>(_animatorControllerName);
            _attackEvent = _animator.GetEvent(_attackEventKey);
            _physic = machine.GetComponentReference<PhysicModuleController>(_physicControllerName);
        }
    }
}
