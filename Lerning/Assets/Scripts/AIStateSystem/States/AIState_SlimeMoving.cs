using System.Collections;
using UnityEngine;
using CustomUnityUtils.TimeUtils;
using ColliderTouchSystem;
using PhysicModuleSystem;

namespace AIStateSystem.States
{
    public class AIState_SlimeMoving : AIState
    {
        [AIComponentRef, SerializeField] private string _physicModuleName;
        [AIAreaChoose, SerializeField] private string _areaWallName;
        [SerializeField] private Cooldown _waitTimeToChange;

        private TouchComponent wall;

        private PhysicModuleController _controller;

        private Vector3 direction;

        public override string StateLogicName => "Slime Moving";
        public override IEnumerator StateLogic()
        {
            while(true)
            {
                if (wall.IsTouched && _waitTimeToChange.IsReady)
                {
                    direction *= -1;
                    _waitTimeToChange.Reset();
                }

                _controller.SetDirection(direction);

                yield return new WaitForEndOfFrame();
            }
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);

            wall = machine.GetArea(_areaWallName);

            int x = Random.Range(0, 2) * 2 - 1;

            direction = new(x, 0, 0);

            _waitTimeToChange.Reset();

            _controller = machine.GetComponentReference<PhysicModuleController>(_physicModuleName);

            wall = machine.GetArea(_areaWallName);
        }
    }
}