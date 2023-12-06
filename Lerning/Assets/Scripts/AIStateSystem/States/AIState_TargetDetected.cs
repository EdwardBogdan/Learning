using System.Collections;
using UnityEngine;
using PhysicModuleSystem;
using SpawnEquipment;

namespace AIStateSystem.States
{
    public class AIState_TargetDetected : AIState
    {
        [SerializeField]
        private string _dialogKey;

        [AIComponentRef, SerializeField] private string _physicControllerName;
        [AIComponentRef, SerializeField] private string _spawnManagerName;

        [Min(0), SerializeField] private float _delay;

        [Header("Exits"), AIStateChoose, SerializeField] 
        private AIState _targetStillDetected;

        [AIStateChoose, SerializeField] 
        private AIState _targetLost;
        public override string StateLogicName => "Target Detected";

        private PhysicModuleController _controller;
        private SpawnManager _spawner;

        public override IEnumerator StateLogic()
        {
            _spawner.SpawnTarget(_dialogKey);

            _controller.SetDirection(machine.DirectionToTarget);
            _controller.SetDirection(Vector3.zero);

            yield return new WaitForSeconds(_delay);

            if (machine.VisionRay)
            {
                machine.StartState(_targetStillDetected);
            }
            else
            {
                machine.StartState(_targetLost);
            }
        }

        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _controller = machine.GetComponentReference<PhysicModuleController>(_physicControllerName);
            _spawner = machine.GetComponentReference<SpawnManager>(_spawnManagerName);
        }
    }
}
