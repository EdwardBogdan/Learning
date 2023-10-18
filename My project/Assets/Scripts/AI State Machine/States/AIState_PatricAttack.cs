using MyProject.Physic.PAController;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_PatricAttack : AIState
    {
        [HideInInspector] public AIState _attackFinished;

        [SerializeField] private float _sec;

        private BaseAnimatorController _animator;
        public override string StateLogicName => "Patric attacks";
        public override IEnumerator StateLogic()
        {
            Vector3 point = machine.DirectionToTarget;

            float time = Time.time;

            while (Time.time <= (time + _sec))
            {
                _animator?.ACTriggerAttack();
                machine.SetDirection(point);
                yield return null;
            }

            machine.SetDirection(Vector3.zero);
            machine.SetTarget(null);
            machine.StartState(_attackFinished);
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _animator = machine.GetComponent<BaseAnimatorController>();
        }
    }
}
