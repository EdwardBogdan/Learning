using System.Collections;
using UnityEngine;
using MyProject.Utils;
using MyProject.Components.Triggers;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_LookingForTarget : AIState
    {
        [HideInInspector] public AIState _targetIsDetectedAgain;
        [HideInInspector] public AIState _targetLost;

        [SerializeField] private string _chaseAreaName;
        [SerializeField] private Cooldown _lostTargetDelay;

        private TouchComponent _chaseArea;
        public override string StateLogicName => "Looking for the Target";
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _chaseArea = machine.GetArea(_chaseAreaName);
        }
        public override IEnumerator StateLogic()
        {
            if (machine.Target != null)
            {
                machine.SetDirection(Vector2.zero);

                if (_lostTargetDelay.IsReady)
                {
                    machine.Spawner?.SpawnTarget("Dialog Request");
                } 

                _lostTargetDelay.Reset();

                while (!_lostTargetDelay.IsReady)
                {
                    if (_chaseArea.IsTouched && machine.VisionRay)
                    {
                        machine.StartState(_targetIsDetectedAgain);
                    }
                    yield return null;
                }
            }
            machine.SetTarget(null);
            machine.StartState(_targetLost);
        }
    }
}
