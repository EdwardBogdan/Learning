using AIStateSystem.Patroling;
using PhysicModuleSystem;
using System.Collections;
using UnityEngine;

namespace AIStateSystem.States
{
    public class AIState_PointPatrol : AIState
    {
        [Min(0.1f), SerializeField] 
        private float _checkInterval;

        [SerializeField] 
        private float _treshold = 1f;

        [AIComponentRef, SerializeField] 
        private string _physicControllerName;

        [Header("Exits"), AIStateChoose, SerializeField] 
        private AIState _targetIsDetected;

        private int _pointIndex;
        private Transform[] _points;

        private PhysicModuleController _physic;

        private delegate IEnumerator logic();
        private logic stateLogic;
        public override string StateLogicName => "Point Patrol";
        private bool IsOnPoint => (_points[_pointIndex].position - machine.SelfPos).magnitude < _treshold;
        public override IEnumerator StateLogic()
        {
            while (true)
            {
                if (machine.VisionRay)
                {
                    machine.StartState(_targetIsDetected);
                    break;
                }

                yield return stateLogic.Invoke();
            }
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);

            if(machine.TryGetComponent(out PatrolPointsForAIState patrol))
            {
                _points = patrol._points;
            }

            stateLogic = (_points == null || _points.Length <= 0) ? JustWait : Patrol;

            _physic = machine.GetComponentReference<PhysicModuleController>(_physicControllerName);
        }

        private IEnumerator JustWait()
        {
            _physic.SetDirection(new(0, 0));
            yield return new WaitForSeconds(_checkInterval);
        }

        private IEnumerator Patrol()
        {
            if (IsOnPoint)
            {
                _pointIndex = (int)Mathf.Repeat(_pointIndex + 1, _points.Length);
            }

            var direction = _points[_pointIndex].position - machine.SelfPos;

            direction.y = 0;
            direction.Normalize();
            _physic.SetDirection(direction);

            yield return new WaitForSeconds(_checkInterval);
        }
    }
}
