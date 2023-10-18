using MyProject.Utils;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_TargetDetected : AIState
    {
        [HideInInspector] public AIState _targetStillDetected;
        [HideInInspector] public AIState _targetLost;

        [SerializeField] private Cooldown _aggressionDelay;
        public override string StateLogicName => "Target Detected";

        public override IEnumerator StateLogic()
        {
            if (machine.VisionRay)
            {
                machine.SetDirection(machine.DirectionToTarget);
                machine.SetDirection(Vector3.zero);

                if (_aggressionDelay.IsReady)
                {
                    machine.Spawner?.SpawnTarget("Dialog Dead");
                    _aggressionDelay.Reset();
                }
                while (!_aggressionDelay.IsReady)
                {
                    machine.SetDirection(Vector3.zero);
                    yield return null;
                }
                machine.StartState(_targetStillDetected);
            }
            else
            {
                machine.StartState(_targetLost);
            }
        }
    }
}
