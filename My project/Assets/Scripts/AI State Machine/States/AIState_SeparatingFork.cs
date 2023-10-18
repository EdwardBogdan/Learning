using MyProject.Components.Triggers;
using System;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_SeparatingFork : AIState
    {
        [HideInInspector] public AIState _targetLost;

        [SerializeField] private string _chaseAreaName;
        [SerializeField] private bool _moveToTarget;
        [SerializeField] private SeparetingCheck[] forkExits;

        private TouchComponent _chaseArea;

        public override string StateLogicName => "Separating Fork";
        public override IEnumerator StateLogic()
        {
            while (_chaseArea.IsTouched && machine.VisionRay)
            {
                foreach (SeparetingCheck check in forkExits)
                {
                    if (check.area.IsTouched)
                    {
                        machine.StartState(check.state);
                    }
                }

                if (_moveToTarget)
                {
                    Vector2 _direction = machine.DirectionToTarget;
                    machine.SetDirection(_direction);
                }
                yield return null;
            }
            machine.StartState(_targetLost);
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _chaseArea = machine.GetArea(_chaseAreaName);

            foreach (SeparetingCheck check in forkExits)
            {
                check.area = machine.GetArea(check.areaName);
            }
        }
    }

    [Serializable]
    class SeparetingCheck
    {
        public string areaName;
        public AIState state;

        [HideInInspector] public TouchComponent area;
    }
}
