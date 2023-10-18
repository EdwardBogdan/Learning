using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_WaitTheTarget : AIState
    {
        [HideInInspector] public AIState _targetIsDetected;
        public override string StateLogicName => "Wait the Target";
        public override IEnumerator StateLogic()
        {
            while (true)
            {
                if (machine.VisionRay)
                {
                    machine.StartState(_targetIsDetected);
                }
                yield return null;
            }
        }
    }
}
