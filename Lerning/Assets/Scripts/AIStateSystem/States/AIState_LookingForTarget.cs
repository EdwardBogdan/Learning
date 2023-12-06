using System.Collections;
using UnityEngine;
using CustomUnityUtils.TimeUtils;
using ColliderTouchSystem;
using PhysicModuleSystem;
using SpawnEquipment;

namespace AIStateSystem.States
{
    public class AIState_LookingForTarget : AIState
    {
        [SerializeField] 
        private string _dialogKey;

        [AIAreaChoose, SerializeField] 
        private string _chaseAreaName;

        [AIComponentRef, SerializeField] 
        private string _spawnerName;

        [AIComponentRef, SerializeField] 
        private string _physicControllerName;

        [SerializeField] 
        private Cooldown _lostTargetDelay;

        [Header("Exits"), AIStateChoose, SerializeField] private AIState _targetIsDetectedAgain;
        [AIStateChoose, SerializeField] private AIState _targetLost;

        private TouchComponent _chaseArea;

        private SpawnManager _spawner;
        private PhysicModuleController _physic;
        public override string StateLogicName => "Looking for the Target";
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _chaseArea = machine.GetArea(_chaseAreaName);
            _spawner = machine.GetComponentReference<SpawnManager>(_spawnerName);
            _physic = machine.GetComponentReference<PhysicModuleController>(_physicControllerName);
        }
        public override IEnumerator StateLogic()
        {
            if (machine.Target != null)
            {
                _physic.SetDirection(Vector2.zero);

                if (_lostTargetDelay.IsReady)
                {
                   _spawner.SpawnReturnTarget(_dialogKey);
                } 

                _lostTargetDelay.Reset();

                while (!_lostTargetDelay.IsReady)
                {
                    if (_chaseArea.IsTouched && machine.VisionRay)
                    {
                        machine.StartState(_targetIsDetectedAgain);
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
            machine.SetTarget(null);
            machine.StartState(_targetLost);
        }
    }
}
