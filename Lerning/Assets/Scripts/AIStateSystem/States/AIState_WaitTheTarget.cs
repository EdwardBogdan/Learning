using PhysicModuleSystem;
using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_WaitTheTarget : AIState
    {
        [Min(0.1f), SerializeField] private float _checkInterval;
        [AIComponentRef, SerializeField] private string _physicControllerName;
        [AIStateChoose, SerializeField] private AIState _targetIsDetected;
        
        public override string StateLogicName => "Wait the Target";

        private PhysicModuleController _physic;
        public override IEnumerator StateLogic()
        {
            machine.SetTarget(null);
            _physic.SetDirection(Vector2.zero);

            while (true)
            {
                yield return new WaitForSeconds(_checkInterval);

                if (machine.VisionRay)
                {
                    machine.StartState(_targetIsDetected);
                    break;
                }
            }
        }

        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _physic = machine.GetComponentReference<PhysicModuleController>(_physicControllerName); ;
        }
    }
}
