using MyProject.Components.Triggers;
using MyProject.Physic.PAController;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_AnimationAttack : AIState
    {
        [HideInInspector] public AIState _targetOutAttackArea;

        [SerializeField] private string _attackAreaName;

        private TouchComponent _attackArea;

        private BaseAnimatorController _animator;

        public override string StateLogicName => "Animation attack to the Target";
        public override IEnumerator StateLogic()
        {
            while (_attackArea.IsTouched)
            {
                _animator?.ACTriggerAttack();
                machine.SetDirection(Vector2.zero);
                yield return null;
            }
            machine.StartState(_targetOutAttackArea);
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _animator = machine.GetComponent<BaseAnimatorController>();
            _attackArea = machine.GetArea(_attackAreaName);
        }
    }
}
