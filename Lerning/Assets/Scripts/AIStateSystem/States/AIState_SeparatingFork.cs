using ColliderTouchSystem;
using PhysicModuleSystem;
using System;
using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_SeparatingFork : AIState
    {
        [SerializeField] private bool _moveToTarget;
        [Min(0.1f), SerializeField] private float _checkInterval;

        [AIAreaChoose, SerializeField] private string _chaseAreaName;
        [AIComponentRef, SerializeField] private string _physicControllerName;

        [Header("Exits")]
        [AIStateChoose, SerializeField] private AIState _targetLost;
        [SerializeField] private SeparetingCheck[] forkExits;

        private TouchComponent _chaseArea;
        private PhysicModuleController _controller;

        private Action stateLogic;

        public override string StateLogicName => "Separating Fork";
        public override IEnumerator StateLogic()
        {
            while (_chaseArea.IsTouched && machine.VisionRay)
            {
                stateLogic.Invoke();

                yield return new WaitForEndOfFrame();
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

            stateLogic = Check;

            if (_moveToTarget) stateLogic += Move;

            _controller = machine.GetComponentReference<PhysicModuleController>(_physicControllerName);
        }

        private void Check()
        {
            foreach (SeparetingCheck check in forkExits)
            {
                if (check.area.IsTouched)
                {
                    machine.StartState(check.state);
                    break;
                }
            }
        }
        private void Move()
        {
            Vector2 _direction = machine.DirectionToTarget;
            _controller.SetDirection(_direction);
        }
        [Serializable]
        private class SeparetingCheck
        {
            [AIAreaChoose]public string areaName;
            [AIStateChoose]public AIState state;

            [HideInInspector] public TouchComponent area;
        }
    }
}
