using MyProject.Components.Triggers;
using MyProject.Physic.PAController;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_AnimationThrow : AIState
    {
        [HideInInspector] public AIState _afterThrow;

        [SerializeField] private string _throwAreaName;

        private TouchComponent _throwArea;

        private BaseAnimatorController _animator;

        public override string StateLogicName => "Animation throw to the Target";
        public override IEnumerator StateLogic()
        {
            while (_throwArea.IsTouched && machine.VisionRay)
            {
                _animator.ACTriggerThrow();
                yield return null;
            }
            machine.StartState(_afterThrow);
        }

        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _animator = machine.GetComponent<BaseAnimatorController>();
            _throwArea = machine.GetArea(_throwAreaName);
        }
    }
}
