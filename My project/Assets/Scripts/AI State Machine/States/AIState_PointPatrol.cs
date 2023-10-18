using MyProject.AIStateMachineSpace.Patroling;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_PointPatrol : AIState
    {
        [SerializeField] float _treshold = 1f;

        [HideInInspector] public AIState _targetIsDetected;

        private int _pointIndex;
        private Transform[] _points;

        public override string StateLogicName => "Point Patrol";
        private bool IsOnPoint => (_points[_pointIndex].position - machine.SelfPos).magnitude < _treshold;
        public override IEnumerator StateLogic()
        {
            if (_points != null && _points.Length <= 0) yield break;

            while (true)
            {
                if (IsOnPoint)
                {
                    _pointIndex = (int)Mathf.Repeat(_pointIndex + 1, _points.Length);
                }

                if (machine.VisionRay)
                {
                    machine.StartState(_targetIsDetected);
                }

                var direction = _points[_pointIndex].position - machine.SelfPos;

                direction.y = 0;
                direction.Normalize();
                machine.SetDirection(direction);

                yield return null;
            }
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            _points = machine.GetComponent<PatrolPointsForAIState>()._points;
        }
        
    }
}
